using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Domain.Entity.ReportPrint.MachineProcess;
using Dmt.DM.UOW;

namespace Dmt.DM.Application.MachineManage
{
    public interface IMachineProcessApp : IScopedAppService
    {
        Task<List<MachineProcessEntity>> GetList(Pagination pagination, DateTime visitDate,int visitNo);
        IQueryable<MachineProcessEntity> GetList(DateTime startDate, DateTime endDate, string bedId);
        IQueryable<MachineProcessEntity> GetList(DateTime visitDate, int visitNo);
        void InsertForm(MachineProcessEntity entity);
        MachineProcessEntity GetFormByVid(string keyValue);
        IQueryable<MachineProcessEntity> GetList();
        IQueryable<MachineProcessEntity> GetListByDate(DateTime startDate, DateTime endDate);
        Task<MachineProcessEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(MachineProcessEntity entity);
        Task<int> SubmitForm<TDto>(MachineProcessEntity entity, TDto dto) where TDto : class;
        void SubmitFormBatch(List<MachineProcessEntity> list);
        string GetImageReport(List<ProcessSummeryInfo> processSummeryInfos);
    }

    public class MachineProcessApp : IMachineProcessApp
    {
        private readonly IRepository<MachineProcessEntity> _service;
        private readonly IPatVisitApp _patVisitApp;
        private readonly IPatientApp _patientApp;
        private readonly IUsersService _usersService;
        private readonly IDialysisMachineApp _dialysisMachineApp;

        public MachineProcessApp(IUnitOfWork uow,IPatVisitApp patVisitApp, IPatientApp patientApp, IUsersService usersService, IDialysisMachineApp dialysisMachineApp)
        {
            _service = uow.GetRepository<MachineProcessEntity>();
            _patVisitApp = patVisitApp;
            _patientApp = patientApp;
            _usersService = usersService;
            _dialysisMachineApp = dialysisMachineApp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="visitDate"></param>
        /// <param name="visitNo"></param>
        /// <returns></returns>
        public async Task<List<MachineProcessEntity>> GetList(Pagination pagination, DateTime visitDate,int visitNo)
        {
            //var json = keyword.ToJObject();
            //var visitDate = json.Value<DateTime>("visitDate").Date;
            //var visitNo = json.Value<int>("visitNo");

            CreateData(visitDate, visitNo);
            var expression = ExtLinq.True<MachineProcessEntity>();
            expression = expression.And(t => t.F_VisitDate == visitDate);
            if (visitNo>0) expression = expression.And(t => t.F_VisitNo == visitNo);
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    expression = expression.And(t => t.F_VisitDate == visitDate);
            //    if (visitNo > 0)
            //    {
            //        expression = expression.And(t => t.F_VisitNo == visitNo);
            //    }
            //}
            //else
            //{
            //    expression = expression.And(t => t.F_Id == null);
            //}
            expression = expression.And(t => t.F_DeleteMark == false || t.F_DeleteMark == null);
            var list = await _service.FindListAsync(expression, pagination);
            if (list.Count > 0)
            {
                var users = _usersService.GetUserNameDict("");
                foreach (var item in list)
                {
                    var find = users.FirstOrDefault(t => t.F_Id == item.F_OperatePerson);
                    if (find != null)
                    {
                        item.F_OperatePerson = find.F_RealName;
                    }
                }
            }
            return list;
        }

        public IQueryable<MachineProcessEntity> GetList(DateTime startDate, DateTime endDate, string bedId)
        {
            var expression = ExtLinq.True<MachineProcessEntity>();
            expression = expression.And(t => t.F_VisitDate >= startDate && t.F_VisitDate <= endDate);
            expression = expression.And(t => t.F_Mid == bedId);
            return _service.IQueryable(expression);
        }

        public IQueryable<MachineProcessEntity> GetList(DateTime visitDate, int visitNo)
        {
            var expression = ExtLinq.True<MachineProcessEntity>();
            expression = expression.And(t => t.F_VisitDate == visitDate);
            if (visitNo > 0) expression = expression.And(t => t.F_VisitNo == visitNo);
           
            return _service.IQueryable(expression);
        }

        /// <summary>
        /// 根据治疗单 生成数据
        /// </summary>
        /// <param name="visitDate"></param>
        /// <param name="visitNo"></param>
        private void CreateData(DateTime visitDate,int visitNo)
        {
            //QualityResultApp resultApp = new QualityResultApp();
            var listSourse = _patVisitApp.GetList().Where(t => t.F_VisitDate == visitDate && (visitNo == 0 || t.F_VisitNo == visitNo) && t.F_DialysisStartTime != null && t.F_DialysisEndTime != null);
            var listTarget = GetList(visitDate, visitNo);
            //查询不包含的数据
            var c = (from r in listSourse
                     where !listTarget.Any(t => t.F_Vid == r.F_Id)
                     select r).ToList();
            //没有 返回
            if (c.Count == 0) return;
            //患者列表
            var patientList = from r in _patientApp.GetQueryable()
                              where c.Any(t => t.F_Pid == r.F_Id)
                              select new
                              {
                                  r.F_Id,
                                  r.F_Name,
                                  r.F_Gender,
                                  r.F_DialysisNo
                              };

            var bedList = from b in _dialysisMachineApp.GetQueryable()
                          select new
                          {
                              b.F_Id,
                              b.F_GroupName,
                              b.F_DialylisBedNo,
                              b.F_MachineName,
                              b.F_MachineNo,
                              b.F_ShowOrder
                          };
            //生成记录
            var addEntityList = new List<MachineProcessEntity>();
            foreach (var item in c)
            {
                var entity = new MachineProcessEntity
                {
                    F_Pid = item.F_Pid,
                    F_Vid = item.F_Id,
                    F_VisitDate = item.F_VisitDate,
                    F_VisitNo = item.F_VisitNo,
                    F_Option1 = false,
                    F_Option2 = false,
                    F_Option3 = false,
                    F_Option4 = false,
                    //F_Option5 = false,
                    F_GroupName = item.F_GroupName,
                    F_DialylisBedNo = item.F_DialysisBedNo
                };
                var p = patientList.FirstOrDefault(t => t.F_Id == item.F_Pid);
                if (p != null)
                {
                    entity.F_PName = p.F_Name;
                    entity.F_PGender = p.F_Gender;
                    entity.F_DialylisNo = p.F_DialysisNo;
                }
                else
                {
                    continue;
                }
                var b = bedList.FirstOrDefault(t => t.F_GroupName == item.F_GroupName && t.F_DialylisBedNo == item.F_DialysisBedNo);
                if (b != null)
                {
                    entity.F_MachineName = b.F_MachineName;
                    entity.F_MachineNo = b.F_MachineNo;
                    entity.F_ShowOrder = b.F_ShowOrder;
                    entity.F_Mid = b.F_Id;
                }
                else
                {
                    continue;
                }
                entity.Create();
                entity.F_EnabledMark = true;
                addEntityList.Add(entity);
            }
            if (addEntityList.Count > 0)
            {
                //保存记录
                _service.Insert(addEntityList);
            }
            //更新记录
        }

        public void InsertForm(MachineProcessEntity entity)
        {
            _service.Insert(entity);
        }

        /// <summary>
        /// 根据透析记录主键查找
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public MachineProcessEntity GetFormByVid(string keyValue)
        {
            var expression = ExtLinq.True<MachineProcessEntity>();
            expression = expression.And(t => t.F_Vid == keyValue);
            return _service.IQueryable(expression).FirstOrDefault();
        }

        public IQueryable<MachineProcessEntity> GetList()
        {
            var expression = ExtLinq.True<MachineProcessEntity>();
            return _service.IQueryable(expression);
        }

        public IQueryable<MachineProcessEntity> GetListByDate(DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<MachineProcessEntity>();
            expression = expression.And(t => t.F_VisitDate >= startDate).And(t => t.F_VisitDate <= endDate);
            return _service.IQueryable(expression);
        }

        public Task<MachineProcessEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(MachineProcessEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(MachineProcessEntity entity, TDto dto) where TDto : class
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

        public void SubmitFormBatch(List<MachineProcessEntity> list)
        {
            foreach (var entity in list)
            {
                entity.Create();
                entity.F_EnabledMark = true;
            }
            _service.Insert(list);
        }

        public string GetImageReport(List<ProcessSummeryInfo> processSummeryInfos)
        {
            var reportFilePath = Path.Combine(AppConsts.AppRootPath,"upload","reportfiles", "MachineProcess.frx");// FileHelper.MapPath("\\ReportFiles\\MachineProcess.frx");
            var dataSetName = "ProcessSummeryInfo";
            var exportFormat = "html";
            return FastReportHelper.GetReportString(reportFilePath, dataSetName, processSummeryInfos, exportFormat);
        }
    }
}
