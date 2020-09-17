using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.UOW;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Dmt.DM.Application.SystemManage
{
    public interface IItemsDetailApp : IScopedAppService
    {
        Task<List<ItemsDetailEntity>> GetList(string itemId = "", string keyword = "");
        Task<List<ItemsDetailEntity>> GetItemList(string enCode);

        /// <summary>
        /// 查找默认透析方式
        /// </summary>
        /// <returns></returns>
        Task<string> GetDefaultDialysisType();

        Task<ItemsDetailEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue);
        Task<int> SubmitForm<TDto>(ItemsDetailEntity entity, TDto dto) where TDto : class;
    }

    public class ItemsDetailApp : IItemsDetailApp
    {
        private readonly IRepository<ItemsDetailEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public ItemsDetailApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<ItemsDetailEntity>();
            _httpContext = httpContext;
        }

        public Task<List<ItemsDetailEntity>> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ItemsDetailEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ItemName.Contains(keyword));
                expression = expression.Or(t => t.F_ItemCode.Contains(keyword));
            }
            return _service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToListAsync();
        }

        public Task<List<ItemsDetailEntity>> GetItemList(string enCode)
        {
            var exp = ExtLinq.True<ItemsEntity>();
            exp = exp.And(t => t.F_EnCode == enCode);
            exp = exp.And(t => t.F_EnabledMark != false);
            var item = _uow.GetRepository<ItemsEntity>().FindEntity(exp);
            var id = item.F_Id;
            var expression = ExtLinq.True<ItemsDetailEntity>();
            expression = expression.And(t => t.F_ItemId == id);
            expression = expression.And(t => t.F_EnabledMark != false);
            return _service.IQueryable(expression).OrderBy(t => t.F_ItemCode).ToListAsync();
        }
        /// <summary>
        /// 查找默认透析方式
        /// </summary>
        /// <returns></returns>
        public Task<string> GetDefaultDialysisType()
        {
            var exp = ExtLinq.True<ItemsEntity>();
            exp = exp.And(t => t.F_EnCode == "DialysisType");
            exp = exp.And(t => t.F_EnabledMark != false);
            var item = _uow.GetRepository<ItemsEntity>().FindEntity(exp);
            var id = item.F_Id;
            var expression = ExtLinq.True<ItemsDetailEntity>();
            expression = expression.And(t => t.F_ItemId == id);
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_IsDefault == true);
            return _service.IQueryable(expression).Select(t => t.F_ItemCode).FirstOrDefaultAsync();
        }

        public Task<ItemsDetailEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }
        public Task<int> SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsDetailEntity.Modify(keyValue);
                if (claim != null) itemsDetailEntity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(itemsDetailEntity);
            }
            else
            {
                itemsDetailEntity.Create();
                if (claim != null) itemsDetailEntity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(itemsDetailEntity);
            }
        }

        public Task<int> SubmitForm<TDto>(ItemsDetailEntity entity, TDto dto) where TDto : class
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                if (claim != null) entity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                if (claim != null) entity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(entity);
            }
        }
    }
}
