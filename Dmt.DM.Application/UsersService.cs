using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.EntityFrameWork;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmt.DM.Application
{
    public interface IUsersService
    {
        Task<List<UserEntity>> GetList(Pagination pagination, string keyword);
        IQueryable<UserEntity> GetUserNameDict(string keyValue);
        Task<string> GetSerialNumberAsync(string userId);
        Task<UserEntity> FindUserAsync(string username, string password);
        Task<UserEntity> FindUserAsync(string userId);
        Task UpdateUserLastActivityDateAsync(string userId);
        Task<UserEntity> GetCurrentUserAsync();
        string GetCurrentUserId();
        Task<(bool Succeeded, string Error)> ChangePasswordAsync(UserEntity user, string currentPassword, string newPassword);
        Task<(bool Succeeded, string Error)> RevisePasswordAsync(UserEntity user, string newPassword);
        Task<int> UpdateForm(UserEntity userEntity);
        Task<int> SubmitForm<TDto>(UserEntity userEntity, TDto dto) where TDto: class;
    }

    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork<HsDbContext> _uow;
        private readonly DbSet<UserEntity> _users;
        private readonly ISecurityService _securityService;
        private readonly IHttpContextAccessor _contextAccessor;

        public UsersService(
            IUnitOfWork<HsDbContext> uow,
            ISecurityService securityService,
            IHttpContextAccessor contextAccessor)
        {
            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _users = _uow.Set<UserEntity>();

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));

            _contextAccessor = contextAccessor;
            _contextAccessor.CheckArgumentIsNull(nameof(_contextAccessor));
        }

        public Task<List<UserEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<UserEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Account.Contains(keyword));
                expression = expression.Or(t => t.F_RealName.Contains(keyword));
                expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            expression = expression.And(t => t.F_Account != "admin");
            return _uow.GetRepository<UserEntity>().FindListAsync(expression, pagination);
        }

        public IQueryable<UserEntity> GetUserNameDict(string keyValue)
        {
            var expression = ExtLinq.True<UserEntity>();
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                expression = expression.And(t => t.F_RealName.Contains(keyValue));
                expression = expression.Or(t => t.F_Account.Contains(keyValue));
            }

            return _users.Where(expression).SortBy(t => t.F_RealName);
        }

        public Task<UserEntity> FindUserAsync(string userId)
        {
            return _users.FindAsync(userId);
        }

        public Task<UserEntity> FindUserAsync(string username, string password)
        {
            var passwordHash = _securityService.GetSha256Hash(password);
            return _users.FirstOrDefaultAsync(x => x.F_Account == username && x.F_Password == passwordHash);
        }

        public async Task<string> GetSerialNumberAsync(string userId)
        {
            var user = await FindUserAsync(userId);
            return user.F_SerialNumber;
        }

        public async Task UpdateUserLastActivityDateAsync(string userId)
        {
            var user = await FindUserAsync(userId);
            if (user.F_LastLoggedIn != null)
            {
                var updateLastActivityDate = TimeSpan.FromMinutes(2);
                var currentUtc = DateTimeOffset.Now;
                var timeElapsed = currentUtc.Subtract(user.F_LastLoggedIn.Value);
                if (timeElapsed < updateLastActivityDate)
                {
                    return;
                }
            }
            user.F_LastLoggedIn = DateTimeOffset.Now;
            await _uow.SaveChangesAsync();
        }

        public string GetCurrentUserId()
        {
            var claimsIdentity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userDataClaim = claimsIdentity?.FindFirst(ClaimTypes.UserData);
            var userId = userDataClaim?.Value;
            return userId ?? "";
        }

        public Task<UserEntity> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            return FindUserAsync(userId);
        }

        public async Task<(bool Succeeded, string Error)> ChangePasswordAsync(UserEntity user, string currentPassword, string newPassword)
        {
            var currentPasswordHash = _securityService.GetSha256Hash(currentPassword);
            if (user.F_Password != currentPasswordHash)
            {
                return (false, "Current password is wrong.");
            }

            user.F_Password = _securityService.GetSha256Hash(newPassword);
            user.F_SerialNumber = Guid.NewGuid().ToString("N"); // To force other logins to expire.
            await _uow.SaveChangesAsync();
            return (true, string.Empty);
        }

        public Task<int> UpdateForm(UserEntity userEntity)
        {
            userEntity.F_LastModifyUserId = GetCurrentUserId();
            userEntity.F_LastModifyTime=DateTime.Now;
            return _uow.SaveChangesAsync();
        }

        public async Task<(bool Succeeded, string Error)> RevisePasswordAsync(UserEntity user, string newPassword)
        {
            user.F_Password = _securityService.GetSha256Hash(newPassword);
            user.F_SerialNumber = Guid.NewGuid().ToString("N"); // To force other logins to expire.
            await _uow.SaveChangesAsync();
            return (true, string.Empty);
        }

        public Task<int> SubmitForm<TDto>(UserEntity userEntity, TDto dto) where TDto : class
        {
            var service = _uow.GetRepository<UserEntity>();
            if (!string.IsNullOrEmpty(userEntity.F_Id))
            {
                userEntity.Modify(userEntity.F_Id);
                userEntity.F_LastModifyUserId = GetCurrentUserId();
                return service.UpdateAsync<TDto>(userEntity, dto);
            }
            else
            {
                userEntity.Create();
                userEntity.F_CreatorUserId = GetCurrentUserId();
                userEntity.F_Password = _securityService.GetSha256Hash(userEntity.F_Password);
                userEntity.F_SerialNumber = Guid.NewGuid().ToString("N"); 
                return service.InsertAsync(userEntity);
            }
        }
    }
}
