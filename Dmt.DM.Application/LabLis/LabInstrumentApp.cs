using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.LabLis;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.LabLis
{
    public interface ILabInstrumentApp : IScopedAppService
    {
        /// <summary>
        /// 查询仪器列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        Task<List<LabInstrumentEntity>> GetList(Pagination pagination, string queryJson);

        /// <summary>
        /// 更据关键字筛选
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IQueryable<LabInstrumentEntity> GetList(string keyword = "");

        /// <summary>
        /// 查询某一仪器检验项目
        /// </summary>
        /// <param name="instrumentId">ID主键</param>
        /// <returns></returns>
        IEnumerable<LabInstrumentItemEntity> GetItem(string instrumentId);

        /// <summary>
        /// 查询单一子项
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        Task<LabInstrumentItemEntity> GetSingleItem(string keyValue);

        /// <summary>
        /// 删除子项
        /// </summary>
        /// <param name="keyValue"></param>
        Task<int> DeleteItem(string keyValue);

        /// <summary>
        /// 根据类别查找
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<LabInstrumentEntity>> GetListByType(string keyword);

        Task<LabInstrumentEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(LabInstrumentEntity entity);
        Task<int> SubmitForm<TDto>(LabInstrumentEntity entity, TDto dto) where TDto : class;
        Task<LabInstrumentItemEntity> SubmitItemForm<TDto>(LabInstrumentItemEntity entity, TDto dto) where TDto : class;
    }

    public class LabInstrumentApp : ILabInstrumentApp
    {
        private readonly IRepository<LabInstrumentEntity> _service = null;
        private readonly IRepository<LabInstrumentItemEntity> _itemService = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IUsersService _usersService = null;

        public LabInstrumentApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<LabInstrumentEntity>();
            _itemService = _uow.GetRepository<LabInstrumentItemEntity>();
            _usersService = usersService;
        }

        /// <summary>
        /// 查询仪器列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public Task<List<LabInstrumentEntity>> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<LabInstrumentEntity>();

            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                var keyword = queryParam["keyword"]?.ToString();
                expression = expression.And(t => t.F_Code.Contains(keyword) || t.F_Name.Contains(keyword) || t.F_ShortName.Contains(keyword));
            }
            if (!queryParam["timeType"].IsEmpty())
            {
                var timeType = queryParam["timeType"]?.ToString();
                expression = expression.And(t => t.F_TestType == timeType);
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        /// <summary>
        /// 更据关键字筛选
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IQueryable<LabInstrumentEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<LabInstrumentEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Code.Contains(keyword));
                expression = expression.Or(t => t.F_Name.Contains(keyword));
            }
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression).OrderBy(t => t.F_TestType);
        }

        /// <summary>
        /// 查询某一仪器检验项目
        /// </summary>
        /// <param name="instrumentId">ID主键</param>
        /// <returns></returns>
        public IEnumerable<LabInstrumentItemEntity> GetItem(string instrumentId)
        {
            //ILabInstrumentItemRepository _itemService = new LabInstrumentItemRepository();
            var expression = ExtLinq.True<LabInstrumentItemEntity>();
            expression = expression.And(t => t.F_MachineId == instrumentId);
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _itemService.IQueryable(expression);
        }
        /// <summary>
        /// 查询单一子项
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public async Task<LabInstrumentItemEntity> GetSingleItem(string keyValue)
        {
            var itemEntity = await _itemService.FindEntityAsync(keyValue);
            return itemEntity ?? new LabInstrumentItemEntity();
        }

        /// <summary>
        /// 删除子项
        /// </summary>
        /// <param name="keyValue"></param>
        public Task<int> DeleteItem(string keyValue)
        {
            var itemEntity = _itemService.FindEntity(keyValue);
            if (itemEntity != null)
            {
                //itemEntity.Remove();
                //_itemService.Update(itemEntity, true);
                return _itemService.DeleteAsync(itemEntity);
            }

            return Task.FromResult(0);
        }


        /// <summary>
        /// 根据类别查找
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Task<List<LabInstrumentEntity>> GetListByType(string keyword)
        {
            var expression = ExtLinq.True<LabInstrumentEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_TestType == keyword);
            }
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression).OrderBy(t => t.F_Name).ToListAsync();
        }

        public Task<LabInstrumentEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            var entity = _service.FindEntity(keyValue);
            entity.F_DeleteMark = true;
            return UpdateForm(entity);
        }

        public Task<int> UpdateForm(LabInstrumentEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(LabInstrumentEntity entity, TDto dto) where TDto : class
        {
            //var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            //claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            //var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                if (!entity.F_EnabledMark.HasValue)
                {
                    entity.F_EnabledMark = true;
                }
                if (!entity.F_Sorter.HasValue)
                {
                    entity.F_Sorter = 0;
                }
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }

        public async Task<LabInstrumentItemEntity> SubmitItemForm<TDto>(LabInstrumentItemEntity entity, TDto dto) where TDto:class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                await _itemService.UpdateAsync(entity, dto);
            }
            else
            {
                if (!entity.F_EnabledMark.HasValue)
                {
                    entity.F_EnabledMark = true;
                }
                //if (!entity.F_ResultType.HasValue)
                //{
                //    entity.F_ResultType = true;
                //}
                if (!entity.F_Sorter.HasValue)
                {
                    entity.F_Sorter = 0;
                }
                //if (!entity.F_KeepDecimal.HasValue)
                //{
                //    entity.F_KeepDecimal = 2;
                //}
                if (!entity.F_IsQualityItem.HasValue)
                {
                    entity.F_IsQualityItem = false;
                }
                if (!entity.F_ConvertCoefficient.HasValue)
                {
                    entity.F_ConvertCoefficient = 1;
                }
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                await _itemService.InsertAsync(entity);
            }
            return entity;
        }
    }
}
