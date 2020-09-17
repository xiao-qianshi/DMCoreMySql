using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Domain.Entity.ReportPrint.Evaluation;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IFileIndexApp : IScopedAppService
    {
        Task<List<FileIndexEntity>> GetList(Pagination pagination, string keyword);
        Task<EvaluationEntity> GetEvaluationForm(string pid, DateTime visitDate);
        Task<EvaluationEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(EvaluationEntity entity);
        Task<int> SubmitForm(EvaluationEntity entity, string keyValue);
        Task<string> GetEvaluationReport(string keyValue);
    }

    public class FileIndexApp : IFileIndexApp
    {
        private readonly IRepository<FileIndexEntity> _serviceMain = null;
        private readonly IRepository<EvaluationEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public FileIndexApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _serviceMain = _uow.GetRepository<FileIndexEntity>();
            _service = _uow.GetRepository<EvaluationEntity>();
            _httpContext = httpContext;
        }

        public Task<List<FileIndexEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<FileIndexEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Name == keyword);
                expression = expression.Or(t => t.F_DialysisNo.ToString() == keyword);
                expression = expression.Or(t => t.F_CardNo == keyword);
                expression = expression.Or(t => t.F_PatientNo == keyword);
                expression = expression.Or(t => t.F_RecordNo == keyword);
            }
            //expression = expression.And(t => t.F_DeleteMark != false);
            return _serviceMain.FindListAsync(expression, pagination);
        }
        /// <summary>
        /// 根据班次日期查询评估单
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="visitDate">透析日期</param>
        /// <returns></returns>
        public Task<EvaluationEntity> GetEvaluationForm(string pid, DateTime visitDate)
        {
            var expression = ExtLinq.True<EvaluationEntity>();
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_VisitDate == visitDate);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindEntityAsync(expression);
        }

        public Task<EvaluationEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            _service.Delete(t => t.F_Id == keyValue);
            return _serviceMain.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(EvaluationEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> SubmitForm(EvaluationEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                _service.Update(entity);
                var fileEntity = _serviceMain.FindEntity(keyValue);
                if (fileEntity != null)
                {
                    fileEntity.F_LastModifyTime = entity.F_LastModifyTime;
                    return _serviceMain.UpdateAsync(fileEntity, true);
                }
                else
                {
                    return Task.FromResult(0);
                }
            }
            else
            {
                //新建
                var patient = _uow.GetRepository<PatientEntity>().FindEntity(entity.F_Pid);// db.FindEntity<PatientEntity>(entity.F_Pid);
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
                    DateTime sctxdate = (DateTime)entity.Sctxdate;
                    int years = (int)(DateTime.Now - sctxdate).TotalDays / 365;
                    int months = (int)((DateTime.Now - sctxdate).TotalDays - years * 365) / 30;
                    int days = (int)(DateTime.Now - sctxdate).TotalDays - years * 365 - months * 30;
                    if (years > 0)
                    {
                        entity.Sfsctxvalue3 = years.ToString();
                    }
                    else
                    {
                        entity.Sfsctxvalue3 = null;
                    }
                    if (months > 0)
                    {
                        entity.Sfsctxvalue2 = months.ToString();
                    }
                    else
                    {
                        entity.Sfsctxvalue2 = null;
                    }
                    if (days > 0)
                    {
                        entity.Sfsctxvalue1 = days.ToString();
                    }
                    else
                    {
                        entity.Sfsctxvalue1 = null;
                    }
                }
                else
                {
                    entity.Sfsctxvalue1 = null;
                    entity.Sfsctxvalue2 = null;
                    entity.Sfsctxvalue3 = null;
                }
                var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
                claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
                var claimUserId = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
                var claimUserName = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.Name);
                //var LoginInfo = OperatorProvider.Provider.GetCurrent();
                entity.F_Id = Common.GuId();
                entity.F_CreatorUserId = claimUserId?.Value;
                entity.F_CreatorTime = DateTime.Now;
                //entity.Create();
                return _serviceMain.InsertAsync(new FileIndexEntity
                {
                    F_Id = entity.F_Id,
                    F_Pid = patient.F_Id,
                    F_Name = patient.F_Name,
                    F_DialysisNo = patient.F_DialysisNo,
                    F_CardNo = patient.F_CardNo,
                    F_RecordNo = patient.F_RecordNo,
                    F_Gender = patient.F_Gender,
                    F_CreatorTime = entity.F_CreatorTime,
                    F_RealName = claimUserName?.Value,
                    F_FileType = "透前评估单"
                });
            }
        }

        public Task<string> GetEvaluationReport(string keyValue)
        {
            var entity = _service.FindEntity(keyValue);
            var patient = _uow.GetRepository<PatientEntity>().FindEntity(entity.F_Pid);
            var fBusinessObject = new List<Category>();
            var category = new Category()
            {
                Name = patient.F_Name,
                Age = patient.F_BirthDay == null ? "" : ((int)(DateTime.Now - (DateTime)patient.F_BirthDay).TotalDays / 365).ToString() + "岁",
                CreateDate = entity.F_CreatorTime,
                Dept = "",
                No = patient.F_DialysisNo,
                Gender = patient.F_Gender,
                Evaluations = new List<EvaluationEntity> { entity }
            };
            fBusinessObject.Add(category);
            string reportFilePath = FileHelper.MapPath("\\ReportFiles\\Evaluation.frx");
            string dataSetName = "Categories";
            string exportFormat = "html";
            return Task.FromResult(FastReportHelper.GetReportString(reportFilePath, dataSetName, fBusinessObject, exportFormat));
        }

    }
}
