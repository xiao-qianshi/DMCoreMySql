using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Dmt.DM.Application.PatientManage
{
    public interface IProcessFlowApp : IScopedAppService
    {
        /// <summary>
        /// keyword 透析日期
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<ProcessFlowEntity>> GetList(Pagination pagination, string keyword);

        Task<List<ProcessFlowEntity>> GetList();
        Task<List<ProcessFlowEntity>> GetList(DateTime visitDate);
        Task<ProcessFlowEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(ProcessFlowEntity entity);
        Task<int> SubmitForm<TDto>(ProcessFlowEntity entity, TDto dto) where TDto : class;
        Task<int> SubmitFormBatch(List<ProcessFlowEntity> list);
    }

    public class ProcessFlowApp : IProcessFlowApp
    {
        private readonly IRepository<ProcessFlowEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IUsersService _usersService = null;
        private readonly IPatVisitApp _visitApp = null;
        private readonly IPatientApp _patientApp = null;
        private readonly IQualityResultApp _resultApp = null;
        public ProcessFlowApp(IUnitOfWork uow, IUsersService usersService, IPatVisitApp patVisitApp,IPatientApp patientApp, IQualityResultApp resultApp)
        {
            _uow = uow;
            _service = _uow.GetRepository<ProcessFlowEntity>();
            _usersService = usersService;
            _visitApp = patVisitApp;
            _patientApp = patientApp;
            _resultApp = resultApp;
        }

        /// <summary>
        /// keyword 透析日期
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<ProcessFlowEntity>> GetList(Pagination pagination, string keyword)
        {
            var json = keyword.ToJObject();
            var visitDate = json.Value<DateTime>("visitDate").Date;
            var visitNo = json.Value<int>("visitNo");

            await CreateDataAsync(visitDate);
            var expression = ExtLinq.True<ProcessFlowEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_VisitDate == visitDate);
                if (visitNo > 0)
                {
                    expression = expression.And(t => t.F_VisitNo == visitNo);
                }
            }
            else
            {
                expression = expression.And(t => t.F_Id == null);
            }
            expression = expression.And(t => t.F_DeleteMark == false || t.F_DeleteMark == null);
            return await _service.FindListAsync(expression, pagination);
        }
        /// <summary>
        /// 根据治疗单 生成数据
        /// </summary>
        /// <param name="visitDate"></param>
        private async Task CreateDataAsync(DateTime visitDate)
        {
            //QualityResultApp resultApp = new QualityResultApp();
            var listSourse = _visitApp.GetList().Where(t =>t.F_VisitDate == visitDate && t.F_DialysisStartTime != null && t.F_DialysisEndTime != null);
            var listTarget = await GetList(visitDate);
            //查询不包含的数据
            var c = from r in listSourse
                    where listTarget.All(t => t.F_Vid != r.F_Id)
                    select r;
            //查询化验结果 尿素 UREA
            var lisResltList = (from r in _resultApp.GetList()
                                where listSourse.Any(t => t.F_Pid == r.F_Pid)
                                && r.F_ReportTime >= visitDate
                                && r.F_ReportTime < visitDate.AddDays(1)
                                && r.F_ItemCode == "UREA"
                                select new
                                {
                                    r.F_Pid,
                                    r.F_ReportTime,
                                    r.F_Result
                                }).ToList();
            //患者列表
            var patientList = _patientApp.GetQueryable().Where(r => listSourse.Any(t => t.F_Pid == r.F_Id))
                .Select(r => new {r.F_Id, r.F_Name, r.F_Gender, r.F_DialysisNo});
            //生成记录
            var addEntityList = new List<ProcessFlowEntity>();
            foreach (var item in c)
            {
                var entity = new ProcessFlowEntity
                {
                    F_Pid = item.F_Pid,
                    F_Vid = item.F_Id,
                    F_VisitDate = item.F_VisitDate,
                    F_VisitNo = item.F_VisitNo,
                    F_PreWeight = item.F_WeightTQ,
                    F_PostWeight = item.F_WeightTH,
                    F_TotalHours = (item.F_DialysisEndTime.ToDate() - item.F_DialysisStartTime.ToDate()).TotalHours.ToFloat(1)
                };
                var p = patientList.FirstOrDefault(t => t.F_Id == item.F_Pid);
                if (p != null)
                {
                    entity.F_PName = p.F_Name;
                    entity.F_PGender = p.F_Gender;
                    entity.F_DialylisNo = p.F_DialysisNo;
                }
                var lis = lisResltList.FindAll(t => t.F_Pid == item.F_Pid).OrderBy(t => t.F_ReportTime).ToList();
                if (lis.Count > 0)
                {
                    entity.F_PreUrea = lis[0].F_Result.ToFloat(2);
                    if (lis.Count > 1)
                    {
                        entity.F_PostUrea = lis[1].F_Result.ToFloat(2);
                    }
                }
                if (entity.F_PreWeight != null && entity.F_PostWeight != null && entity.F_PreUrea != null && entity.F_PostUrea != null)
                {
                    var preWeight = entity.F_PreWeight.ToFloat(2);
                    var postWeight = entity.F_PostWeight.ToFloat(2);
                    if (preWeight > postWeight)
                    {
                        var preUrea = entity.F_PreUrea.ToFloat(2);
                        var postUrea = entity.F_PostUrea.ToFloat(2);
                        if (preUrea > postUrea)
                        {
                            var duration = entity.F_TotalHours.ToFloat(1);
                            if (duration > 0)
                            {
                                //计算Kt/V
                                entity.F_Result = (-Math.Log(postUrea / preUrea - 0.008 * duration) + (4 - 3.5 * postUrea / preUrea) * (preWeight - postWeight) / postWeight).ToFloat(2);
                            }
                        }
                    }
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
            var u = from r in listTarget
                    where r.F_PreUrea == null || r.F_PreWeight == null || r.F_PostUrea == null || r.F_PostWeight == null
                    select r;
            foreach (var item in u)
            {
                bool isModified = false;
                if (item.F_PreUrea == null || item.F_PostUrea == null)
                {
                    var lis = lisResltList.FindAll(t => t.F_Pid == item.F_Pid).OrderBy(t => t.F_ReportTime).ToList();
                    if (lis.Count > 0)
                    {
                        if (item.F_PreUrea == null)
                        {
                            item.F_PreUrea = lis[0].F_Result.ToFloat(2);
                            isModified = true;
                        }
                        if (lis.Count > 1)
                        {
                            if (item.F_PostUrea == null)
                            {
                                item.F_PostUrea = lis[1].F_Result.ToFloat(2);
                                isModified = true;
                            }
                        }
                    }
                }
                if (item.F_PreWeight == null || item.F_PostWeight == null)
                {
                    var find = listSourse.FirstOrDefault(t => t.F_Pid == item.F_Pid);
                    if (item.F_PreWeight == null)
                    {
                        item.F_PreWeight = find.F_WeightTQ;
                        isModified = true;
                    }
                    if (item.F_PostWeight == null)
                    {
                        item.F_PreWeight = find.F_WeightTH;
                        isModified = true;
                    }
                }
                if (item.F_PreWeight != null && item.F_PostWeight != null && item.F_PreUrea != null && item.F_PostUrea != null)
                {
                    var preWeight = item.F_PreWeight.ToFloat(2);
                    var postWeight = item.F_PostWeight.ToFloat(2);
                    if (preWeight > postWeight)
                    {
                        var preUrea = item.F_PreUrea.ToFloat(2);
                        var postUrea = item.F_PostUrea.ToFloat(2);
                        if (preUrea > postUrea)
                        {
                            var duration = item.F_TotalHours.ToFloat(1);
                            if (duration > 0)
                            {
                                //计算Kt/V
                                item.F_Result = (-Math.Log(postUrea / preUrea - 0.008 * duration) + (4 - 3.5 * postUrea / preUrea) * (preWeight - postWeight) / postWeight).ToFloat(2);
                                isModified = true;
                            }
                        }
                    }
                }
                if (isModified) _service.Update(item);
            }
        }

        public Task<List<ProcessFlowEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<List<ProcessFlowEntity>> GetList(DateTime visitDate)
        {
            var expression = ExtLinq.True<ProcessFlowEntity>();
            expression = expression.And(t => t.F_VisitDate == visitDate);
            return _service.IQueryable(expression).ToListAsync();
        }

        public Task<ProcessFlowEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(ProcessFlowEntity entity)
        {
            if (entity.F_PreWeight != null && entity.F_PostWeight != null && entity.F_PreUrea != null && entity.F_PostUrea != null)
            {
                var preWeight = entity.F_PreWeight.ToFloat(2);
                var postWeight = entity.F_PostWeight.ToFloat(2);
                if (preWeight > postWeight)
                {
                    var preUrea = entity.F_PreUrea.ToFloat(2);
                    var postUrea = entity.F_PostUrea.ToFloat(2);
                    if (preUrea > postUrea)
                    {
                        var duration = entity.F_TotalHours.ToFloat(1);
                        if (duration > 0)
                        {
                            //计算Kt/V
                            entity.F_Result = (-Math.Log(postUrea / preUrea - 0.008 * duration) + (4 - 3.5 * postUrea / preUrea) * (preWeight - postWeight) / postWeight).ToFloat(2);
                        }
                    }
                }
            }
            //为false时
            if (!entity.F_Step_1.ToBool())
            {
                entity.F_Step_1_Reason1 = entity.F_Step_1_Reason2 = entity.F_Step_1_Reason3 = false;
                entity.F_Step_1_Measures = null;
            }
            if (!entity.F_Step_2.ToBool())
            {
                entity.F_Step_2_Reason1 = entity.F_Step_2_Reason2 = entity.F_Step_2_Reason3 = entity.F_Step_2_Reason4 = false;
                entity.F_Step_2_Measures = null;
            }
            if (!entity.F_Step_3.ToBool())
            {
                entity.F_Step_3_Measures = null;
            }
            if (!entity.F_Step_4.ToBool())
            {
                entity.F_Step_4_Measures = null;
            }
            if (!entity.F_Step_5.ToBool())
            {
                entity.F_Step_5_Measures = null;
            }
            return _service.UpdateAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(ProcessFlowEntity entity, TDto dto) where TDto : class
        {
            
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                if (entity.F_PreWeight != null && entity.F_PostWeight != null && entity.F_PreUrea != null && entity.F_PostUrea != null)
                {
                    var preWeight = entity.F_PreWeight.ToFloat(2);
                    var postWeight = entity.F_PostWeight.ToFloat(2);
                    if (preWeight > postWeight)
                    {
                        var preUrea = entity.F_PreUrea.ToFloat(2);
                        var postUrea = entity.F_PostUrea.ToFloat(2);
                        if (preUrea > postUrea)
                        {
                            var duration = entity.F_TotalHours.ToFloat(1);
                            if (duration > 0)
                            {
                                //计算Kt/V
                                entity.F_Result = (-Math.Log(postUrea / preUrea - 0.008 * duration) + (4 - 3.5 * postUrea / preUrea) * (preWeight - postWeight) / postWeight).ToFloat(2);
                            }
                        }
                    }
                }

                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }

        public Task<int> SubmitFormBatch(List<ProcessFlowEntity> list)
        {
            
            foreach (var entity in list)
            {
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                entity.F_EnabledMark = true;
            }
            return _service.InsertAsync(list);
        }

    }
}
