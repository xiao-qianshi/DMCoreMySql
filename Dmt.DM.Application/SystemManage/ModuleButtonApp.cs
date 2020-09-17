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

namespace Dmt.DM.Application.SystemManage
{
    public interface IModuleButtonApp : IScopedAppService
    {
        Task<List<ModuleButtonEntity>> GetList(string moduleId = "");
        Task<ModuleButtonEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> SubmitForm(ModuleButtonEntity moduleButtonEntity, string keyValue);
        Task<int> SubmitForm<TDto>(ModuleButtonEntity moduleButtonEntity, TDto dto) where TDto: class;
        Task SubmitCloneButton(string moduleId, string ids);
    }

    public class ModuleButtonApp : IModuleButtonApp
    {
        private readonly IRepository<ModuleButtonEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;

        public ModuleButtonApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = uow.GetRepository<ModuleButtonEntity>();
            _httpContext = httpContext;
        }

        public Task<List<ModuleButtonEntity>> GetList(string moduleId = "")
        {
            var expression = ExtLinq.True<ModuleButtonEntity>();
            if (!string.IsNullOrEmpty(moduleId))
            {
                expression = expression.And(t => t.F_ModuleId == moduleId);
            }
            return _service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToListAsync();
        }
        public Task<ModuleButtonEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            if (_service.IQueryable().Count(t => t.F_ParentId == keyValue) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                return _service.DeleteAsync(t => t.F_Id == keyValue);
            }
        }
        public Task<int> SubmitForm(ModuleButtonEntity moduleButtonEntity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleButtonEntity.Modify(keyValue);
                if (claim != null) moduleButtonEntity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(moduleButtonEntity);
            }
            else
            {
                moduleButtonEntity.Create();
                if (claim != null) moduleButtonEntity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(moduleButtonEntity);
            }
        }

        public Task<int> SubmitForm<TDto>(ModuleButtonEntity moduleButtonEntity, TDto dto) where TDto:class
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(moduleButtonEntity.F_Id))
            {
                moduleButtonEntity.Modify(moduleButtonEntity.F_Id);
                if (claim != null) moduleButtonEntity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(moduleButtonEntity, dto);
            }
            else
            {
                moduleButtonEntity.Create();
                if (claim != null) moduleButtonEntity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(moduleButtonEntity);
            }
        }

        public async Task SubmitCloneButton(string moduleId, string Ids)
        {
            var ArrayId = Ids.Split(',');
            var data = await this.GetList();
            var entitys = new List<ModuleButtonEntity>();
            foreach (var item in ArrayId)
            {
                var moduleButtonEntity = data.FirstOrDefault(t => t.F_Id == item);
                if (moduleButtonEntity == null) continue;
                moduleButtonEntity.F_Id = Common.GuId();
                moduleButtonEntity.F_ModuleId = moduleId;
                entitys.Add(moduleButtonEntity);
            }
            if (entitys.Count > 0)
            {
                //_service.BeginTrans();
                foreach (var itemEntity in entitys)
                {
                    await _service.InsertAsync(itemEntity, false);
                }
                //_service.CommitTrans();
            }
        }
    }
}
