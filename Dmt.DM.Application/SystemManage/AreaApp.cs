using Dmt.DM.Domain.Entity.SystemManage;
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
    public interface IAreaApp : IScopedAppService
    {
        Task<List<AreaEntity>> GetList();
        Task<AreaEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> SubmitForm(AreaEntity areaEntity, string keyValue);
        Task<int> SubmitForm<TDto>(AreaEntity entity, TDto dto) where TDto : class;
    }

    public class AreaApp : IAreaApp
    {
        private IRepository<AreaEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public AreaApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<AreaEntity>();
            _httpContext = httpContext;
        }
        public Task<List<AreaEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }
        public Task<AreaEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            if (_service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                return _service.DeleteAsync(t => t.F_Id == keyValue);
            }
        }
        public Task<int> SubmitForm(AreaEntity areaEntity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.Modify(keyValue);
                areaEntity.F_LastModifyUserId = claim?.Value;
                return _service.UpdateAsync(areaEntity);
            }
            else
            {
                areaEntity.Create();
                areaEntity.F_CreatorUserId = claim?.Value;
                return _service.InsertAsync(areaEntity);
            }
        }

        public Task<int> SubmitForm<TDto>(AreaEntity entity, TDto dto) where TDto : class
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = claim?.Value;
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = claim?.Value;
                return _service.InsertAsync(entity);
            }
        }
    }
}
