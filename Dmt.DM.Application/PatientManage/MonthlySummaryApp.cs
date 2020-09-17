using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IMonthlySummaryApp : IScopedAppService
    {
        Task<List<MonthlySummaryEntity>> GetList(string patientId, string month = null);
        Task<MonthlySummaryEntity> GetForm(string patientId, string month = null);
        Task<MonthlySummaryEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(MonthlySummaryEntity entity);
        Task<int> SubmitForm(MonthlySummaryEntity entity, string keyValue);
    }

    public class MonthlySummaryApp : IMonthlySummaryApp
    {
        private readonly IRepository<MonthlySummaryEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;
        private readonly string _currentMonth = DateTime.Today.ToDateString().Substring(0, 7);

        public MonthlySummaryApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<MonthlySummaryEntity>();
            _httpContext = httpContext;
        }

        public Task<List<MonthlySummaryEntity>> GetList(string patientId, string month = null)
        {
            var expression = ExtLinq.True<MonthlySummaryEntity>();
            expression = expression.And(t => t.F_Pid == patientId);
            if (month.IsEmpty())
            {
                month = _currentMonth;
            }
            expression = expression.And(t => t.F_Month == month);
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public Task<MonthlySummaryEntity> GetForm(string patientId, string month = null)
        {
            var expression = ExtLinq.True<MonthlySummaryEntity>();
            expression = expression.And(t => t.F_Pid == patientId);
            if (month.IsEmpty())
            {
                month = _currentMonth;
            }
            expression = expression.And(t => t.F_Month == month);
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            var entity = _service.FindEntity(expression);
            if (entity == null) //生成记录
            {
                entity = CreateDate(patientId, month);
            }
            return Task.FromResult(entity);
        }

        private MonthlySummaryEntity CreateDate(string patientId, string month = null)
        {
            if (month.IsEmpty())
            {
                month = _currentMonth;
            }
            var entity = _service.FindEntity(t => t.F_Pid == patientId && t.F_Month == month);
            if (entity == null)
            {
                entity = new MonthlySummaryEntity {F_Pid = patientId, F_Month = month};
            }
            //entity.Create();
            var startDate = DateTime.Parse(month + "-01");
            var endDate = startDate.AddMonths(1).AddDays(-1);//本月底日期
            var endDate2 = startDate.AddMonths(1);//下月一号
            var patientInfo = _uow.GetRepository<PatientEntity>().FindEntity(patientId);
            var visitRecords = _uow.GetRepository<PatVisitEntity>().IQueryable(t => t.F_Pid == patientId && t.F_VisitDate >= startDate && t.F_VisitDate <= endDate && t.F_DialysisEndTime != null
                                                                                                    && t.F_DialysisStartTime != null && t.F_EnabledMark != false && t.F_DeleteMark != true)
                                    .Select(t => new
                                    {
                                        t.F_VisitDate,
                                        t.F_Ca,
                                        t.F_DialysisType,
                                        t.F_VascularAccess,
                                        t.F_AccessName,
                                        t.F_BloodSpeed,
                                        t.F_DialysisEndTime,
                                        t.F_DialysisHours,
                                        t.F_DialysisStartTime,
                                        t.F_DialyzerType1,
                                        t.F_DialyzerType2,
                                        t.F_Hco3,
                                        t.F_HeparinAddAmount,
                                        t.F_HeparinAddSpeedUnit,
                                        t.F_HeparinAmount,
                                        t.F_HeparinType,
                                        t.F_HeparinUnit,
                                        t.F_K,
                                        t.F_LowCa,
                                        t.F_Na,
                                        t.F_WeightST,
                                        t.F_WeightSXTH,
                                        t.F_WeightTH,
                                        t.F_WeightTQ,
                                        t.F_WeightYT
                                    })
                                    .ToList()
                                    .Select(t => new
                                    {
                                        VisitDate = t.F_VisitDate.ToDate(),
                                        DialysisType = t.F_DialysisType,
                                        VascularAccess = t.F_VascularAccess,
                                        AccessName = t.F_AccessName,
                                        BloodSpeed = t.F_BloodSpeed,
                                        DialysisStartTime = t.F_DialysisStartTime.ToDate(),
                                        DialysisEndTime = t.F_DialysisEndTime.ToDate(),
                                        DialysisHours = t.F_DialysisHours.ToFloat(1),
                                        DialyzerType1 = t.F_DialyzerType1,
                                        DialyzerType2 = t.F_DialyzerType2,
                                        Hco3 = t.F_Hco3,
                                        Ca = t.F_Ca,
                                        K = t.F_K,
                                        LowCa = t.F_LowCa,
                                        Na = t.F_Na,
                                        HeparinType = t.F_HeparinType,
                                        HeparinAmount = t.F_HeparinAmount,
                                        HeparinUnit = t.F_HeparinUnit,
                                        HeparinAddAmount = t.F_HeparinAddAmount,
                                        HeparinAddSpeedUnit = t.F_HeparinAddSpeedUnit,
                                        WeightST = t.F_WeightST,
                                        WeightSXTH = t.F_WeightSXTH,
                                        WeightTH = t.F_WeightTH,
                                        WeightTQ = t.F_WeightTQ,
                                        WeightYT = t.F_WeightYT
                                    })
                                    .ToList();
            if (visitRecords.Count == 0) return entity;
            #region 透析次数及透析器
            entity.F_TotalCount = visitRecords.Count; //总透析次数
            var findRows = visitRecords.Where(t => t.DialysisType == "HD" || t.DialysisType == "HFHD"); //包含高通量HD
            if (findRows.Any())
            {
                entity.F_HDTimes = findRows.Count();
                var dialyzerTypeId = findRows.OrderByDescending(t => t.VisitDate).Select(t => t.DialyzerType1).First();
                if (!dialyzerTypeId.IsEmpty())
                {
                    var material = _uow.GetRepository<MaterialEntity>().FindEntity(dialyzerTypeId);
                    entity.F_HDDialyzerType = material == null ? null : material.F_Id;
                }
            }

            findRows = visitRecords.Where(t => t.DialysisType == "HF");
            if (findRows.Any())
            {
                entity.F_HFTimes = findRows.Count();
                var dialyzerTypeId = findRows.OrderByDescending(t => t.VisitDate).Select(t => t.DialyzerType1).First();
                if (!dialyzerTypeId.IsEmpty())
                {
                    var material = _uow.GetRepository<MaterialEntity>().FindEntity(dialyzerTypeId);
                    entity.F_HFDialyzerType = material == null ? null : material.F_Id;
                }
            }

            findRows = visitRecords.Where(t => t.DialysisType == "HDF");
            if (findRows.Any())
            {
                entity.F_HDFTimes = findRows.Count();
                var dialyzerTypeId = findRows.OrderByDescending(t => t.VisitDate).Select(t => t.DialyzerType1).First();
                if (!dialyzerTypeId.IsEmpty())
                {
                    var material = _uow.GetRepository<MaterialEntity>().FindEntity(dialyzerTypeId);
                    entity.F_HDFDialyzerType = material == null ? null : material.F_Id;
                }
            }

            findRows = visitRecords.Where(t => t.DialysisType == "HDHP");
            if (findRows.Any())
            {
                entity.F_HDHPTimes = findRows.Count();
                var dialyzerTypeId = findRows.OrderByDescending(t => t.VisitDate).Select(t => t.DialyzerType1).First();
                if (!dialyzerTypeId.IsEmpty())
                {
                    var material = _uow.GetRepository<MaterialEntity>().FindEntity(dialyzerTypeId);
                    entity.F_HDHPDialyzerType = material == null ? null : material.F_Id;
                }
            }
            #endregion

            //干体重
            var ideaWeightLogs = _uow.GetRepository<IdeaWeightEntity>().IQueryable(t => t.F_Pid == patientId && t.F_CreatorTime >= startDate && t.F_CreatorTime <= endDate2 && t.F_IdealWeight != null && t.F_EnabledMark != false && t.F_DeleteMark != true)
                .Select(t => new
                {
                    date = t.F_CreatorTime,
                    value = t.F_IdealWeight
                })
                .ToList();
            if (ideaWeightLogs.Any())
            {
                //entity.F_IdeaWeight = ideaWeightLogs.Average(t => t.value.ToFloat(2)); // 可能包含误操作 弃用
                entity.F_IdeaWeight = ideaWeightLogs.OrderByDescending(t => t.date).FirstOrDefault().value.ToFloat(2);  //选用本月最后一次数据
            }
            else
            {
                if (patientInfo.F_IdealWeight != null)
                {
                    entity.F_IdeaWeight = patientInfo.F_IdealWeight.ToFloat(2);
                }
            }

            //尿量  从评估单取数据
            var urineVolumeLogs = _uow.GetRepository<EvaluationEntity>().IQueryable(t => t.F_Pid == patientId && t.F_VisitDate >= startDate && t.F_VisitDate <= endDate && t.Nlvalue1 != null && t.F_EnabledMark != false && t.F_DeleteMark != true)
                                    .Select(t => new
                                    {
                                        t.Nlvalue1
                                    })
                                    .ToList();
            if (urineVolumeLogs.Count > 0)
            {
                entity.F_UrineVolume = urineVolumeLogs.Average(t => t.Nlvalue1.ToFloat(0));
            }

            //血管通路 
            var lastVisitRecord = visitRecords.OrderByDescending(t => t.VisitDate).First();
            entity.F_VascularAccess = lastVisitRecord.VascularAccess;
            //抗凝方式  剂量
            var filterRow = visitRecords.Where(t => t.HeparinType != null && t.HeparinAmount != null).OrderByDescending(t => t.VisitDate).FirstOrDefault();
            if (filterRow != null)
            {
                var drugInfo = _uow.GetRepository<DrugsEntity>().FindEntity(t => t.F_Id == filterRow.HeparinType);
                entity.F_HeparinType = drugInfo.F_Id;
                entity.F_HeparinAmount = filterRow.HeparinAmount.ToFloat(3);
                entity.F_HeparinUnit = filterRow.HeparinUnit;
                //+ filterRow.HeparinUnit;
            }
            //最大血流速
            findRows = visitRecords.Where(t => t.BloodSpeed != null);
            if (findRows.Any())
            {
                entity.F_BloodSpeed = findRows.Max(t => t.BloodSpeed.ToFloat(0));
            }
            //透析液配方
            findRows = visitRecords.Where(t => t.Ca != null && t.K != null).OrderByDescending(t => t.VisitDate);
            if (findRows.Any())
            {
                var firstData = findRows.FirstOrDefault();
                if (firstData.Ca != null)
                {
                    entity.F_TXYCa = firstData.Ca.ToFloat(2);
                }
                if (firstData.K != null)
                {
                    entity.F_TXYK = firstData.K.ToFloat(2);
                }
                if (firstData.Hco3 != null)
                {
                    entity.F_TXYHco3 = firstData.Hco3.ToFloat(2);
                }
            }

            //透析时长 平均值
            var estimateHoursLogs = visitRecords
                .Select(t => new
                {
                    tickValue = (t.DialysisEndTime.ToDate() - t.DialysisStartTime.ToDate()).TotalHours
                })
                .Where(t => t.tickValue > 0 && t.tickValue < 24);

            if (estimateHoursLogs.Any())
            {
                entity.F_EstimateHours = (float)estimateHoursLogs.Average(t => t.tickValue).ToDouble(1);
            }
            //透析频次 每周
            entity.F_WeekTimes = Math.Round(visitRecords.Count * 7 / (endDate2 - startDate).TotalDays, 0, MidpointRounding.ToEven).ToInt();
            if (entity.F_WeekTimes == 0)
            {
                entity.F_WeekTimes = 1;
            }

            //化验结果
            var qualityResultLogs = _uow.GetRepository<QualityResultEntity>().IQueryable(t => t.F_Pid == patientId && t.F_ReportTime >= startDate && t.F_ReportTime <= endDate2 && t.F_EnabledMark != false && t.F_DeleteMark != true)
                        .Select(t => new
                        {
                            t.F_ReportTime,
                            t.F_ItemCode,
                                //t.F_ItemId,
                                t.F_ItemName,
                            t.F_Result,
                            t.F_ResultType,
                            t.F_Unit
                        }).ToList();
            //血常规
            var findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "HGB");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (float.TryParse(row.F_Result, out float result))
                {
                    entity.F_Hb = result;
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "RBC");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (float.TryParse(row.F_Result, out float result))
                {
                    entity.F_RBC = result;
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "HCT");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (float.TryParse(row.F_Result, out float result))
                {
                    entity.F_HCT = result;
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "WBC");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (float.TryParse(row.F_Result, out float result))
                {
                    entity.F_WBC = result;
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "PLT");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (float.TryParse(row.F_Result, out float result))
                {
                    entity.F_PLT = result;
                }
            }

            //透前和透后项目
            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "CREA");
            if (findQualityRows.Any())
            {
                var rowCount = findQualityRows.Count();
                if (rowCount >= 2)  //由两次数据
                {
                    var rows = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).OrderBy(t => t.F_ReportTime);
                    if (float.TryParse(rows.First().F_Result, out float result))
                    {
                        entity.F_Scr = result;
                    }
                    if (float.TryParse(rows.Last().F_Result, out result))
                    {
                        entity.F_Scr1 = result;
                    }
                }
                else
                {
                    var row = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).FirstOrDefault();
                    var reportDate = row.F_ReportTime.ToDate().Date;
                    //查询当天透析记录
                    var findvisit = visitRecords.FirstOrDefault(t => t.VisitDate == reportDate);
                    if (findvisit != null)
                    {
                        if (row.F_ReportTime.ToDate() < findvisit.DialysisStartTime.ToDate()) //透前
                        {
                            if (float.TryParse(row.F_Result, out float result))
                            {
                                entity.F_Scr = result;
                            }
                        }
                        else
                        {
                            if (float.TryParse(row.F_Result, out float result))  ////透后
                            {
                                entity.F_Scr1 = result;
                            }
                        }
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "UREA");
            if (findQualityRows.Any())
            {
                var rowCount = findQualityRows.Count();
                if (rowCount >= 2)  //由两次数据
                {
                    var rows = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).OrderBy(t => t.F_ReportTime);
                    if (float.TryParse(rows.First().F_Result, out float result))
                    {
                        entity.F_Urea = result;
                    }
                    if (float.TryParse(rows.Last().F_Result, out result))
                    {
                        entity.F_Urea1 = result;
                    }
                }
                else
                {
                    var row = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).FirstOrDefault();
                    var reportDate = row.F_ReportTime.ToDate().Date;
                    //查询当天透析记录
                    var findvisit = visitRecords.FirstOrDefault(t => t.VisitDate == reportDate);
                    if (findvisit != null)
                    {
                        if (row.F_ReportTime.ToDate() < findvisit.DialysisStartTime.ToDate()) //透前
                        {
                            if (float.TryParse(row.F_Result, out float result))
                            {
                                entity.F_Urea = result;
                            }
                        }
                        else
                        {
                            if (float.TryParse(row.F_Result, out float result))  ////透后
                            {
                                entity.F_Urea1 = result;
                            }
                        }
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "UA");
            if (findQualityRows.Any())
            {
                var rowCount = findQualityRows.Count();
                if (rowCount >= 2)  //由两次数据
                {
                    var rows = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).OrderBy(t => t.F_ReportTime);
                    if (float.TryParse(rows.First().F_Result, out float result))
                    {
                        entity.F_UA = result;
                    }
                    if (float.TryParse(rows.Last().F_Result, out result))
                    {
                        entity.F_UA1 = result;
                    }
                }
                else
                {
                    var row = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).FirstOrDefault();
                    var reportDate = row.F_ReportTime.ToDate().Date;
                    //查询当天透析记录
                    var findvisit = visitRecords.FirstOrDefault(t => t.VisitDate == reportDate);
                    if (findvisit != null)
                    {
                        if (row.F_ReportTime.ToDate() < findvisit.DialysisStartTime.ToDate()) //透前
                        {
                            if (float.TryParse(row.F_Result, out float result))
                            {
                                entity.F_UA = result;
                            }
                        }
                        else
                        {
                            if (float.TryParse(row.F_Result, out float result))  ////透后
                            {
                                entity.F_UA1 = result;
                            }
                        }
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "K");
            if (findQualityRows.Any())
            {
                var rowCount = findQualityRows.Count();
                if (rowCount >= 2)  //由两次数据
                {
                    var rows = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).OrderBy(t => t.F_ReportTime);
                    if (float.TryParse(rows.First().F_Result, out float result))
                    {
                        entity.F_K = result;
                    }
                    if (float.TryParse(rows.Last().F_Result, out result))
                    {
                        entity.F_K1 = result;
                    }
                }
                else
                {
                    var row = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).FirstOrDefault();
                    var reportDate = row.F_ReportTime.ToDate().Date;
                    //查询当天透析记录
                    var findvisit = visitRecords.FirstOrDefault(t => t.VisitDate == reportDate);
                    if (findvisit != null)
                    {
                        if (row.F_ReportTime.ToDate() < findvisit.DialysisStartTime.ToDate()) //透前
                        {
                            if (float.TryParse(row.F_Result, out float result))
                            {
                                entity.F_K = result;
                            }
                        }
                        else
                        {
                            if (float.TryParse(row.F_Result, out float result))  ////透后
                            {
                                entity.F_K1 = result;
                            }
                        }
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "Na");
            if (findQualityRows.Any())
            {
                var rowCount = findQualityRows.Count();
                if (rowCount >= 2)  //由两次数据
                {
                    var rows = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).OrderBy(t => t.F_ReportTime);
                    if (float.TryParse(rows.First().F_Result, out float result))
                    {
                        entity.F_Na = result;
                    }
                    if (float.TryParse(rows.Last().F_Result, out result))
                    {
                        entity.F_Na1 = result;
                    }
                }
                else
                {
                    var row = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).FirstOrDefault();
                    var reportDate = row.F_ReportTime.ToDate().Date;
                    //查询当天透析记录
                    var findvisit = visitRecords.FirstOrDefault(t => t.VisitDate == reportDate);
                    if (findvisit != null)
                    {
                        if (row.F_ReportTime.ToDate() < findvisit.DialysisStartTime.ToDate()) //透前
                        {
                            if (float.TryParse(row.F_Result, out float result))
                            {
                                entity.F_Na = result;
                            }
                        }
                        else
                        {
                            if (float.TryParse(row.F_Result, out float result))  ////透后
                            {
                                entity.F_Na1 = result;
                            }
                        }
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "Cl");
            if (findQualityRows.Any())
            {
                var rowCount = findQualityRows.Count();
                if (rowCount >= 2)  //由两次数据
                {
                    var rows = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).OrderBy(t => t.F_ReportTime);
                    if (float.TryParse(rows.First().F_Result, out float result))
                    {
                        entity.F_Cl = result;
                    }
                    if (float.TryParse(rows.Last().F_Result, out result))
                    {
                        entity.F_Cl1 = result;
                    }
                }
                else
                {
                    var row = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).FirstOrDefault();
                    var reportDate = row.F_ReportTime.ToDate().Date;
                    //查询当天透析记录
                    var findvisit = visitRecords.FirstOrDefault(t => t.VisitDate == reportDate);
                    if (findvisit != null)
                    {
                        if (row.F_ReportTime.ToDate() < findvisit.DialysisStartTime.ToDate()) //透前
                        {
                            if (float.TryParse(row.F_Result, out float result))
                            {
                                entity.F_Cl = result;
                            }
                        }
                        else
                        {
                            if (float.TryParse(row.F_Result, out float result))  ////透后
                            {
                                entity.F_Cl1 = result;
                            }
                        }
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "CO2");
            if (findQualityRows.Any())
            {
                var rowCount = findQualityRows.Count();
                if (rowCount >= 2)  //由两次数据
                {
                    var rows = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).OrderBy(t => t.F_ReportTime);
                    if (float.TryParse(rows.First().F_Result, out float result))
                    {
                        entity.F_HCO3 = result;
                    }
                    if (float.TryParse(rows.Last().F_Result, out result))
                    {
                        entity.F_HCO31 = result;
                    }
                }
                else
                {
                    var row = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).FirstOrDefault();
                    var reportDate = row.F_ReportTime.ToDate().Date;
                    //查询当天透析记录
                    var findvisit = visitRecords.FirstOrDefault(t => t.VisitDate == reportDate);
                    if (findvisit != null)
                    {
                        if (row.F_ReportTime.ToDate() < findvisit.DialysisStartTime.ToDate()) //透前
                        {
                            if (float.TryParse(row.F_Result, out float result))
                            {
                                entity.F_HCO3 = result;
                            }
                        }
                        else
                        {
                            if (float.TryParse(row.F_Result, out float result))  ////透后
                            {
                                entity.F_HCO31 = result;
                            }
                        }
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "TCA");
            if (findQualityRows.Any())
            {
                var rowCount = findQualityRows.Count();
                if (rowCount >= 2)  //由两次数据
                {
                    var rows = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).OrderBy(t => t.F_ReportTime);
                    if (float.TryParse(rows.First().F_Result, out float result))
                    {
                        entity.F_Ca = result;
                    }
                    if (float.TryParse(rows.Last().F_Result, out result))
                    {
                        entity.F_Ca1 = result;
                    }
                }
                else
                {
                    var row = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).FirstOrDefault();
                    var reportDate = row.F_ReportTime.ToDate().Date;
                    //查询当天透析记录
                    var findvisit = visitRecords.FirstOrDefault(t => t.VisitDate == reportDate);
                    if (findvisit != null)
                    {
                        if (row.F_ReportTime.ToDate() < findvisit.DialysisStartTime.ToDate()) //透前
                        {
                            if (float.TryParse(row.F_Result, out float result))
                            {
                                entity.F_Ca = result;
                            }
                        }
                        else
                        {
                            if (float.TryParse(row.F_Result, out float result))  ////透后
                            {
                                entity.F_Ca1 = result;
                            }
                        }
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "P");
            if (findQualityRows.Any())
            {
                var rowCount = findQualityRows.Count();
                if (rowCount >= 2)  //由两次数据
                {
                    var rows = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).OrderBy(t => t.F_ReportTime);
                    if (float.TryParse(rows.First().F_Result, out float result))
                    {
                        entity.F_P = result;
                    }
                    if (float.TryParse(rows.Last().F_Result, out result))
                    {
                        entity.F_P1 = result;
                    }
                }
                else
                {
                    var row = findQualityRows.Select(t => new
                    {
                        t.F_ReportTime,
                        t.F_Result
                    }).FirstOrDefault();
                    var reportDate = row.F_ReportTime.ToDate().Date;
                    //查询当天透析记录
                    var findvisit = visitRecords.FirstOrDefault(t => t.VisitDate == reportDate);
                    if (findvisit != null)
                    {
                        if (row.F_ReportTime.ToDate() < findvisit.DialysisStartTime.ToDate()) //透前
                        {
                            if (float.TryParse(row.F_Result, out float result))
                            {
                                entity.F_P = result;
                            }
                        }
                        else
                        {
                            if (float.TryParse(row.F_Result, out float result))  ////透后
                            {
                                entity.F_P1 = result;
                            }
                        }
                    }
                }
            }

            //其他
            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "FER");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (float.TryParse(row.F_Result, out float result))
                {
                    entity.F_FER = result;
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "TS");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (float.TryParse(row.F_Result, out float result))
                {
                    entity.F_TS = result;
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "iPTH");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (float.TryParse(row.F_Result, out float result))
                {
                    entity.F_iPTH = result;
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "CRP");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (float.TryParse(row.F_Result, out float result))
                {
                    entity.F_CRP = result;
                }
            }

            //计算 URR   % spKt/V
            if (entity.F_Urea != null && entity.F_Urea1 != null)
            {
                findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "UREA");
                if (findQualityRows.Any())
                {
                    var reportDate = findQualityRows.Last().F_ReportTime.ToDate().Date;
                    var findvisit = visitRecords.FirstOrDefault(t => t.VisitDate == reportDate);
                    if (findvisit != null)
                    {
                        if (findvisit.WeightTH != null && findvisit.WeightTH != null)
                        {
                            var preUrea = entity.F_Urea.ToFloat(2);
                            var postUrea = entity.F_Urea1.ToFloat(2);
                            var duration = (float)(findvisit.DialysisEndTime - findvisit.DialysisStartTime).TotalHours;
                            var ultrafiltrationVolume = findvisit.WeightTH.ToFloat(2) - findvisit.WeightTH.ToFloat(2);
                            var postWeight = findvisit.WeightTH.ToFloat(2);
                            if (preUrea > 0 && postUrea > 0 && preUrea > postUrea && duration > 0 && duration < 24 && ultrafiltrationVolume > 0 && ultrafiltrationVolume < 20 && postWeight > 0)
                            {
                                entity.F_spKtV = (float)(-Math.Log(postUrea / preUrea - 0.008 * duration) + (4 - 3.5 * postUrea / preUrea) * ultrafiltrationVolume / postWeight);
                                entity.F_URR = 100 * (1 - postUrea / preUrea);
                            }
                        }
                    }
                }
            }

            //传筛
            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "Anti-HCV");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (!row.F_Result.IsEmpty())
                {
                    if (row.F_Result.Contains("+") || row.F_Result.Contains("阳"))
                    {
                        entity.F_HCV = true;
                    }
                    else
                    {
                        entity.F_HCV = false;
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "HIV");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (!row.F_Result.IsEmpty())
                {
                    if (row.F_Result.Contains("+") || row.F_Result.Contains("阳"))
                    {
                        entity.F_HIV = true;
                    }
                    else
                    {
                        entity.F_HIV = false;
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "梅毒");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (!row.F_Result.IsEmpty())
                {
                    if (row.F_Result.Contains("+") || row.F_Result.Contains("阳"))
                    {
                        entity.F_RPR = true;
                    }
                    else
                    {
                        entity.F_RPR = false;
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "HBsAg");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (!row.F_Result.IsEmpty())
                {
                    if (row.F_Result.Contains("+") || row.F_Result.Contains("阳"))
                    {
                        entity.F_HBsAg = true;
                    }
                    else
                    {
                        entity.F_HBsAg = false;
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "Anti-HBs");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (!row.F_Result.IsEmpty())
                {
                    if (row.F_Result.Contains("+") || row.F_Result.Contains("阳"))
                    {
                        entity.F_HBsAb = true;
                    }
                    else
                    {
                        entity.F_HBsAb = false;
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "HBeAg");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (!row.F_Result.IsEmpty())
                {
                    if (row.F_Result.Contains("+") || row.F_Result.Contains("阳"))
                    {
                        entity.F_HBeAg = true;
                    }
                    else
                    {
                        entity.F_HBeAg = false;
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "Anti-HBe");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (!row.F_Result.IsEmpty())
                {
                    if (row.F_Result.Contains("+") || row.F_Result.Contains("阳"))
                    {
                        entity.F_HBeAb = true;
                    }
                    else
                    {
                        entity.F_HBeAb = false;
                    }
                }
            }

            findQualityRows = qualityResultLogs.Where(t => t.F_ItemCode == "Anti-HBc");
            if (findQualityRows.Any())
            {
                var row = findQualityRows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault();
                if (!row.F_Result.IsEmpty())
                {
                    if (row.F_Result.Contains("+") || row.F_Result.Contains("阳"))
                    {
                        entity.F_HBcAb = true;
                    }
                    else
                    {
                        entity.F_HBcAb = false;
                    }
                }
            }

            //服药记录
            var drugsLogs = _uow.GetRepository<OrdersExecLogEntity>().IQueryable(t => t.F_Pid == patientId && t.F_NurseOperatorTime >= startDate && t.F_NurseOperatorTime <= endDate2 && t.F_OrderType == "药疗")
                .GroupBy(t => t.F_OrderCode)
                .Select(g => new
                {
                    OrderCode = g.FirstOrDefault().F_OrderCode,
                    OrderText = g.FirstOrDefault().F_OrderText,
                    OrderUnit = g.FirstOrDefault().F_OrderUnitSpec,
                    OrderAmount = g.Sum(r => r.F_OrderAmount)
                })
                .Join(
                    _uow.GetRepository<DrugsEntity>().IQueryable(t => t.F_DrugEfficacy != null & t.F_EnabledMark != false && t.F_DeleteMark != true)
                    .Select(t => new
                    {
                        Id = t.F_Id,
                        Efficacy = t.F_DrugEfficacy
                    })
                    , o => o.OrderCode
                    , d => d.Id,
                    (o, d) => new
                    {
                        o.OrderCode,
                        o.OrderText,
                        o.OrderAmount,
                        o.OrderUnit,
                        d.Efficacy
                    }
                )
                .ToList();
            if (drugsLogs.Count > 0)
            {
                var drugssb = new StringBuilder();
                foreach (var item in drugsLogs)
                {
                    drugssb.AppendLine(item.Efficacy + "     " + item.OrderText + "     " + item.OrderAmount + item.OrderUnit);
                }
                entity.F_DrugSummary = drugssb.ToString();
            }

            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);

            if (entity.F_Id.IsEmpty())
            {
                entity.Create();
                entity.F_CreatorUserId = claim?.Value;
                _service.Insert(entity);
            }
            else
            {
                _service.Update(entity);
            }
            return entity;
        }

        public Task<MonthlySummaryEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(MonthlySummaryEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> SubmitForm(MonthlySummaryEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                return _service.UpdateAsync(entity);
            }
            else
            {
                entity.Create();
                return _service.InsertAsync(entity);
            }
        }

    }
}
