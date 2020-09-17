using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.LabLis;
using Dmt.DM.Mapper.ValueObject;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.LabLis
{
    public interface ILabItemApp : IScopedAppService
    {
        Task<List<LabItemEntity>> GetList(Pagination pagination, string queryJson);
        Task<List<LabItemSelectOptions>> GetSelectOptions(string keyValue);

        /// <summary>
        /// 更据关键字筛选
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IQueryable<LabItemEntity> GetList(string keyword = "");

        /// <summary>
        /// 根据类别查找
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<LabItemEntity>> GetListByType(string keyword);

        Task<LabItemEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(LabItemEntity entity);
        Task<int> SubmitForm<TDto>(LabItemEntity entity, TDto dto) where TDto : class;
    }

    public class LabItemApp : ILabItemApp
    {
        private readonly IRepository<LabItemEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IUsersService _usersService = null;
        private readonly IMemoryCache _memoryCache;

        public LabItemApp(IUnitOfWork uow, IUsersService usersService, IMemoryCache memoryCache)
        {
            _uow = uow;
            _service = _uow.GetRepository<LabItemEntity>();
            _usersService = usersService;
            _memoryCache = memoryCache;
        }
        public Task<List<LabItemEntity>> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<LabItemEntity>();

            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                var keyword = queryParam["keyword"]?.ToString();
                expression = expression.And(t => t.F_Code.Contains(keyword) || t.F_Name.Contains(keyword) || t.F_EnName.Contains(keyword) || t.F_ShortName.Contains(keyword));
            }
            if (!queryParam["timeType"].IsEmpty())
            {
                var timeType = queryParam["timeType"]?.ToString();
                expression = expression.And(t => t.F_Type == timeType);
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }


        public Task<List<LabItemSelectOptions>> GetSelectOptions(string keyValue)
        {
            List<LabItemSelectOptions> list;
            if (string.IsNullOrEmpty(keyValue))
            {
                if (_memoryCache.TryGetValue("labitem_select_options", out list)) return Task.FromResult(list);
                list = GetList().Select(item => new LabItemSelectOptions
                {
                    Id = item.F_Id,
                    Code = item.F_Code,
                    Container = item.F_Container,
                    CuvetteNo = item.F_CuvetteNo,
                    EnName = item.F_EnName,
                    IsExternal = item.F_IsExternal,
                    IsPeriodic = item.F_IsPeriodic,
                    Memo = item.F_Memo,
                    Name = item.F_Name,
                    Py = item.F_PY,
                    SampleType = item.F_SampleType,
                    ShortName = item.F_ShortName,
                    Sorter = item.F_Sorter,
                    ThirdPartyCode = item.F_ThirdPartyCode,
                    TimeInterval = item.F_TimeInterval,
                    Type = item.F_Type
                }).ToList();
                if (list.Any()) _memoryCache.Set("labitem_select_options", list, TimeSpan.FromMinutes(10));
            }
            else
            {
                list = GetList().Where(t => t.F_Id == keyValue).Select(item => new LabItemSelectOptions
                {
                    Id = item.F_Id,
                    Code = item.F_Code,
                    Container = item.F_Container,
                    CuvetteNo = item.F_CuvetteNo,
                    EnName = item.F_EnName,
                    IsExternal = item.F_IsExternal,
                    IsPeriodic = item.F_IsPeriodic,
                    Memo = item.F_Memo,
                    Name = item.F_Name,
                    Py = item.F_PY,
                    SampleType = item.F_SampleType,
                    ShortName = item.F_ShortName,
                    Sorter = item.F_Sorter,
                    ThirdPartyCode = item.F_ThirdPartyCode,
                    TimeInterval = item.F_TimeInterval,
                    Type = item.F_Type
                }).ToList(); ;
            }
            return Task.FromResult(list);
        }

        /// <summary>
        /// 更据关键字筛选
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IQueryable<LabItemEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<LabItemEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Code.Contains(keyword));
                expression = expression.Or(t => t.F_Name.Contains(keyword));
            }
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression).OrderBy(t => t.F_Name);
        }
        /// <summary>
        /// 根据类别查找
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Task<List<LabItemEntity>> GetListByType(string keyword)
        {
            var expression = ExtLinq.True<LabItemEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Type == keyword);
            }
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression).OrderBy(t => t.F_Name).ToListAsync();
        }

        public Task<LabItemEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            var entity = _service.FindEntity(keyValue);
            entity.F_DeleteMark = true;
            return UpdateForm(entity);
        }

        public Task<int> UpdateForm(LabItemEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(LabItemEntity entity, TDto dto) where TDto:class
        {
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
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }

    }
}
