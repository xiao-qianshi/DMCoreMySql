using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IDialysisObservationApp : IScopedAppService
    {
        Task<List<DialysisObservationEntity>> GetList(Pagination pagination, string pid, DateTime startDate, DateTime endDate);
        IQueryable<DialysisObservationEntity> GetList();
        IQueryable<DialysisObservationEntity> GetList(string pid);
        IQueryable<DialysisObservationEntity> GetListByVisit(string visitId);
        Task<DialysisObservationEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(DialysisObservationEntity entity);
        Task<int> InsertForm(DialysisObservationEntity entity);
        Task<int> InsertForm(DialysisObservationEntity entity,string visitId);
        Task<int> SubmitForm<TDto>(DialysisObservationEntity entity, TDto dto) where TDto:class;
        Task<int> CopyForm(DialysisObservationEntity entity);
    }

    public class DialysisObservationApp : IDialysisObservationApp
    {
        private readonly IRepository<DialysisObservationEntity> _service = null;
        private IUnitOfWork _uow = null;
        private readonly IUsersService _usersService = null;

        public DialysisObservationApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<DialysisObservationEntity>();
            _usersService = usersService;
        }

        public Task<List<DialysisObservationEntity>> GetList(Pagination pagination, string pid, DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<DialysisObservationEntity>();
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_VisitDate >= startDate);
            expression = expression.And(t => t.F_VisitDate <= endDate);
            expression = expression.And(t => t.F_EnabledMark == true);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);

        }

        public IQueryable<DialysisObservationEntity> GetList()
        {
            var expression = ExtLinq.True<DialysisObservationEntity>();
            //expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_EnabledMark == true);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression);
        }

        public IQueryable<DialysisObservationEntity> GetList(string pid)
        {
            var expression = ExtLinq.True<DialysisObservationEntity>();
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_EnabledMark == true);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression);
        }

        public IQueryable<DialysisObservationEntity> GetListByVisit(string visitId)
        {
            var expression = ExtLinq.True<DialysisObservationEntity>();
            var visitRecord = _uow.GetRepository<PatVisitEntity>().FindEntity(visitId);
            if (visitRecord == null)
            {
                expression = expression.And(t => 1 == 0);
            }
            else
            {
                expression = expression.And(t => t.F_Pid == visitRecord.F_Pid);
                expression = expression.And(t => t.F_VisitDate == visitRecord.F_VisitDate);
            }
            expression = expression.And(t => t.F_EnabledMark == true);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression);
        }

        public Task<DialysisObservationEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(DialysisObservationEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> InsertForm(DialysisObservationEntity entity)
        {
            return _service.InsertAsync(entity);
        }

        public async Task<int> InsertForm(DialysisObservationEntity entity,string visitId)
        {
            var visitRecord = await _uow.GetRepository<PatVisitEntity>().FindEntityAsync(visitId);
            if (visitRecord == null) return 0;
            entity.Create();
           
            entity.F_Pid = visitRecord.F_Pid;
            entity.F_VisitDate = visitRecord.F_VisitDate;
            entity.F_VisitNo = visitRecord.F_VisitNo;
            if (!entity.F_NurseOperatorTime.HasValue)
            {
                entity.F_NurseOperatorTime =DateTime.Now;
            }

            var user = await _usersService.GetCurrentUserAsync();
            entity.F_Nurse = user.F_Id;
            entity.F_NurseName = user.F_RealName;
            entity.F_CreatorUserId = user.F_Id;
            entity.F_EnabledMark = true;
            return await _service.InsertAsync(entity);
        }

        public async Task<int> SubmitForm<TDto>(DialysisObservationEntity entity, TDto dto) where TDto:class
        {
            var userId = _usersService.GetCurrentUserId();
            var user = await _usersService.FindUserAsync(userId);
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = userId;
                return await _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = userId;
                entity.F_EnabledMark = true;
                entity.F_Nurse = userId;
                entity.F_NurseName = user.F_RealName;
                if (entity.F_NurseOperatorTime.HasValue)
                {
                    var date = entity.F_NurseOperatorTime.ToDate().Date;
                    //查询透析记录
                    var visitRecord = _uow.GetRepository<PatVisitEntity>().IQueryable()
                        .FirstOrDefault(t => t.F_Pid == entity.F_Pid && t.F_VisitDate == date);
                    entity.F_VisitDate = visitRecord?.F_VisitDate;
                    entity.F_VisitNo = visitRecord?.F_VisitNo;
                }

                return await _service.InsertAsync(entity);
            }
        }

        public async Task<int> CopyForm(DialysisObservationEntity entity)
        {
            var newEntity = new DialysisObservationEntity
            {
                F_A = entity.F_A,
                F_BF = entity.F_BF,
                F_C = entity.F_C,
                F_GSL = entity.F_GSL,
                F_HR = entity.F_HR,
                F_SSY = entity.F_SSY,
                F_SZY = entity.F_SZY,
                F_T = entity.F_T,
                F_TMP = entity.F_TMP,
                F_UFR = entity.F_UFR,
                F_UFV = entity.F_UFV,
                F_V = entity.F_V,
                F_VisitDate = entity.F_VisitDate,
                F_VisitNo = entity.F_VisitNo,
                F_NurseOperatorTime = DateTime.Now,
                F_Pid = entity.F_Pid
            };
            newEntity.Create();
            var user = await _usersService.GetCurrentUserAsync();
            newEntity.F_Nurse = user.F_Id;
            newEntity.F_NurseName = user.F_RealName;
            newEntity.F_CreatorUserId = user.F_Id;
            newEntity.F_EnabledMark = true;
            return await InsertForm(newEntity);
        }
    }
}
