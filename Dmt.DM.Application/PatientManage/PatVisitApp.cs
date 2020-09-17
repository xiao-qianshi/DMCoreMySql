using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Domain.Entity.ReportPrint;
using Dmt.DM.Domain.Entity.ReportPrint.DialysisRecord;
using Dmt.DM.UOW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IPatVisitApp : IScopedAppService
    {
        /// <summary>
        /// 透析记录分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="visitNo">透析班次 1 第一班；2 第二班；4 第三班；8 全部班次（3 第一、二班 ；5 第一、三班 ；6 第二、三班）</param>
        /// <param name="status">完成状态 1 已完成 2 未完成 4 全部</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<PatVisitEntity>> GetList(Pagination pagination, string keyword, int visitNo, int status,DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 透析记录筛选
        /// </summary>
        /// <returns></returns>
        IQueryable<PatVisitEntity> GetList();

        /// <summary>
        /// 提交体重数据（自助称重）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="weightData">体重值</param>
        /// <returns></returns>
        Task<string> SubmitWeightData(string id, float weightData);

        Task<int> InsertForm(PatVisitEntity entity);

        /// <summary>
        /// 根据治疗单主键查询称重记录
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        IQueryable<WeightLogEntity> GetWeightLogList(string visitId);

        /// <summary>
        /// 透析记录刷选（日期、班次）
        /// </summary>
        /// <param name="visitDate"></param>
        /// <param name="visitNo">透析班次 1 第一班；2 第二班；4 第三班；8 全部班次（3 第一、二班 ；5 第一、三班 ；6 第二、三班）</param>
        /// <returns></returns>
        IQueryable<PatVisitEntity> GetList(DateTime visitDate, int visitNo);

        /// <summary>
        /// 查询是否归档
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        bool GetArchiveStatus(string visitId);

        /// <summary>
        ///查询是否记账
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        bool GetAcctStatus(string visitId);

        Task<PatVisitEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(PatVisitEntity entity);

        /// <summary>
        /// 修改预脱体重 ，添加日志
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="weightValue">预脱量</param>
        /// <returns></returns>
        Task<int> UpdateYTWeight(string visitId,float weightValue);

        Task<int> SubmitForm<TDto>(PatVisitEntity entity, TDto dto) where TDto : class;

        /// <summary>
        /// 保存透析小节
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<int> SaveMemo(string visitId,string content);

        /// <summary>
        /// 打印透析记录单（单一）
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<string> GetRecordReport(Category category);

        /// <summary>
        /// 打印透析记录单（多个）
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        Task<string> GetRecordReport(List<Category> categories);

        /// <summary>
        /// 打印透前记录单
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        Task<string> GetRecordReportTQ(List<Category> categories);

        /// <summary>
        /// 打印透前准备清单
        /// </summary>
        /// <param name="dialysisPrepare"></param>
        /// <returns></returns>
        Task<string> GetPrepareReport(DialysisPrepare dialysisPrepare);

        /// <summary>
        /// 打印护士核对卡片
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> GetDialysisCheckImage(List<DialysisCheckRecord> data);

        /// <summary>
        /// 更改透析记录状态（上机、下机、审核）
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="operateTime">操作时间</param>
        /// <param name="operateType">操作类型：上机、下机、审核</param>
        /// <returns></returns>
        Task<int> SetOperateMachineTime(string visitId, DateTime operateTime, string operateType);

        /// <summary>
        /// 医生签名
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        Task<int> DoctorSign(string visitId);
    }

    public class PatVisitApp : IPatVisitApp
    {
        private readonly IRepository<PatVisitEntity> _service = null;
        private IUnitOfWork _uow = null;
        private readonly IUsersService _usersService;

        public PatVisitApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<PatVisitEntity>();
            _usersService = usersService;
        }

        /// <summary>
        /// 透析记录分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="visitNo">透析班次 1 第一班；2 第二班；4 第三班；8 全部班次（3 第一、二班 ；5 第一、三班 ；6 第二、三班）</param>
        /// <param name="status">完成状态 1 已完成 2 未完成 4 全部</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Task<List<PatVisitEntity>> GetList(Pagination pagination, string keyword, int visitNo, int status,DateTime? startDate, DateTime? endDate)
        {
            var expression = ExtLinq.True<PatVisitEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_DialysisBedNo == keyword || t.F_Name.Contains(keyword));
            }
            if (startDate.HasValue)
            {
                var date = startDate.ToDate().Date;
                expression = expression.And(t => t.F_VisitDate >= date);
            }
            if (endDate.HasValue)
            {
                var date = endDate.ToDate().Date;
                expression = expression.And(t => t.F_VisitDate <= date);
            }
            switch (visitNo)
            {
                case 1:
                    expression = expression.And(t => t.F_VisitNo == 1);
                    break;
                case 2:
                    expression = expression.And(t => t.F_VisitNo == 2);
                    break;
                case 3:
                    expression = expression.And(t => t.F_VisitNo == 1 || t.F_VisitNo == 2);
                    break;
                case 4:
                    expression = expression.And(t => t.F_VisitNo == 3);
                    break;
                case 5:
                    expression = expression.And(t => t.F_VisitNo == 1 || t.F_VisitNo == 3);
                    break;
                case 6:
                    expression = expression.And(t => t.F_VisitNo == 2 || t.F_VisitNo == 3);
                    break;
            }
            switch (status)
            {
                case 1:
                    expression = expression.And(t => t.F_DialysisEndTime != null);
                    break;
                case 2:
                    expression = expression.And(t => t.F_DialysisEndTime == null);
                    break;
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        /// <summary>
        /// 透析记录筛选
        /// </summary>
        /// <returns></returns>
        public IQueryable<PatVisitEntity> GetList()
        {
            var expression = ExtLinq.True<PatVisitEntity>();
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression);
        }

        /// <summary>
        /// 提交体重数据（自助称重）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="weightData">体重值</param>
        /// <returns></returns>
        public async Task<string> SubmitWeightData(string id, float weightData)
        {
            var weightLogRepository = _uow.GetRepository<WeightLogEntity>();
            await weightLogRepository.InsertAsync(new WeightLogEntity//写入体重日志记录
            {
                F_Id = Common.GuId(),
                F_Vid = id,
                F_Value = weightData
            });
            var entity = await _service.FindEntityAsync(id);
            if (entity == null) return "透析记录主键有误！";

            if (entity.F_FirstWeightTime == null)//透前体重
            {
                var patientRepository = _uow.GetRepository<PatientEntity>();
                var patient = await patientRepository.FindEntityAsync((object) entity.F_Pid);
                if (patient.F_IdealWeight == null)
                {
                    entity.F_FirstWeightTime = DateTime.Now;
                    entity.F_WeightTQ = weightData.ToFloat(2);
                    await _service.UpdateAsync(entity);
                    return "干体重未设置，请联系医生！";
                }
                else
                {
                    var ideaWeight = patient.F_IdealWeight.ToFloat(2);
                    if (ideaWeight > weightData)
                    {
                        entity.F_FirstWeightTime = DateTime.Now;
                        entity.F_WeightTQ = weightData.ToFloat(2);
                        await _service.UpdateAsync(entity);
                        return "干体重设置有误，请联系医生！";
                    }
                    else
                    {
                        entity.F_FirstWeightTime = DateTime.Now;
                        entity.F_WeightTQ = weightData.ToFloat(2);
                        float cz = 0;
                        if (entity.F_WeightYT == null)
                        {
                            cz = weightData - ideaWeight;
                            cz = cz > 5 ? 5 : cz.ToFloat(2);
                            entity.F_WeightYT = cz;
                        }
                        else
                        {
                            var origaldata = entity.F_WeightYT.ToFloat(2);
                            if (origaldata > 0 && origaldata <= 5)
                            {
                                cz = origaldata;
                            }
                            else
                            {
                                cz = weightData - ideaWeight;
                                cz = cz > 5 ? 5 : cz.ToFloat(2);
                                //entity.F_WeightYT = cz;
                            }
                        }
                        await _service.UpdateAsync(entity);
                        return "预脱：" + cz + "Kg";
                    }
                }
            }
            else
            {
                var span = DateTime.Now - entity.F_FirstWeightTime.ToDate();
                if (span.TotalMinutes < 50) //50分钟以内 算透前体重
                {
                    var patientRepository = _uow.GetRepository<PatientEntity>();
                    var patient = await patientRepository.FindEntityAsync((object) entity.F_Pid);
                    if (patient.F_IdealWeight == null)
                    {
                        entity.F_WeightTQ = weightData.ToFloat(2);
                        await _service.UpdateAsync(entity);
                        return "干体重未设置，请联系医生！";
                    }
                    else
                    {
                        var ideaWeight = patient.F_IdealWeight.ToFloat(2);
                        if (ideaWeight > weightData)
                        {
                            return "干体重设置有误，请联系医生！";
                        }
                        else
                        {
                            //var cz = weightData - ideaWeight;
                            //cz = cz > 5 ? 5 : cz.ToFloat(2);
                            entity.F_WeightTQ = weightData.ToFloat(2);
                            //entity.F_WeightYT = cz;

                            float cz = 0;
                            if (entity.F_WeightYT == null)
                            {
                                cz = weightData - ideaWeight;
                                cz = cz > 5 ? 5 : cz.ToFloat(2);
                                entity.F_WeightYT = cz;
                            }
                            else
                            {
                                var origaldata = entity.F_WeightYT.ToFloat(2);
                                if (origaldata > 0 && origaldata <= 5)
                                {
                                    cz = origaldata;
                                }
                                else
                                {
                                    cz = weightData - ideaWeight;
                                    cz = cz > 5 ? 5 : cz.ToFloat(2);
                                    //entity.F_WeightYT = cz;
                                }
                            }

                            await _service.UpdateAsync(entity);
                            return "预脱：" + cz + "Kg";
                        }
                    }
                }
                else //有称重时间 而且超过50分钟  算透后体重
                {
                    if (entity.F_WeightTQ != null)
                    {
                        if (entity.F_WeightTH == null)
                        {
                            var cz = entity.F_WeightTQ.ToFloat(2) - weightData;
                            if (cz > 0)
                            {
                                entity.F_WeightTH = weightData.ToFloat(2);
                                entity.F_WeightST = cz.ToFloat(2);
                                await _service.UpdateAsync(entity);
                                return "实脱：" + cz + "Kg";
                            }
                        }
                    }
                    if (entity.F_WeightST != null)
                    {
                        return "实脱：" + entity.F_WeightST??"" + "Kg";
                    }
                    return "请咨询医生实脱体重！";
                }
            }
        }

        public Task<int> InsertForm(PatVisitEntity entity)
        {
            return _service.InsertAsync(entity);
        }

        /// <summary>
        /// 根据治疗单主键查询称重记录
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public IQueryable<WeightLogEntity> GetWeightLogList(string visitId)
        {
            var weightLogRepository = _uow.GetRepository<WeightLogEntity>();
            var expression = ExtLinq.True<WeightLogEntity>();
            expression = expression.And(t => t.F_Vid ==visitId);
            return weightLogRepository.IQueryable(expression);
        }


        /// <summary>
        /// 透析记录刷选（日期、班次）
        /// </summary>
        /// <param name="visitDate"></param>
        /// <param name="visitNo">透析班次 1 第一班；2 第二班；4 第三班；8 全部班次（3 第一、二班 ；5 第一、三班 ；6 第二、三班）</param>
        /// <returns></returns>
        public IQueryable<PatVisitEntity> GetList(DateTime visitDate, int visitNo)
        {
            var expression = ExtLinq.True<PatVisitEntity>();
            expression = expression.And(t => t.F_VisitDate == visitDate);
            switch (visitNo)
            {
                case 1:
                    expression = expression.And(t => t.F_VisitNo == 1);
                    break;
                case 2:
                    expression = expression.And(t => t.F_VisitNo == 2);
                    break;
                case 3:
                    expression = expression.And(t => t.F_VisitNo == 1 || t.F_VisitNo == 2);
                    break;
                case 4:
                    expression = expression.And(t => t.F_VisitNo == 3);
                    break;
                case 5:
                    expression = expression.And(t => t.F_VisitNo == 1 || t.F_VisitNo == 3);
                    break;
                case 6:
                    expression = expression.And(t => t.F_VisitNo == 2 || t.F_VisitNo == 3);
                    break;
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression);
        }
        /// <summary>
        /// 查询是否归档
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public bool GetArchiveStatus(string visitId)
        {
            var expression = ExtLinq.True<PatVisitEntity>();
            expression = expression.And(t => t.F_Id == visitId);
            var result = _service.IQueryable(expression).Select(t => t.F_IsArchive).FirstOrDefault();
            return result != null && result != false;
        }
        /// <summary>
        ///查询是否记账
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public bool GetAcctStatus(string visitId)
        {
            var expression = ExtLinq.True<PatVisitEntity>();
            expression = expression.And(t => t.F_Id == visitId);
            var result = _service.IQueryable(expression).Select(t => t.F_IsAcct).FirstOrDefault();
            return result != null && result != false;
        }

        public Task<PatVisitEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }

        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(PatVisitEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        /// <summary>
        /// 修改预脱体重 ，添加日志
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="weightValue">预脱量</param>
        /// <returns></returns>
        public async Task<int> UpdateYTWeight(string visitId,float weightValue)
        {
            var entity = await GetForm(visitId);
            if (entity == null) return 0;
            entity.F_WeightYT = weightValue;
            await UpdateForm(entity);

            var repository = _uow.GetRepository<YTWeightLogEntity>();
            var yTWeightLog = new YTWeightLogEntity
            {
                F_Vid = entity.F_Id,
                F_Value = entity.F_WeightYT,
                F_CreatorUserId = _usersService.GetCurrentUserId()
            };
            yTWeightLog.Create();
            return await repository.InsertAsync(yTWeightLog);
        }

        public Task<int> SubmitForm<TDto>(PatVisitEntity entity, TDto dto) where TDto :class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);

                if (entity.F_WeightTQ != null)//手工录入透前体重
                {
                    var expression = ExtLinq.True<PatVisitEntity>();
                    expression = expression.And(t => t.F_Id == entity.F_Id);
                    var find = _service.IQueryable(expression).Select(t => new
                    {
                        //t.F_Pid,
                        t.F_FirstWeightTime,
                        //t.F_WeightYT,
                        t.F_VisitDate
                    }).FirstOrDefault();
                    if (find != null)
                    {
                        if (find.F_FirstWeightTime == null && find.F_VisitDate.ToDate().Date == DateTime.Now.Date)
                        {
                            entity.F_FirstWeightTime = DateTime.Now;
                           
                        }
                    }
                }

                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();

                if (entity.F_VisitDate == null)
                {
                    entity.F_VisitDate = DateTime.Now.Date;
                }
                if (entity.F_FirstWeightTime == null && entity.F_WeightTQ != null)
                {
                    entity.F_FirstWeightTime = DateTime.Now;
                }
               

                //查询30天内最近的一次透析记录
                var exp = ExtLinq.True<PatVisitEntity>();
                exp = exp.And(t => t.F_Pid == entity.F_Pid);
                var dt = DateTime.Now.AddDays(-30).Date;
                exp = exp.And(t => t.F_VisitDate >= dt);
                var findList = _service.IQueryable(exp).OrderByDescending(t => t.F_VisitDate);
                var lastEntity = findList.FirstOrDefault(t => t.F_DialysisType==entity.F_DialysisType && t.F_DialysisType != null) ?? findList.FirstOrDefault();
                //var lastEntity = _service.IQueryable(exp).OrderByDescending(t=>t.F_VisitDate).ToList().FirstOrDefault();

                if (lastEntity != null)
                {
                    if (entity.F_WeightSXTH == null)
                    {
                        entity.F_WeightSXTH = lastEntity.F_WeightTH;
                    }
                    //复制其他的值
                    if (entity.F_AccessName == null)
                    {
                        entity.F_AccessName = lastEntity.F_AccessName;
                    }
                    if (entity.F_Ca == null)
                    {
                        entity.F_Ca = lastEntity.F_Ca;
                    }
                    if (entity.F_DialyzerType1 == null)
                    {
                        entity.F_DialyzerType1 = lastEntity.F_DialyzerType1;
                    }
                    if (entity.F_DialyzerType1 == null)
                    {
                        entity.F_DialyzerType1 = lastEntity.F_DialyzerType1;
                    }
                    if (entity.F_DialyzerType2 == null)
                    {
                        entity.F_DialyzerType2 = lastEntity.F_DialyzerType2;
                    }
                    if (entity.F_DilutionType == null)
                    {
                        entity.F_DilutionType = lastEntity.F_DilutionType;
                    }
                    if (entity.F_Hco3 == null)
                    {
                        entity.F_Hco3 = lastEntity.F_Hco3;
                    }
                    if (entity.F_IsCritical == null)
                    {
                        entity.F_IsCritical = lastEntity.F_IsCritical;
                    }
                    if (entity.F_K == null)
                    {
                        entity.F_K = lastEntity.F_K;
                    }
                    if (entity.F_Na == null)
                    {
                        entity.F_Na = lastEntity.F_Na;
                    }
                    if (entity.F_PatientSourse == null)
                    {
                        entity.F_PatientSourse = lastEntity.F_PatientSourse;
                    }
                    if (entity.F_VascularAccess == null)
                    {
                        entity.F_VascularAccess = lastEntity.F_VascularAccess;
                    }

                    if (entity.F_BloodSpeed == null)
                    {
                        entity.F_BloodSpeed = lastEntity.F_BloodSpeed;
                    }
                    if (entity.F_DialysateTemperature == null)
                    {
                        entity.F_DialysateTemperature = lastEntity.F_DialysateTemperature;
                    }
                    if (entity.F_EstimateHours == null)
                    {
                        entity.F_EstimateHours = lastEntity.F_EstimateHours;
                    }
                }

                //抗凝剂 策略 以透析参数设置为主，不存在是查询最后一次的透析记录
                lastEntity = findList.FirstOrDefault();
                if (lastEntity != null)
                {
                    if (entity.F_HeparinAddAmount == null)
                    {
                        entity.F_HeparinAddAmount = lastEntity.F_HeparinAddAmount;
                    }
                    if (entity.F_HeparinAmount == null)
                    {
                        entity.F_HeparinAmount = lastEntity.F_HeparinAmount;
                    }
                    if (entity.F_HeparinType == null)
                    {
                        entity.F_HeparinType = lastEntity.F_HeparinType;
                    }
                    if (entity.F_HeparinUnit == null)
                    {
                        entity.F_HeparinUnit = lastEntity.F_HeparinUnit;
                    }
                }

                entity.F_EnabledMark = true;

                return _service.InsertAsync(entity);
            }
        }
        /// <summary>
        /// 保存透析小节
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<int> SaveMemo(string visitId,string content)
        {
            var entity = await GetForm(visitId);
            if (entity == null) return 0;
            entity.F_Memo = content;
            return await UpdateForm(entity);
        }
        /// <summary>
        /// 打印透析记录单（单一）
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Task<string> GetRecordReport(Category category)
        {
            var fBusinessObject = new List<Category>
            {
                category
            };
            var reportFilePath = Path.Combine(AppConsts.AppRootPath,"upload", "reportfiles", "DialysisRecord.frx");//FileHelper.MapPath("\\ReportFiles\\DialysisRecord.frx");
            const string dataSetName = "Categories";
            const string exportFormat = "html";
            return Task.FromResult(FastReportHelper.GetReportString(reportFilePath, dataSetName, fBusinessObject, exportFormat));
        }

        /// <summary>
        /// 打印透析记录单（多个）
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public Task<string> GetRecordReport(List<Category> categories)
        {
            var reportFilePath = Path.Combine(AppConsts.AppRootPath, "upload", "reportfiles", "DialysisRecord.frx");
            const string dataSetName = "Categories";
            const string exportFormat = "html";
            return Task.FromResult(FastReportHelper.GetReportString(reportFilePath, dataSetName, categories, exportFormat));
        }
        /// <summary>
        /// 打印透前记录单
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public Task<string> GetRecordReportTQ(List<Category> categories)
        {
            var reportFilePath = Path.Combine(AppConsts.AppRootPath, "upload", "reportfiles", "DialysisRecordTQ.frx"); //FileHelper.MapPath("\\ReportFiles\\DialysisRecordTQ.frx");
            const string dataSetName = "Categories";
            const string exportFormat = "html";
            return Task.FromResult(FastReportHelper.GetReportString(reportFilePath, dataSetName, categories, exportFormat));
        }

        /// <summary>
        /// 打印透前准备清单
        /// </summary>
        /// <param name="dialysisPrepare"></param>
        /// <returns></returns>
        public Task<string> GetPrepareReport(DialysisPrepare dialysisPrepare)
        {
            var fBusinessObject = new List<DialysisPrepare>
            {
                dialysisPrepare
            };
            var reportFilePath = Path.Combine(AppConsts.AppRootPath, "upload", "reportfiles", "DialysisPrepare.frx"); //FileHelper.MapPath("\\ReportFiles\\DialysisPrepare.frx");
            const string dataSetName = "DialysisPrepare";
            const string exportFormat = "html";
            return Task.FromResult(FastReportHelper.GetReportString(reportFilePath, dataSetName, fBusinessObject, exportFormat));
        }
        /// <summary>
        /// 打印护士核对卡片
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<string> GetDialysisCheckImage(List<DialysisCheckRecord> data)
        {
            var reportFilePath = Path.Combine(AppConsts.AppRootPath, "upload", "reportfiles", "DialysisCheckRecord.frx");// FileHelper.MapPath("\\ReportFiles\\DialysisCheckRecord.frx");
            const string dataSetName = "DialysisCheckRecord";
            const string exportFormat = "html";
            return Task.FromResult(FastReportHelper.GetReportString(reportFilePath, dataSetName, data, exportFormat));
        }

        /// <summary>
        /// 更改透析记录状态（上机、下机、审核）
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="operateTime">操作时间</param>
        /// <param name="operateType">操作类型：上机、下机、审核</param>
        /// <returns></returns>
        public async Task<int> SetOperateMachineTime(string visitId, DateTime operateTime, string operateType)
        {
            var entity = await GetForm(visitId);
            var userId = _usersService.GetCurrentUserId();
            switch (operateType)
            {
                case "上机":
                    if (entity.F_DialysisStartTime == null)
                    {
                        entity.F_DialysisStartTime = operateTime;
                        entity.F_StartPerson = userId;
                    }

                    break;
                case "下机":
                    if (entity.F_DialysisEndTime == null)
                    {
                        entity.F_DialysisEndTime = operateTime;
                        entity.F_EndPerson = userId;
                    }

                    break;
                case "审核":
                    if (entity.F_CheckPerson == null)
                    {
                        entity.F_CheckPerson = userId;
                    }

                    break;
            }

            return await UpdateForm(entity);
        }

        /// <summary>
        /// 医生签名
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public async Task<int> DoctorSign(string visitId)
        {
            var userId = _usersService.GetCurrentUserId();
            var entity = await GetForm(visitId);
            entity.F_RecordDoctor = userId;
            return await UpdateForm(entity);
        }
    }
}
