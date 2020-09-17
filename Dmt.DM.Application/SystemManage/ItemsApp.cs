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
    public interface IItemsApp: IScopedAppService
    {
        Task<List<ItemsEntity>> GetList();
        Task<ItemsEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> SubmitForm(ItemsEntity itemsEntity, string keyValue);
        Task<int> SubmitForm<TDto>(ItemsEntity itemsEntity, TDto dto) where TDto : class;
    }

    public class ItemsApp : IItemsApp
    {
        private readonly IRepository<ItemsEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public ItemsApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<ItemsEntity>();
            _httpContext = httpContext;
        }

        public Task<List<ItemsEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }
        public Task<ItemsEntity> GetForm(string keyValue)
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

        public Task<int> SubmitForm(ItemsEntity itemsEntity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsEntity.Modify(keyValue);
                if (claim != null) itemsEntity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(itemsEntity);
            }
            else
            {
                itemsEntity.Create();
                if (claim != null) itemsEntity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(itemsEntity);
            }
        }

        public Task<int> SubmitForm<TDto>(ItemsEntity itemsEntity, TDto dto) where TDto : class
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(itemsEntity.F_Id))
            {
                itemsEntity.Modify(itemsEntity.F_Id);
                if (claim != null) itemsEntity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(itemsEntity, dto);
            }
            else
            {
                itemsEntity.Create();
                if (claim != null) itemsEntity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(itemsEntity);
            }
        }
    }
}
