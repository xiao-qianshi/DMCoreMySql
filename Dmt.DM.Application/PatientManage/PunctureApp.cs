using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IPunctureApp : IScopedAppService
    {
        Task<List<PunctureEntity>> GetList(Pagination pagination, string keyword);
        Task<List<PunctureEntity>> GetList();
        Task<List<PunctureEntity>> GetList(string keyword = "", int count = 10);
        Task<PunctureEntity> GetSingle(string pid, DateTime visitDate);
        Task<PunctureEntity> GetForm(string keyValue);
        Task<int> InsertForm(PunctureEntity entity);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(PunctureEntity entity);
        Task<int> SubmitForm<TDto>(PunctureEntity entity, TDto dto) where TDto : class;
        Task<List<PunctureEntity>> GetListByDateRange(Pagination pagination, string pid, DateTime? startDate, DateTime? endDate);
    }

    public class PunctureApp : IPunctureApp
    {
        private readonly IRepository<PunctureEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IUsersService _usersService = null;

        public PunctureApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<PunctureEntity>();
            _usersService = usersService;
        }

        public Task<List<PunctureEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<PunctureEntity>();
            expression = !string.IsNullOrEmpty(keyword) ? expression.And(t => t.F_Pid == keyword) : expression.And(t => t.F_Pid == "");
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public Task<List<PunctureEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<List<PunctureEntity>> GetList(string keyword = "", int count = 10)
        {
            var expression = ExtLinq.True<PunctureEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Pid == keyword);
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).OrderByDescending(t => t.F_OperateTime).Take(count).ToListAsync();
        }

        public Task<PunctureEntity> GetSingle(string pid, DateTime visitDate)
        {
            var startDate = visitDate.Date;
            var endDate = visitDate.Date.AddDays(1);
            var expression = ExtLinq.True<PunctureEntity>();
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_OperateTime >= startDate && t.F_OperateTime <= endDate);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).OrderByDescending(t => t.F_OperateTime).FirstOrDefaultAsync();
        }

        public Task<PunctureEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }

        public Task<int> InsertForm(PunctureEntity entity)
        {
            entity.Create();
            entity.F_CreatorUserId = _usersService.GetCurrentUserId();
            return _service.InsertAsync(entity);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            //_service.Delete(t => t.F_Id == keyValue);
            var entity = _service.FindEntity(keyValue);
            entity.F_DeleteMark = true;
            return UpdateForm(entity);
        }

        public Task<int> UpdateForm(PunctureEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(PunctureEntity entity, TDto dto) where TDto : class
        {
            var userId = _usersService.GetCurrentUserId();
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = userId;
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                if (entity.F_EnabledMark == null)
                {
                    entity.F_EnabledMark = true;
                }
                entity.F_CreatorUserId = userId;
                entity.F_Nurse = userId;
                return _service.InsertAsync(entity);
            }
        }
        public Task<List<PunctureEntity>> GetListByDateRange(Pagination pagination, string pid, DateTime? startDate, DateTime? endDate)
        {
            var expression = ExtLinq.True<PunctureEntity>();
            if (!string.IsNullOrEmpty(pid))
            {
                expression = expression.And(t => t.F_Pid == pid);
                if (startDate != null)
                {
                    var date = startDate.ToDate();
                    expression = expression.And(t => t.F_OperateTime >= date);
                }
                if (endDate != null)
                {
                    var date = endDate.ToDate();
                    expression = expression.And(t => t.F_OperateTime <= date);
                }

            }
            else
            {
                expression = expression.And(t => t.F_Pid == "");
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
    }
}
