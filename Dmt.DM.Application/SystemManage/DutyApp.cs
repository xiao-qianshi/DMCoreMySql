using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dmt.DM.Application.SystemManage
{
    public interface IDutyApp : IScopedAppService
    {
        Task<List<RoleEntity>> GetList(string keyword = "");
        Task<RoleEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> SubmitForm(RoleEntity roleEntity, string keyValue);
        Task<int> SubmitForm<TDto>(RoleEntity entity, TDto dto) where TDto : class;
    }

    public class DutyApp : IDutyApp
    {
        private IRepository<RoleEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public DutyApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<RoleEntity>();
            _httpContext = httpContext;
        }

        public Task<List<RoleEntity>> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 2);
            return _service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToListAsync();
        }
        public Task<RoleEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }
        public Task<int> SubmitForm(RoleEntity roleEntity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));

            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Modify(keyValue);
                if (claim != null) roleEntity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(roleEntity);
            }
            else
            {
                roleEntity.Create();
                roleEntity.F_Category = 2;
                if (claim != null) roleEntity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(roleEntity);
            }
        }

        public Task<int> SubmitForm<TDto>(RoleEntity entity, TDto dto) where TDto : class
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));

            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                if (claim != null) entity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_Category = 2;
                if (claim != null) entity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(entity);
            }
        }
    }
}
