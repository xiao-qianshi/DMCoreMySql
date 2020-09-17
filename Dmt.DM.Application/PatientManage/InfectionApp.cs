using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IInfectionApp : IScopedAppService
    {
        Task<List<InfectionEntity>> GetList(Pagination pagination, string keyword);
        Task<List<InfectionEntity>> GetList(Pagination pagination, DateTime startDate, DateTime endDate);
        Task<List<InfectionEntity>> GetList();
        Task<InfectionEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> InsertForm(InfectionEntity entity);
        Task<int> UpdateForm(InfectionEntity entity);
        Task<int> SubmitForm<TDto>(InfectionEntity entity, TDto dto) where TDto:class;
    }

    public class InfectionApp : IInfectionApp
    {
        private readonly IRepository<InfectionEntity> _service = null;
        private IUnitOfWork _uow = null;
        private readonly IUsersService _usersService = null;

        public InfectionApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<InfectionEntity>();
            _usersService = usersService;
        }

        public Task<List<InfectionEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<InfectionEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                //expression = expression.And(t => t.F_InfectionName.Contains(keyword));
                //expression = expression.Or(t => t.F_ItemSpec.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<List<InfectionEntity>> GetList(Pagination pagination, DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<InfectionEntity>();
            expression = expression.And(t => t.F_ReportDate >= startDate && t.F_ReportDate <= endDate);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<List<InfectionEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<InfectionEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(InfectionEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> InsertForm(InfectionEntity entity)
        {
            return _service.InsertAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(InfectionEntity entity, TDto dto) where TDto:class
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
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }
    }
}
