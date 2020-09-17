using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.ValueObject;
using Dmt.DM.UOW;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IQualityItemApp : IScopedAppService
    {
        Task<List<QualityItemEntity>> GetList(Pagination pagination, string itemType=null,string keyword=null);
        Task<IEnumerable<QualityItemSelectOptions>> GetList(string keyword = "");
        Task<IEnumerable<QualityItemSelectOptions>> GetPartitedList();
        IEnumerable<QualityItemPartitionEntity> GetPartitionList(string parentId);
        Task<QualityItemEntity> GetFormByCode(string keyValue);
        Task<QualityItemEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(QualityItemEntity entity);
        Task<int> SubmitForm<TDto>(QualityItemEntity entity,List<QualityItemPartitionEntity> partitionEntities, TDto dto) where TDto : class;
    }

    public class QualityItemApp : IQualityItemApp
    {
        private readonly IRepository<QualityItemEntity> _service = null;
        private readonly IRepository<QualityItemPartitionEntity> _partitionService = null;
        private IUnitOfWork _uow = null;
        private IUsersService _userService = null;
        private readonly IMemoryCache _memoryCache = null;

        public QualityItemApp(IUnitOfWork uow, IUsersService userService, IMemoryCache memoryCache)
        {
            _uow = uow;
            _service = _uow.GetRepository<QualityItemEntity>();
            _partitionService = _uow.GetRepository<QualityItemPartitionEntity>();
            _userService = userService;
            _memoryCache = memoryCache;
        }

        public Task<List<QualityItemEntity>> GetList(Pagination pagination, string itemType=null,string keyword=null)
        {
            var expression = ExtLinq.True<QualityItemEntity>();
            if (!string.IsNullOrWhiteSpace(itemType)) expression = expression.And(t => t.F_ItemType == itemType);
            if (!string.IsNullOrWhiteSpace(keyword)) expression = expression.And(t => t.F_HisItemCode.Contains(keyword) || t.F_ItemCode.Contains(keyword) || t.F_ItemName.Contains(keyword));
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        
        public Task<IEnumerable<QualityItemSelectOptions>> GetList(string keyword = "")
        {
            if (_memoryCache.TryGetValue("qualityitem_select_options", out List<QualityItemSelectOptions> cacheData))
                return string.IsNullOrEmpty(keyword)
                    ? Task.FromResult(cacheData.AsEnumerable())
                    : Task.FromResult(cacheData.Where(t =>
                        t.HisItemCode.Contains(keyword) || t.ItemCode.Contains(keyword) ||
                        t.ItemName.Contains(keyword)));
            {
                var expression = ExtLinq.True<QualityItemEntity>();
                expression = expression.And(t => t.F_EnabledMark == true);
                expression = expression.And(t => t.F_DeleteMark != true);
                cacheData = _service.IQueryable(expression).OrderBy(t => t.F_ItemCode).Select(r =>
                    new QualityItemSelectOptions
                    {
                        Id = r.F_Id,
                        ItemType = r.F_ItemType,
                        OrderNo = r.F_OrderNo,
                        ItemCode = r.F_ItemCode,
                        ItemName = r.F_ItemName,
                        HisItemCode = r.F_HisItemCode,
                        Unit = r.F_Unit,
                        Result = r.F_Result,
                        LowerValue = r.F_LowerValue,
                        UpperValue = r.F_UpperValue,
                        LowerCriticalValue = r.F_LowerCriticalValue,
                        UpperCriticalValue = r.F_UpperCriticalValue,
                        ResultType = r.F_ResultType,
                        Memo = r.F_Memo
                    }).ToList();
                _memoryCache.Set("qualityitem_select_options", cacheData, TimeSpan.FromMinutes(5));
            }

            return string.IsNullOrEmpty(keyword) ? Task.FromResult(cacheData.AsEnumerable()) : Task.FromResult(cacheData.Where(t =>
                t.HisItemCode.Contains(keyword) || t.ItemCode.Contains(keyword) || t.ItemName.Contains(keyword)));
        }

        public async Task<IEnumerable<QualityItemSelectOptions>> GetPartitedList()
        {
            var list = (await GetList(""))
                .Join(_partitionService.IQueryable().Select(r => new { r.F_ParentId }).Distinct(), i => i.Id, p => p.F_ParentId, (i, p) => i)
                .ToList();
            return list;
        }

        public IEnumerable<QualityItemPartitionEntity> GetPartitionList(string parentId)
        {
            var expression = ExtLinq.True<QualityItemPartitionEntity>();
            expression = expression.And(t => t.F_ParentId == parentId);
            return _partitionService.IQueryable(expression);
        }

        public Task<QualityItemEntity> GetFormByCode(string keyValue)
        {
            return _service.FindEntityAsync(t => t.F_ItemCode == keyValue);
        }

        public Task<QualityItemEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            var entity = _service.FindEntity(keyValue);
            entity.F_DeleteMark = true;
            return UpdateForm(entity);
        }

        public Task<int> UpdateForm(QualityItemEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public async Task<int> SubmitForm<TDto>(QualityItemEntity entity,List<QualityItemPartitionEntity> partitionEntities, TDto dto) where TDto : class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = _userService.GetCurrentUserId();
                await _service.UpdateAsync(entity, dto);
            }
            else
            {
                if (entity.F_EnabledMark == null)
                {
                    entity.F_EnabledMark = true;
                }
                if (entity.F_OrderNo == null)
                {
                    entity.F_OrderNo = 0;
                }
                entity.Create();
                entity.F_CreatorUserId = _userService.GetCurrentUserId();
                await _service.InsertAsync(entity);
            }

            return await UpdatePartitions(entity.F_Id, partitionEntities);
        }

        private async Task<int> UpdatePartitions(string parentId, List<QualityItemPartitionEntity> list)
        {
            var parentEntity = await _service.FindEntityAsync(parentId);
            if (parentEntity.F_ResultType != true) return 0;
            await _partitionService.DeleteAsync(t => t.F_ParentId == parentId);
            var userId = _userService.GetCurrentUserId();
            foreach (var item in list)
            {
                item.Create();
                item.F_ParentId = parentId;
                item.F_CreatorUserId = userId;
            }
            return await _partitionService.InsertAsync(list);
        }
    }
}
