using System;
using Dmt.DM.EntityFrameWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;

namespace Dmt.DM.Application
{
    public interface IDbInitializerService
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }

    public class DbInitializerService : IDbInitializerService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ISecurityService _securityService;

        public DbInitializerService(IServiceScopeFactory scopeFactory, ISecurityService securityService)
        {
            _scopeFactory = scopeFactory;
            _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<HsDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<HsDbContext>())
                {
                    // Add default roles
                    //var adminRole = new RoleEntity { Name = CustomRoles.Admin };
                    //var userRole = new RoleEntity { Name = CustomRoles.User };
                    //if (!context.RoleEntities.Any())
                    //{
                    //    context.Add(adminRole);
                    //    context.Add(userRole);
                    //    context.SaveChanges();
                    //}

                    // Add Admin user
                    //if (!context.UserEntities.Any())
                    //{
                    //    var adminUser = new UserEntity
                    //    {
                    //        F_Id = Common.GuId(),
                    //        F_Account = "admin",
                    //        F_RealName = "管理员",
                    //        F_IsActive = true,
                    //        F_LastLoggedIn = null,
                    //        F_Password = _securityService.GetSha256Hash("123"),
                    //        F_SerialNumber = Guid.NewGuid().ToString("N"),
                    //        F_IsAdministrator = true,
                    //        F_CreatorTime = DateTime.Now
                    //    };
                    //    context.Add(adminUser);
                    //    context.SaveChanges();

                    //    //context.Add(new UserRole { Role = adminRole, User = adminUser });
                    //    //context.Add(new UserRole { Role = userRole, User = adminUser });
                    //    //context.SaveChanges();
                    //}
                }
            }
        }
    }
}
