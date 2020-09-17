using Dmt.DM.EntityFrameWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;

namespace Dmt.DM.Application
{
    public interface IRolesService
    {
        //Task<List<RoleEntity>> FindUserRolesAsync(int userId);
        //Task<bool> IsUserInRoleAsync(int userId, string roleName);
        //Task<List<UserEntity>> FindUsersInRoleAsync(string roleName);
        Task<RoleEntity> FindUserRoleAsync(string roleId);
    }

    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork<HsDbContext> _uow;
        private readonly DbSet<RoleEntity> _roles;
        //private readonly DbSet<UserEntity> _users;

        public RolesService(IUnitOfWork<HsDbContext> uow)
        {
            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));
            _roles = _uow.Set<RoleEntity>();
            //_users = _uow.Set<UserEntity>();
        }

        //public Task<List<Role>> FindUserRolesAsync(int userId)
        //{
        //    var userRolesQuery = from role in _roles
        //                         from userRoles in role.UserRoles
        //                         where userRoles.UserId == userId
        //                         select role;

        //    return userRolesQuery.OrderBy(x => x.Name).ToListAsync();
        //}

        //public async Task<bool> IsUserInRoleAsync(int userId, string roleName)
        //{
        //    var userRolesQuery = from role in _roles
        //                         where role.Name == roleName
        //                         from user in role.UserRoles
        //                         where user.UserId == userId
        //                         select role;
        //    var userRole = await userRolesQuery.FirstOrDefaultAsync();
        //    return userRole != null;
        //}

        //public Task<List<User>> FindUsersInRoleAsync(string roleName)
        //{
        //    var roleUserIdsQuery = from role in _roles
        //                           where role.Name == roleName
        //                           from user in role.UserRoles
        //                           select user.UserId;
        //    return _users.Where(user => roleUserIdsQuery.Contains(user.Id))
        //                 .ToListAsync();
        //}
        public Task<RoleEntity> FindUserRoleAsync(string roleId)
        {
            return _roles.FirstOrDefaultAsync(t => t.F_Id.Equals(roleId));
        }
    }
}
