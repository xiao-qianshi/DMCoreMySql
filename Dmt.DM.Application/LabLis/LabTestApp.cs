using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.LabLis;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmt.DM.Application.LabLis
{
    public interface ILabTestApp : IScopedAppService
    {
        Task<List<LabTestEntity>> GetList(string instrumentId, DateTime testDate);
        string GetDetailedList(string instrumentId, DateTime testDate);
        Task<string> AuditTest(LabTestEntity entity);
        Task<List<LabReportEntity>> SaveItem(string testId, string items);

        /// <summary>
        /// 查询报告子项
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        List<LabReportEntity> GetReport(long testId);

        LabTestEntity GetForm(string keyValue);
        Task<int> AssignSample(string instrumentId, DateTime testDate, string sheetId);
    }

    public class LabTestApp : ILabTestApp
    {
        private readonly IRepository<LabTestEntity> _service = null;
        private readonly IRepository<LabReportEntity> _reportService = null;
        private readonly IRepository<LabSheetEntity> _labSheetService = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;
        private readonly ILabInstrumentApp _labInstrumentApp = null;
        private ILabRequestApp _labRequestApp = null;
        private readonly IRepository<QualityItemEntity> _qualityItemService = null;
        private readonly IRepository<QualityResultEntity> _qualityResultService = null;

        public LabTestApp(IUnitOfWork uow, IHttpContextAccessor httpContext, ILabInstrumentApp labInstrumentApp, ILabRequestApp labRequestApp)
        {
            _uow = uow;
            _service = _uow.GetRepository<LabTestEntity>();
            _reportService = _uow.GetRepository<LabReportEntity>();
            _qualityItemService = _uow.GetRepository<QualityItemEntity>();
            _labSheetService = _uow.GetRepository<LabSheetEntity>();
            _qualityResultService = _uow.GetRepository<QualityResultEntity>();
            _httpContext = httpContext;
            _labInstrumentApp = labInstrumentApp;
            _labRequestApp = labRequestApp;
        }

        public Task<List<LabTestEntity>> GetList(string instrumentId, DateTime testDate)
        {
            var expression = ExtLinq.True<LabTestEntity>();
            expression = expression.And(t => t.F_InstrumentId == instrumentId);
            expression = expression.And(t => t.F_TestDate == testDate);
            expression = expression.And(t => t.F_EnabledMark != false && t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public string GetDetailedList(string instrumentId, DateTime testDate)
        {
            var result = string.Empty;
            var data = from l in _uow.GetRepository<LabTestEntity>().IQueryable().Where(t => t.F_InstrumentId == instrumentId && t.F_TestDate == testDate && t.F_EnabledMark != false && t.F_DeleteMark != true)
                       join p in _uow.GetRepository<PatientEntity>().IQueryable() on l.F_PatientId equals p.F_Id
                       join s in _uow.GetRepository<LabSheetItemsEntity>().IQueryable() on l.F_RequestId equals s.F_RequestId
                       select new
                       {
                           id = l.F_Id,
                           instrumentId = l.F_InstrumentId,
                           testDate = l.F_TestDate,
                           testNo = l.F_TestNo,
                           testId = l.F_TestId,
                           barcode = l.F_Barcode,
                           sampleType = l.F_SampleType,
                           status = l.F_Status,
                           signInTime = l.F_SignInTime,
                           signInPerson = l.F_SignInPerson,
                           testTime = l.F_TestTime,
                           testPerson = l.F_TestPerson,
                           recieveResultTime = l.F_RecieveResultTime,
                           auditTime = l.F_AuditTime,
                           auditPerson = l.F_AuditPerson,
                           patientId = l.F_PatientId,
                           patientName = p.F_Name,
                           patientGender = p.F_Gender,
                           dialysisNo = p.F_DialysisNo,
                           birthDay = p.F_BirthDay,
                           diagnosis = p.F_Diagnosis,
                           requestId = l.F_RequestId,
                           itemCode = s.F_Code,
                           itemName = s.F_Name,
                           itemShortName = s.F_ShortName,
                           itemSorter = s.F_Sorter,
                           itemType = s.F_Type,
                           itemThirdPartyCode = s.F_ThirdPartyCode
                       };
            result = data.ToList().GroupBy(t => t.id, (key, r) =>
                {
                    var enumerable = r.ToList();
                    return new
                    {
                        id = key,
                        enumerable.FirstOrDefault()?.instrumentId,
                        enumerable.FirstOrDefault()?.testDate,
                        enumerable.FirstOrDefault()?.testNo,
                        enumerable.FirstOrDefault()?.testId,
                        enumerable.FirstOrDefault()?.barcode,
                        enumerable.FirstOrDefault()?.sampleType,
                        enumerable.FirstOrDefault()?.status,
                        enumerable.FirstOrDefault()?.signInTime,
                        enumerable.FirstOrDefault()?.signInPerson,
                        enumerable.FirstOrDefault()?.testTime,
                        enumerable.FirstOrDefault()?.testPerson,
                        enumerable.FirstOrDefault()?.recieveResultTime,
                        enumerable.FirstOrDefault()?.auditTime,
                        enumerable.FirstOrDefault()?.auditPerson,
                        enumerable.FirstOrDefault()?.patientId,
                        enumerable.FirstOrDefault()?.patientName,
                        enumerable.FirstOrDefault()?.patientGender,
                        enumerable.FirstOrDefault()?.dialysisNo,
                        enumerable.FirstOrDefault()?.birthDay,
                        patientAgeStr = enumerable.FirstOrDefault().birthDay.HasValue
                            ? ((DateTime.Now - enumerable.FirstOrDefault().birthDay.ToDate()).TotalDays.ToInt() / 365)
                            .ToString() + "岁"
                            : "   ",
                        enumerable.FirstOrDefault()?.diagnosis,
                        enumerable.FirstOrDefault()?.requestId,
                        items = enumerable.Select(child => new
                        {
                            child.itemCode,
                            child.itemName,
                            child.itemShortName,
                            child.itemSorter,
                            child.itemType,
                            child.itemThirdPartyCode,
                        }).OrderBy(f => f.itemSorter)
                    };
                }).OrderBy(c => c.testNo)
            .ToJson();
            return result;
        }

        public async Task<string> AuditTest(LabTestEntity entity)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
            var claimName = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.Name);
            var result = string.Empty;
            //查询子项
            var items = GetReport(entity.F_TestId);
            if (items.Count == 0)
            {
                result = "无报告明细";
            }
            else
            {
                //保存质控数据
                var itemDict = _labInstrumentApp.GetItem(entity.F_InstrumentId)
                    .Where(t => t.F_IsQualityItem == true)
                    .Select(t => new
                    {
                        t.F_Id,
                        t.F_IsQualityItem,
                        t.F_QualityItemId,
                        t.F_QualityItemName,
                        t.F_ConvertCoefficient,
                        t.F_KeepDecimal,
                        t.F_ResultType,
                        t.F_MachineId,
                        t.F_Code,
                        t.F_Name,
                        t.F_CnName,
                        t.F_EnName
                    }).ToList();
                var list = items.Join(itemDict, m => m.F_ItemId, n => n.F_Id, (m, n) => new
                {
                    result = m,
                    dict = n
                });
                if (list.Any())
                {
                    var sheet =( await _labRequestApp.GetFormByBarcode(entity.F_Barcode)).FirstOrDefault(t => t.F_RequestId == entity.F_RequestId);
                    if (sheet == null)
                    {
                        result = "申请单已删除";
                    }
                    else
                    {
                        //var LoginInfo = OperatorProvider.Provider.GetCurrent();
                        var currentTime = DateTime.Now;
                        foreach (var row in list)
                        {
                            var qualityItem = await _qualityItemService.FindEntityAsync(row.dict.F_QualityItemId);
                            if (qualityItem == null) continue;
                            var quality = new QualityResultEntity
                            {
                                F_Id = Common.GuId(),
                                F_CreatorTime = currentTime,
                                F_CreatorUserId = claim?.Value,
                                F_EnabledMark = true,
                                F_Flag = row.result.F_Flag,
                                F_ItemCode = qualityItem.F_ItemCode,
                                F_ItemName = qualityItem.F_ItemName,
                                F_ItemId = qualityItem.F_Id,
                                F_LowerCriticalValue = qualityItem.F_LowerCriticalValue,
                                F_LowerValue = qualityItem.F_LowerValue,
                                F_Pid = entity.F_PatientId,
                                F_ReportTime = currentTime
                            };
                            if (row.dict.F_ResultType ?? true)
                            {
                                quality.F_ResultType = true;
                                var temp = row.result.F_Result;
                                if (!temp.HasValue) continue;
                                int keep = 2;
                                if (row.dict.F_KeepDecimal.HasValue)
                                {
                                    keep = row.dict.F_KeepDecimal.ToInt();
                                    if (keep == 0) keep = 2;
                                }
                                float convertCoefficient = 1;
                                if (row.dict.F_ConvertCoefficient.HasValue)
                                {
                                    convertCoefficient = row.dict.F_ConvertCoefficient.ToFloat(keep);
                                }
                                quality.F_Result = (temp * convertCoefficient).ToFloat(keep).ToString();
                            }
                            else
                            {
                                quality.F_Result = row.result.F_ResultText;
                            }
                            quality.F_Unit = qualityItem.F_Unit;
                            quality.F_UpperCriticalValue = qualityItem.F_UpperCriticalValue;
                            quality.F_UpperValue = qualityItem.F_UpperValue;
                            await _qualityResultService.InsertAsync(quality);
                        }
                        entity.F_Status = 3;
                        entity.F_AuditPerson = claim?.Value;
                        entity.F_AuditTime = currentTime;
                        await _service.UpdatePartialAsync(entity);
                        sheet.F_AuditTime = currentTime;
                        sheet.F_AuditUserId = claim?.Value;
                        sheet.F_AuditUserName = claimName?.Value;
                        await _labSheetService.UpdatePartialAsync(sheet);
                    }
                }
            }
            return result;
        }

        public async Task<List<LabReportEntity>> SaveItem(string testId, string items)
        {
            List<LabReportEntity> list = new List<LabReportEntity>();
            var entity = await _service.FindEntityAsync(testId);
            var itemDict = _labInstrumentApp.GetItem(entity.F_InstrumentId).Select(t => new
            {
                t.F_Id,
                t.F_CriticalMaxValue,
                t.F_CriticalMinValue,
                t.F_KeepDecimal,
                t.F_ReferenceTextValue,
                t.F_ResultType
            }).ToList();

            //先删除 后添加
            await _reportService.DeleteAsync(t => t.F_TestId == entity.F_TestId);
            foreach (var item in items.ToJArrayObject())
            {
                var itemId = item.Value<string>("F_ItemId");
                if (itemId.IsEmpty()) continue;
                var findrow = itemDict.FirstOrDefault(t => t.F_Id == itemId);
                if (findrow == null) continue;
                var resultType = findrow.F_ResultType ?? true;
                var itemEntity = new LabReportEntity();
                itemEntity.Create();
                itemEntity.F_ItemId = itemId;
                var resultText = string.Empty;
                if (resultType)
                {
                    var resultStr = item.Value<string>("F_Result");
                    if (float.TryParse(resultStr, out float result))
                    {
                        int keepDecimal = findrow.F_KeepDecimal ?? 2;
                        result = result.ToFloat(2);
                        itemEntity.F_Result = result;
                        var lowRefStr = item.Value<string>("F_LowRef");
                        if (!lowRefStr.IsEmpty())
                        {
                            if (float.TryParse(lowRefStr, out float lowRef))
                            {
                                itemEntity.F_LowRef = lowRef;
                                if (result < lowRef)
                                {
                                    itemEntity.F_Flag = "L";
                                }
                            }

                        }
                        var upperRefStr = item.Value<string>("F_UpperRef");
                        if (!upperRefStr.IsEmpty())
                        {
                            if (float.TryParse(upperRefStr, out float upperRef))
                            {
                                itemEntity.F_UpperRef = upperRef;
                                if (result > upperRef)
                                {
                                    itemEntity.F_Flag = "H";
                                }
                            }

                        }
                        var sorterStr = item.Value<string>("F_Sorter");
                        if (!sorterStr.IsEmpty() && int.TryParse(sorterStr, out int sorter))
                        {
                            itemEntity.F_Sorter = sorter;
                        }
                        if (!findrow.F_CriticalMinValue.HasValue)
                        {
                            if (result < findrow.F_CriticalMinValue.ToFloat(3))
                            {
                                itemEntity.F_IsCritical = true;
                            }
                        }
                        if (!findrow.F_CriticalMaxValue.HasValue)
                        {
                            if (result > findrow.F_CriticalMaxValue.ToFloat(3))
                            {
                                itemEntity.F_IsCritical = true;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    resultText = item.Value<string>("F_ResultText");
                    if (resultText.IsEmpty()) continue;
                    itemEntity.F_ResultText = resultText;
                }
                itemEntity.F_Code = item.Value<string>("F_Code");
                itemEntity.F_MethodName = item.Value<string>("F_MethodName");
                itemEntity.F_Name = item.Value<string>("F_Name");
                itemEntity.F_ShortName = item.Value<string>("F_ShortName");
                itemEntity.F_Unit = item.Value<string>("F_Unit");
                itemEntity.F_TestId = entity.F_TestId;
                await _reportService.InsertAsync(itemEntity);
                list.Add(itemEntity);
            }
            //更改状态
            entity.F_Status = 2;
            await _service.UpdatePartialAsync(entity);
            return list;
        }

        /// <summary>
        /// 查询报告子项
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public List<LabReportEntity> GetReport(long testId)
        {
            var expression = ExtLinq.True<LabReportEntity>();
            expression = expression.And(t => t.F_TestId == testId);
            expression = expression.And(t => t.F_EnabledMark != false);
            return _reportService.IQueryable(expression).ToList();
        }

        public LabTestEntity GetForm(string keyValue)
        {
            return _service.FindEntity(keyValue);
        }

        public async Task<int> AssignSample(string instrumentId, DateTime testDate, string sheetId)
        {
            var sampleNo = 0;
            var sheet =await _labSheetService.FindEntityAsync(sheetId);
            var testEntity = new LabTestEntity();
            testEntity.Create();
            testEntity.F_Barcode = sheet.F_Barcode;
            testEntity.F_InstrumentId = instrumentId;
            testEntity.F_PatientId = sheet.F_Pid;
            testEntity.F_RequestId = sheet.F_RequestId;
            testEntity.F_SampleType = sheet.F_SampleType;
            testEntity.F_SignInPerson = testEntity.F_CreatorUserId;
            testEntity.F_SignInTime = testEntity.F_CreatorTime;
            testEntity.F_Status = 0;
            testEntity.F_TestDate = testDate;
            sampleNo = await CreateSampleNo(instrumentId, testDate);
            testEntity.F_TestNo = sampleNo;
            testEntity.F_TestId = long.Parse(Common.CreateNo());
            await _service.InsertAsync(testEntity);
            sheet.F_Status = 4;
            await _labSheetService.UpdatePartialAsync(sheet);
            return sampleNo;
        }

        /// <summary>
        /// 生成样本号
        /// </summary>
        /// <param name="instrumentId"></param>
        /// <param name="testDate"></param>
        /// <returns></returns>
        private async Task<int> CreateSampleNo(string instrumentId, DateTime testDate)
        {
            var result = 0;
            var expression = ExtLinq.True<TestSampleNoEntity>();
            expression = expression.And(t => t.InstrumentId == instrumentId);
            expression = expression.And(t => t.TestDate == testDate);
            var entity = await _uow.GetRepository<TestSampleNoEntity>().FindEntityAsync(expression);
            if (entity == null)
            {
                entity = new TestSampleNoEntity
                {
                    TestDate = testDate,
                    InstrumentId = instrumentId,
                    SampleNo = 1,
                    CreateTime = DateTime.Now
                };
                await _uow.GetRepository<TestSampleNoEntity>().InsertAsync(entity);
            }
            else
            {
                entity.SampleNo += 1;
                await _uow.GetRepository<TestSampleNoEntity>().UpdatePartialAsync(entity);
            }
            result = entity.SampleNo;
            return result;
        }
    }
}
