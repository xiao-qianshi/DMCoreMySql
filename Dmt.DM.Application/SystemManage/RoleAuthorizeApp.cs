using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.Domain.ViewModel;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmt.DM.Application.SystemManage
{
    public interface IRoleAuthorizeApp : IScopedAppService
    {
        Task<List<RoleAuthorizeEntity>> GetList(string objectId);
        Task<List<ModuleEntity>> GetMenuList(string roleId);
        Task<List<ModuleButtonEntity>> GetButtonList(string roleId);
        Task<bool> ActionValidate(string roleId, string moduleId, string action);
    }

    public class RoleAuthorizeApp : IRoleAuthorizeApp
    {
        public readonly IRepository<RoleAuthorizeEntity> _service = null;
        private readonly ModuleApp _moduleApp = null;
        private readonly ModuleButtonApp _moduleButtonApp = null;

        private readonly IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;
        private readonly IMemoryCache _memoryCache = null;

        public RoleAuthorizeApp(IUnitOfWork uow, IHttpContextAccessor httpContext, IMemoryCache memoryCache)
        {
            _uow = uow;
            _service = uow.GetRepository<RoleAuthorizeEntity>();
            _httpContext = httpContext;
            _moduleApp = new ModuleApp(uow, httpContext);
            _moduleButtonApp = new ModuleButtonApp(uow, httpContext);
        }

        public Task<List<RoleAuthorizeEntity>> GetList(string objectId)
        {
            return _service.IQueryable(t => t.F_ObjectId == objectId).ToListAsync();
        }
        public async Task<List<ModuleEntity>> GetMenuList(string roleId)
        {
            var user = _httpContext.HttpContext.User;
            user.CheckArgumentIsNull(nameof(user));
            var claim = user.FindFirst(t => t.Type == "IsAdmin");
            claim.CheckArgumentIsNull(nameof(claim));
            var data = new List<ModuleEntity>();
            if (claim.Value.Equals("true",StringComparison.InvariantCultureIgnoreCase))
            {
                data = await _moduleApp.GetList();
            }
            else
            {
                var moduledata = await _moduleApp.GetList();
                var authorizedata = await _service.IQueryable(t => t.F_ObjectId == roleId && t.F_ItemType == 1).ToListAsync();
                foreach (var item in authorizedata)
                {
                    var moduleEntity = moduledata.Find(t => t.F_Id == item.F_ItemId);
                    if (moduleEntity != null)
                    {
                        data.Add(moduleEntity);
                    }
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }
        public async Task<List<ModuleButtonEntity>> GetButtonList(string roleId)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type == "IsAdmin");
            claim.CheckArgumentIsNull(nameof(claim));
            var data = new List<ModuleButtonEntity>();
            if (claim.Value.Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                data = await _moduleButtonApp.GetList();
            }
            else
            {
                var buttondata = await _moduleButtonApp.GetList();
                var authorizedata = await _service.IQueryable(t => t.F_ObjectId == roleId && t.F_ItemType == 2).ToListAsync();
                foreach (var item in authorizedata)
                {
                    ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.F_Id == item.F_ItemId);
                    if (moduleButtonEntity != null)
                    {
                        data.Add(moduleButtonEntity);
                    }
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }
        public async Task<bool> ActionValidate(string roleId, string moduleId, string action)
        {
            var authorizeurldata = new List<AuthorizeActionModel>();
            //var cachedata = CacheFactory.Cache().GetCache<List<AuthorizeActionModel>>("authorizeurldata_" + roleId);
            _memoryCache.TryGetValue<List<AuthorizeActionModel>>("authorizeurldata_" + roleId,
                out List<AuthorizeActionModel> cachedata);
            if (cachedata == null)
            {
                var moduledata = await _moduleApp.GetList();
                var buttondata = await _moduleButtonApp.GetList();
                var authorizedata = await _service.IQueryable(t => t.F_ObjectId == roleId).ToListAsync();
                foreach (var item in authorizedata)
                {
                    if (item.F_ItemType == 1)
                    {
                        ModuleEntity moduleEntity = moduledata.Find(t => t.F_Id == item.F_ItemId);
                        authorizeurldata.Add(new AuthorizeActionModel { F_Id = moduleEntity.F_Id, F_UrlAddress = moduleEntity.F_UrlAddress });
                    }
                    else if (item.F_ItemType == 2)
                    {
                        ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.F_Id == item.F_ItemId);
                        authorizeurldata.Add(new AuthorizeActionModel { F_Id = moduleButtonEntity.F_ModuleId, F_UrlAddress = moduleButtonEntity.F_UrlAddress });
                    }
                }
                //CacheFactory.Cache().WriteCache(authorizeurldata, "authorizeurldata_" + roleId, DateTime.Now.AddMinutes(5));
                _memoryCache.Set<List<AuthorizeActionModel>>("authorizeurldata_" + roleId, cachedata,
                    TimeSpan.FromMinutes(20));
            }
            else
            {
                authorizeurldata = cachedata;
            }
            authorizeurldata = authorizeurldata.FindAll(t => t.F_Id == moduleId);
            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.F_UrlAddress))
                {
                    string[] url = item.F_UrlAddress.Split('?');
                    if (item.F_Id == moduleId && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
