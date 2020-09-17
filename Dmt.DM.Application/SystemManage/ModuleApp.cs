using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Dmt.DM.Application.SystemManage
{
    public interface IModuleApp : IScopedAppService
    {
        Task<List<ModuleEntity>> GetList();
        Task<ModuleEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> SubmitForm(ModuleEntity moduleEntity, string keyValue);
        Task<int> SubmitForm<TDto>(ModuleEntity moduleEntity, TDto dto) where TDto : class;
    }

    public class ModuleApp : IModuleApp
    {
        private readonly IRepository<ModuleEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;

        public ModuleApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = uow.GetRepository<ModuleEntity>();
            _httpContext = httpContext;
        }

        public Task<List<ModuleEntity>> GetList()
        {
            return _service.IQueryable().OrderBy(t => t.F_SortCode).ToListAsync();
        }
        public Task<ModuleEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            if (_service.IQueryable().Count(t => t.F_ParentId==keyValue) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                return _service.DeleteAsync(t => t.F_Id == keyValue);
            }
        }
        public Task<int> SubmitForm(ModuleEntity moduleEntity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));

            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleEntity.Modify(keyValue);
                if (claim != null) moduleEntity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(moduleEntity);
            }
            else
            {
                moduleEntity.Create();
                if (claim != null) moduleEntity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(moduleEntity);
            }
        }

        public Task<int> SubmitForm<TDto>(ModuleEntity moduleEntity, TDto dto) where TDto : class
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));

            if (!string.IsNullOrEmpty(moduleEntity.F_Id))
            {
                moduleEntity.Modify(moduleEntity.F_Id);
                if (claim != null) moduleEntity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(moduleEntity, dto);
            }
            else
            {
                moduleEntity.Create();
                if (claim != null) moduleEntity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(moduleEntity);
            }
        }
    }
}
