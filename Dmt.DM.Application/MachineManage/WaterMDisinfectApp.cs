using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dmt.DM.Application.MachineManage
{
    public interface IWaterMDisinfectApp : IScopedAppService
    {
        Task<List<WaterMDisinfectEntity>> GetList(Pagination pagination, string keyword);
        Task<List<WaterMDisinfectEntity>> GetList(Pagination pagination, DateTime startDate, DateTime endDate);
        Task<List<WaterMDisinfectEntity>> GetList();
        Task<WaterMDisinfectEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(WaterMDisinfectEntity entity);
        Task<int> InsertForm(WaterMDisinfectEntity entity);
        Task<int> SubmitForm<TDto>(WaterMDisinfectEntity entity, TDto dto) where TDto : class;
        Task<int> SubmitFormBatch(List<WaterMDisinfectEntity> list);
    }

    public class WaterMDisinfectApp : IWaterMDisinfectApp
    {
        private readonly IRepository<WaterMDisinfectEntity> _service;
        private readonly IUnitOfWork _uow;
        private readonly IUsersService _usersService;

        public WaterMDisinfectApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = uow.GetRepository<WaterMDisinfectEntity>();
            _usersService = usersService;
        }

        public Task<List<WaterMDisinfectEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<WaterMDisinfectEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                var json = keyword.ToJObject();
                var startDateStr = json.Value<string>("startDate");
                var endDateStr = json.Value<string>("endDate");
                if (!string.IsNullOrEmpty(startDateStr))
                {
                    if (DateTime.TryParse(startDateStr, out DateTime temp))
                    {
                        expression = expression.And(t => t.F_DisinfectDate >= temp);
                    }
                }
                if (!string.IsNullOrEmpty(endDateStr))
                {
                    if (DateTime.TryParse(endDateStr, out DateTime temp))
                    {
                        expression = expression.And(t => t.F_DisinfectDate <= temp);
                    }
                }
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<List<WaterMDisinfectEntity>> GetList(Pagination pagination, DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<WaterMDisinfectEntity>();
            expression = expression.And(t => t.F_DisinfectDate >= startDate).And(t => t.F_DisinfectDate <= endDate);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<List<WaterMDisinfectEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<WaterMDisinfectEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(WaterMDisinfectEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> InsertForm(WaterMDisinfectEntity entity)
        {
            return _service.InsertAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(WaterMDisinfectEntity entity, TDto dto) where TDto : class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                return _service.InsertAsync(entity);
            }
        }

        public Task<int> SubmitFormBatch(List<WaterMDisinfectEntity> list)
        {
            foreach (var entity in list)
            {
                entity.Create();
                entity.F_EnabledMark = true;
            }
            return _service.InsertAsync(list);
        }
    }
}
