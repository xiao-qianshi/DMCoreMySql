using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Domain.Entity.ReportPrint.Evaluation;
using Dmt.DM.UOW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IEvaluationApp : IScopedAppService
    {
        Task<EvaluationEntity> GetForm(string keyValue);
        Task<EvaluationEntity> GetForm(string pid, DateTime visitDate);
        Task<int> UpdateForm(EvaluationEntity entity);
        Task<int> DeleteForm(EvaluationEntity entity);
        Task<int> DeleteForm(string keyValue);
        Task<int> SubmitForm<TDto>(EvaluationEntity entity, TDto dto) where TDto : class;
        Task<string> GetEvaluationReport(string keyValue);
    }

    public class EvaluationApp : IEvaluationApp
    {
        private readonly IRepository<EvaluationEntity> _service = null;
        private readonly IUsersService _usersService = null;
        private readonly IUnitOfWork _uow = null;

        public EvaluationApp(IUnitOfWork uow, IUsersService usersService)
        {
            _usersService = usersService;
            _uow = uow;
            _service = _uow.GetRepository<EvaluationEntity>();
        }

        public Task<EvaluationEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }

        public Task<EvaluationEntity> GetForm(string pid, DateTime visitDate)
        {
            var expression = ExtLinq.True<EvaluationEntity>();
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_VisitDate == visitDate);
            return _service.FindEntityAsync(expression);
        }

        public Task<int> UpdateForm(EvaluationEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> DeleteForm(EvaluationEntity entity)
        {
            return _service.DeleteAsync(entity);
        }

        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> SubmitForm<TDto>(EvaluationEntity entity, TDto dto) where TDto : class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                //新建
                var patient = _uow.GetRepository<PatientEntity>().FindEntity(entity.F_Pid);
                if (patient == null)
                {
                    return Task.FromResult(0);
                }
                if (entity.Sctxdate == null)
                {
                    entity.Sctxdate = patient.F_DialysisStartTime;
                }
                if (entity.Sctxdate != null)
                {
                    var sctxdate = (DateTime)entity.Sctxdate;
                    var years = (int)(DateTime.Now - sctxdate).TotalDays / 365;
                    var months = (int)((DateTime.Now - sctxdate).TotalDays - years * 365) / 30;
                    var days = (int)(DateTime.Now - sctxdate).TotalDays - years * 365 - months * 30;
                    entity.Sfsctxvalue3 = years > 0 ? years.ToString() : null;
                    entity.Sfsctxvalue2 = months > 0 ? months.ToString() : null;
                    entity.Sfsctxvalue1 = days > 0 ? days.ToString() : null;
                }
                else
                {
                    entity.Sfsctxvalue1 = null;
                    entity.Sfsctxvalue2 = null;
                    entity.Sfsctxvalue3 = null;
                }
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }

        public async Task<string> GetEvaluationReport(string keyValue)
        {
            var entity = await _service.FindEntityAsync(keyValue);
            var patient = await _uow.GetRepository<PatientEntity>().FindEntityAsync((object) entity.F_Pid);
            List<Category> FBusinessObject;
            FBusinessObject = new List<Category>();
            Category category = new Category()
            {
                Name = patient.F_Name,
                Age = patient.F_BirthDay == null ? "" : ((int)(DateTime.Now - (DateTime)patient.F_BirthDay).TotalDays / 365).ToString() + "岁",
                CreateDate = entity.F_CreatorTime,
                Dept = "",
                No = patient.F_DialysisNo,
                Gender = patient.F_Gender,
                Evaluations = new List<EvaluationEntity> { entity }
            };
            FBusinessObject.Add(category);
            string reportFilePath = Path.Combine(AppConsts.AppRootPath,"upload", "reportfiles", "Evaluation.frx");
            string dataSetName = "Categories";
            string exportFormat = "html";
            return FastReportHelper.GetReportString(reportFilePath, dataSetName, FBusinessObject, exportFormat);
        }

    }
}
