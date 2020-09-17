using Dmt.DM.Code;
using Dmt.DM.Code.Excel;
using Dmt.DM.Domain.Entity.DataReporting;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dmt.DM.Application.PatientManage
{
    public interface IDataReportApp :IScopedAppService
    {
        /// <summary>
        /// 检查文件是否存在，没有则新建一份
        /// </summary>
        /// <returns></returns>
        string CopyModelFile();

        object CreateData(string keyValue);
        void SubmitData(string keyValue);
    }

    public class DataReportApp : IDataReportApp
    {
        private IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;

        public DataReportApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _httpContext = httpContext;
        }

        List<Data1Entity> list1 = new List<Data1Entity>();
        List<Data2Entity> list2 = new List<Data2Entity>();
        List<Data3Entity> list3 = new List<Data3Entity>();
        List<Data4Entity> list4 = new List<Data4Entity>();
        List<Data5Entity> list5 = new List<Data5Entity>();
        List<Data6Entity> list6 = new List<Data6Entity>();
        List<Data7Entity> list7 = new List<Data7Entity>();
        List<Data8Entity> list8 = new List<Data8Entity>();
        List<Data9Entity> list9 = new List<Data9Entity>();
        List<Data10Entity> list10 = new List<Data10Entity>();
        List<Data11Entity> list11 = new List<Data11Entity>();
        List<Data12Entity> list12 = new List<Data12Entity>();
        List<Data13Entity> list13 = new List<Data13Entity>();
        List<Data14Entity> list14 = new List<Data14Entity>();
        List<Data15Entity> list15 = new List<Data15Entity>();
        List<Data16Entity> list16 = new List<Data16Entity>();
        List<Data17Entity> list17 = new List<Data17Entity>();
        List<Data18Entity> list18 = new List<Data18Entity>();
        List<Data19Entity> list19 = new List<Data19Entity>();
        List<Data20Entity> list20 = new List<Data20Entity>();
        List<Data21Entity> list21 = new List<Data21Entity>();
        List<Data22Entity> list22 = new List<Data22Entity>();
        List<Data23Entity> list23 = new List<Data23Entity>();
        List<Data24Entity> list24 = new List<Data24Entity>();
        List<Data25Entity> list25 = new List<Data25Entity>();
        List<Data26Entity> list26 = new List<Data26Entity>();
        List<Data27Entity> list27 = new List<Data27Entity>();

        /// <summary>
        /// 检查文件是否存在，没有则新建一份
        /// </summary>
        /// <returns></returns>
        public string CopyModelFile()
        {
            var targetFileName = Path.Combine(AppConsts.AppRootPath, "upload", "datareport",
                DateTime.Now.ToDateString() + ".xls");
            if (FileHelper.IsExistFile(targetFileName))
            {
                return targetFileName;
            }
            else
            {
                var modelFilePath = Path.Combine(AppConsts.AppRootPath, "upload", "datareport", "患者信息导入模板.xls");
                if (FileHelper.IsExistFile(modelFilePath))
                {
                    FileHelper.CopyFile(modelFilePath, targetFileName);
                    return targetFileName;
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 填充Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        private string FillDataToExcel<T>(List<T> list, string sheetName = "患者基本信息") where T : class
        {
            string fileName = CopyModelFile();
            NPOIExcel excel = new NPOIExcel();
            excel.ToExcel<T>(list, sheetName, fileName);
            return fileName;
        }

        public object CreateData(string keyValue)
        {
            var json = keyValue.ToJObject();
            DateTime dtStart = json.Value<DateTime>("startDate");
            DateTime dtEnd = json.Value<DateTime>("endDate");
            string index = json.Value<string>("index");
            object data = null;
            switch (index)
            {
                case "1":
                    data = CreateData1(dtStart, dtEnd);
                    break;
                case "2":
                    data = CreateData2(dtStart, dtEnd);
                    break;
                case "3":
                    data = CreateData3(dtStart, dtEnd);
                    break;
                case "4":
                    data = CreateData4(dtStart, dtEnd);
                    break;
                case "5":
                    data = CreateData5(dtStart, dtEnd);
                    break;
                case "6":
                    data = CreateData6(dtStart, dtEnd);
                    break;
                case "7":
                    data = CreateData7(dtStart, dtEnd);
                    break;
                case "8":
                    data = CreateData8(dtStart, dtEnd);
                    break;
                case "9":
                    data = CreateData9(dtStart, dtEnd);
                    break;
                case "10":
                    data = CreateData10(dtStart, dtEnd);
                    break;
                case "11":
                    data = CreateData11(dtStart, dtEnd);
                    break;
                case "12":
                    data = CreateData12(dtStart, dtEnd);
                    break;
                case "13":
                    data = CreateData13(dtStart, dtEnd);
                    break;
                case "14":
                    data = CreateData14(dtStart, dtEnd);
                    break;
                case "15":
                    data = CreateData15(dtStart, dtEnd);
                    break;
                case "16":
                    data = CreateData16(dtStart.Date, dtEnd.Date.AddDays(1));
                    break;
                case "17":
                    data = CreateData17(dtStart, dtEnd);
                    break;
                case "18":
                    data = CreateData18(dtStart, dtEnd);
                    break;
                case "19":
                    data = CreateData19(dtStart, dtEnd);
                    break;
                case "20":
                    data = CreateData20(dtStart, dtEnd);
                    break;
                case "21":
                    data = CreateData21(dtStart, dtEnd);
                    break;
                case "22":
                    data = CreateData22(dtStart, dtEnd);
                    break;
                case "23":
                    data = CreateData23(dtStart, dtEnd);
                    break;
                case "24":
                    data = CreateData24(dtStart, dtEnd);
                    break;
                case "25":
                    data = CreateData25(dtStart, dtEnd);
                    break;
                case "26":
                    data = CreateData26(dtStart, dtEnd);
                    break;
                case "27":
                    data = CreateData27(dtStart, dtEnd);
                    break;

            }
            return data ?? new List<string>();
        }

        /// <summary>
        /// 1、患者基本信息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data1Entity> CreateData1(DateTime startDate, DateTime endDate)
        {
            //IPatVisitRepository patVisitRepository = new PatVisitRepository();
            var patient = _uow.GetRepository<PatientEntity>();
            //查询医疗机构名称
            var organize = _uow.GetRepository<OrganizeEntity>(); ///new OrganizeRepository();
            var expression = ExtLinq.True<OrganizeEntity>();
            expression = expression.And(t => t.F_ParentId=="0");
            var organizeEntity = organize.FindEntity(expression);
            //患者基本信息
            var expression1 = ExtLinq.True<PatientEntity>();
            expression1 = expression1.And(t => t.F_CreatorTime >= startDate && t.F_CreatorTime <= endDate)
                .And(t => t.F_Trasfer=="本院血液透析")
                //.Or(t => t.F_LastModifyTime >= startDate && t.F_LastModifyTime <= endDate)
                ;
            List<Data1Entity> list = new List<Data1Entity>();
            foreach (var item in patient.IQueryable(expression1))
            {
                Data1Entity entity = new Data1Entity
                {
                    Field1 = organizeEntity.F_FullName,
                    Field2 = item.F_Name,
                    Field3 = item.F_PY,
                    Field4 = item.F_Gender,
                    Field5 = item.F_IdNo,
                    Field6 = "身份证",
                    Field7 = item.F_IdNo,
                    Field8 = item.F_BirthDay.ToDateString(),
                    Field9 = item.F_Address,
                    Field10 = item.F_PhoneNo,
                    Field11 = item.F_PhoneNo2,
                    Field12 = item.F_Contacts,
                    Field13 = "",
                    Field14 = "",
                    Field15 = item.F_DialysisStartTime.ToDateString(),
                    Field16 = item.F_DialysisNo.ToString(),
                    Field17 = item.F_RecordNo,
                    Field18 = item.F_PatientNo,
                    Field20 = DateTime.Now.ToDateString(),
                    //entity.Field21 = "";
                    //entity.Field22 = "";
                    Field23 = item.F_InsuranceType
                };
                switch (item.F_Trasfer)
                {
                    case "本院血液透析":
                        entity.Field19 = "在透患者";
                        //添加转归情况 转入
                        list8.Add(new Data8Entity
                        {
                            Field1 = item.F_Name,
                            Field2 = item.F_IdNo,
                            Field3 = item.F_CreatorTime.ToDateString(),
                            Field4 = "转入"
                        });
                        break;
                    case "转腹膜透析":
                        entity.Field19 = "转出和退出患者";
                        break;
                    case "转外院透析":
                        entity.Field19 = "转出和退出患者";
                        break;
                    case "脱离透析":
                        entity.Field19 = "转出和退出患者";
                        break;
                    case "肾移植":
                        entity.Field19 = "肾移植患者";
                        break;
                    case "死亡":
                        entity.Field19 = "死亡患者";
                        break;
                    default:
                        entity.Field19 = "在透患者";
                        break;
                }
                list.Add(entity);
            }
            return list;
        }
        /// <summary>
        /// 2、原发病诊断
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data2Entity> CreateData2(DateTime startDate, DateTime endDate)
        {
            return new List<Data2Entity>();
        }
        /// <summary>
        /// 3、病理诊断
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data3Entity> CreateData3(DateTime startDate, DateTime endDate)
        {
            return new List<Data3Entity>();
        }
        /// <summary>
        /// 4、并发症诊断
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data4Entity> CreateData4(DateTime startDate, DateTime endDate)
        {
            return new List<Data4Entity>();
        }
        /// <summary>
        /// 5、传染病诊断
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data5Entity> CreateData5(DateTime startDate, DateTime endDate)
        {
            return new List<Data5Entity>();
        }
        /// <summary>
        /// 6、肿瘤诊断
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data6Entity> CreateData6(DateTime startDate, DateTime endDate)
        {
            return new List<Data6Entity>();
        }
        /// <summary>
        /// 7、过敏诊断
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data7Entity> CreateData7(DateTime startDate, DateTime endDate)
        {
            return new List<Data7Entity>();
        }
        /// <summary>
        /// 8、转归情况
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data8Entity> CreateData8(DateTime startDate, DateTime endDate)
        {
            //患者转归信息
            var expression = ExtLinq.True<PatientEntity>();
            //暂时只处理“转入”
            expression = expression.And(t => t.F_CreatorTime >= startDate && t.F_CreatorTime <= endDate)
                .And(t => t.F_Trasfer=="本院血液透析")
                //.Or(t => t.F_LastModifyTime >= startDate && t.F_LastModifyTime <= endDate)
                ;
            List<Data8Entity> list = new List<Data8Entity>();
            foreach (var item in _uow.GetRepository<PatientEntity>().IQueryable(expression).Select(
                t => new
                {
                    t.F_Name,
                    t.F_IdNo,
                    t.F_Trasfer,
                    t.F_CreatorTime,
                    t.F_LastModifyTime
                }).Where(t => t.F_IdNo != null))
            {
                if (item.F_IdNo.IsEmpty()) continue;
                list.Add(new Data8Entity
                {
                    Field1 = item.F_Name,
                    Field2 = item.F_IdNo,
                    Field3 = item.F_CreatorTime.ToDateString(),
                    Field4 = "转入"
                });
            }
            return list;
        }
        /// <summary>
        /// 9、血管通路
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data9Entity> CreateData9(DateTime startDate, DateTime endDate)
        {
            return new List<Data9Entity>();
        }
        /// <summary>
        /// 10、透析处方
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data10Entity> CreateData10(DateTime startDate, DateTime endDate)
        {
            return new List<Data10Entity>();
        }
        /// <summary>
        /// 11、血压测量
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data11Entity> CreateData11(DateTime startDate, DateTime endDate)
        {
            var list = new List<Data11Entity>();
            var patVisitList = _uow.GetRepository<PatVisitEntity>().IQueryable()
               .Where(t => t.F_VisitDate >= startDate.Date && t.F_VisitDate <= endDate.Date
               && t.F_EnabledMark == true && t.F_DialysisEndTime != null
               && t.F_SystolicPressure != null && t.F_DiastolicPressure != null
               )
               .Select(t => new {
                   t.F_Id,
                   t.F_Pid,
                   t.F_VisitDate,
                   t.F_SystolicPressure,
                   t.F_DiastolicPressure
               });
            foreach (var item in patVisitList.GroupBy(t => t.F_Pid))
            {
                item.OrderBy(t => t.F_VisitDate);
                var firstRow = item.First();
                var patientEntity = _uow.GetRepository<PatientEntity>().FindEntity(firstRow.F_Pid);
                if (patientEntity == null) continue;
                var observationRecords = _uow.GetRepository<DialysisObservationEntity>().IQueryable()
                    .Where(t => t.F_Pid == patientEntity.F_Id && t.F_VisitDate >= startDate.Date
                    && t.F_VisitDate <= endDate.Date && t.F_EnabledMark == true && t.F_DeleteMark == null
                    && t.F_SSY != null && t.F_SZY != null)
                    .Select(t => new
                    {
                        t.F_VisitDate,
                        t.F_NurseOperatorTime,
                        t.F_SSY,
                        t.F_SZY
                    })
                    .ToList();
                foreach (var child in item)
                {
                    Data11Entity data11 = new Data11Entity
                    {
                        Field1 = patientEntity.F_Name,
                        Field2 = patientEntity.F_IdNo,
                        Field3 = child.F_VisitDate.ToDateString(),
                        Field4 = "上肢",
                        Field5 = child.F_SystolicPressure.ToFloat(0).ToString(),
                        Field6 = child.F_DiastolicPressure.ToFloat(0).ToString()
                    };
                    DateTime dateTime = child.F_VisitDate.ToDate();
                    var findOB = observationRecords.FindAll(t => t.F_VisitDate == dateTime).OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault();
                    if (findOB != null)
                    {
                        data11.Field7 = findOB.F_SSY.ToFloat(0).ToString();
                        data11.Field8 = findOB.F_SZY.ToFloat(0).ToString();
                    }
                    list.Add(data11);
                }
            }
            return list;
        }
        /// <summary>
        /// 12、透析充分性
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data12Entity> CreateData12(DateTime startDate, DateTime endDate)
        {
            var list = new List<Data12Entity>();
            //查询检验结果，包含 UREA 尿素
            var date1 = startDate.Date;
            var date2 = endDate.Date.AddDays(1);
            var lisResultsTemp = _uow.GetRepository<QualityResultEntity>().IQueryable().Where(t => t.F_ReportTime >= date1 && t.F_ReportTime <= date2
                                                                                                                                                   && t.F_EnabledMark == true && t.F_DeleteMark == null && t.F_ItemCode=="UREA")
                                                            .Select(t => new
                                                            {
                                                                t.F_Pid,
                                                                t.F_ReportTime,
                                                                t.F_Result
                                                            })
                                                            .ToList();
            var lisResults = lisResultsTemp.Select(t => new
            {
                t.F_Pid,
                t.F_ReportTime,
                t.F_Result,
                F_VisitDate = t.F_ReportTime.ToDate().Date
            }).ToList();
            //查找同一天包含两次结果的患者ID
            Dictionary<string, List<DateTime>> pidDict = new Dictionary<string, List<DateTime>>();
            List<string> pids = new List<string>();
            foreach (var item in lisResults.GroupBy(t => t.F_Pid))
            {
                foreach (var child in item.GroupBy(t => t.F_VisitDate))
                {
                    if (child.Count() >= 2)
                    {
                        if (pidDict.ContainsKey(item.Key))
                        {
                            pidDict[item.Key].Add(child.Key);
                        }
                        else
                        {
                            pids.Add(item.Key);
                            pidDict.Add(item.Key, new List<DateTime> {
                                child.Key
                            });
                        }
                    }
                }
            }
            //透析记录
            var patVisitListTemp = _uow.GetRepository<PatVisitEntity>().IQueryable()
                .Where(t => t.F_VisitDate >= startDate.Date && t.F_VisitDate <= endDate.Date
                && pids.Contains(t.F_Pid)
                && t.F_DialysisEndTime != null && t.F_DialysisStartTime != null
                && t.F_WeightTQ != null
                && t.F_WeightTH != null
                && t.F_EnabledMark == true)
                .Select(t => new
                {
                    t.F_Pid,
                    t.F_VisitDate,
                    t.F_WeightTQ,
                    t.F_WeightTH,
                    t.F_DialysisStartTime,
                    t.F_DialysisEndTime
                }).ToList();
            var patVisitList = patVisitListTemp.Select(t => new
            {
                t.F_Pid,
                F_VisitDate = t.F_VisitDate.ToDate(),
                F_WeightTQ = t.F_WeightTQ.ToFloat(2),
                F_WeightTH = t.F_WeightTH.ToFloat(2),
                F_DialysisStartTime = t.F_DialysisStartTime.ToDate(),
                F_DialysisEndTime = t.F_DialysisEndTime.ToDate()
            }).ToList();
            //患者基本信息
            var patientList = _uow.GetRepository<PatientEntity>().IQueryable()
                .Where(t => pids.Contains(t.F_Id))
                .Select(t => new
                {
                    t.F_Id,
                    t.F_IdNo,
                    t.F_Name,
                    t.F_IdealWeight,
                    t.F_Height
                }).ToList();
            //循环添加数据
            foreach (var dict in pidDict)
            {
                var filterVisitRecords = patVisitList.FindAll(t => t.F_Pid==dict.Key).FindAll(t => dict.Value.Contains(t.F_VisitDate));
                if (filterVisitRecords.Count() == 0) continue;
                var patient = patientList.FirstOrDefault(t => t.F_Id==dict.Key);
                if (patient == null) continue;
                foreach (var item in filterVisitRecords.OrderBy(t => t.F_VisitDate))
                {
                    var results = lisResults.FindAll(t => t.F_Pid==dict.Key && t.F_VisitDate==item.F_VisitDate).OrderBy(t => t.F_ReportTime);
                    if (results.Count() < 2) continue;
                    //hour
                    var duration = (item.F_DialysisEndTime - item.F_DialysisStartTime).TotalHours.ToDouble(2);
                    if (duration < 1 || duration > 10) continue;
                    //L
                    var ultrafiltrationVolume = (item.F_WeightTQ - item.F_WeightTH).ToFloat(2);
                    if (ultrafiltrationVolume < 0 || ultrafiltrationVolume > 8) continue;
                    //Kg
                    float postWeight = item.F_WeightTH.ToFloat(2);
                    if (postWeight < 20) continue;
                    if (!float.TryParse(results.First().F_Result, out float preUrea) || !float.TryParse(results.Last().F_Result, out float postUrea)) continue;
                    Data12Entity entity = new Data12Entity
                    {
                        Field1 = patient.F_Name,
                        Field2 = patient.F_IdNo,
                        Field3 = item.F_VisitDate.ToDateString(),
                        //Field4 = patient.F_Height == null ? "" : (patient.F_Height.ToFloat(0) / 100).ToString(),
                        //Field5 = patient.F_IdealWeight.ToFloat(2).ToString(),
                        //Field6 = "",
                        //Field7 = "",
                        Field8 = preUrea.ToFloat(2).ToString(),
                        Field9 = postUrea.ToFloat(2).ToString(),
                        Field10 = duration.ToString(),
                        Field11 = ultrafiltrationVolume.ToString(),
                        Field12 = (100 * (1 - postUrea / preUrea)).ToFloat(2).ToString(),
                        Field13 = (-Math.Log(postUrea / preUrea - 0.008 * duration) + (4 - 3.5 * postUrea / preUrea) * ultrafiltrationVolume / postWeight).ToFloat(2).ToString()
                    };
                    float height = 0;
                    if (patient.F_Height != null)
                    {
                        height = patient.F_Height.ToFloat(2) / 100;
                        entity.Field4 = height.ToString();
                    }
                    float weight = 0;
                    if (patient.F_IdealWeight != null)
                    {
                        weight = patient.F_IdealWeight.ToFloat(2);
                        entity.Field5 = weight.ToString();
                    }
                    if (height > 0 && weight > 0)
                    {
                        entity.Field6 = (weight / (height * height)).ToFloat(2).ToString();
                        //体表面积（m2）= 0.0061×身高（cm）+0.0128×体重(kg) - 0.1529
                        entity.Field7 = (0.0061 * height * 100 + 0.0128 * weight - 0.1529).ToFloat(2).ToString();
                    }
                    list.Add(entity);
                }
            }
            return list;
        }
        /// <summary>
        /// 13、抗凝剂
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data13Entity> CreateData13(DateTime startDate, DateTime endDate)
        {
            return new List<Data13Entity>();
        }
        /// <summary>
        /// 14、干体重
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data14Entity> CreateData14(DateTime startDate, DateTime endDate)
        {
            var list = new List<Data14Entity>();
            //查询透析记录 时间段查询
            var expression = ExtLinq.True<PatVisitEntity>();
            expression = expression.And(t => t.F_VisitDate >= startDate)
                .And(t => t.F_VisitDate <= endDate)
                .And(t => t.F_DialysisStartTime != null)
                .And(t => t.F_DialysisEndTime != null)
                .And(t => t.F_WeightTQ != null)
                .And(t => t.F_EnabledMark == true);
            var patVisitList =_uow.GetRepository<PatVisitEntity>().IQueryable(expression).Select(t => new { t.F_Id, t.F_Pid, t.F_VisitDate, t.F_WeightTQ, t.F_DialysisStartTime }).ToList();

            foreach (var item in patVisitList.GroupBy(t => t.F_Pid))
            {
                item.OrderBy(t => t.F_VisitDate);
                var first = item.First();
                var patient = _uow.GetRepository<PatientEntity>().IQueryable().FirstOrDefault(t => t.F_Id==first.F_Pid);
                if (patient == null || patient.F_IdNo == null) continue;
                var weightRecords = _uow.GetRepository<IdeaWeightEntity>().IQueryable().Where(t => t.F_Pid==first.F_Pid)
                    .Select(t => new { t.F_CreatorTime, t.F_IdealWeight }).OrderBy(t => t.F_CreatorTime).ToList();
                foreach (var child in item)
                {
                    //根据透析开始时间 查找最近的干体重信息
                    var dialysisStartTime = child.F_DialysisStartTime.ToDate();
                    var find = weightRecords.LastOrDefault(t => t.F_CreatorTime <= dialysisStartTime);
                    var ideaWeight = find == null ? patient.F_IdealWeight : find.F_IdealWeight;
                    if (ideaWeight == null) continue;
                    list.Add(new Data14Entity
                    {
                        Field1 = patient.F_Name,
                        Field2 = patient.F_IdNo,
                        Field3 = child.F_VisitDate.ToDateString(),
                        Field4 = "非卧床",
                        Field5 = ideaWeight.ToFloat(2).ToString(),
                        Field6 = child.F_WeightTQ.ToFloat(2).ToString()
                    });
                }
            }

            return list;
        }
        /// <summary>
        /// 15、合用其他透析模式
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data15Entity> CreateData15(DateTime startDate, DateTime endDate)
        {
            return new List<Data15Entity>();
        }
        /// <summary>
        /// 16、实验室检查
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data16Entity> CreateData16(DateTime startDate, DateTime endDate)
        {
            var list = new List<Data16Entity>();
            var expression = ExtLinq.True<QualityResultEntity>();
            expression = expression.And(t => t.F_ReportTime >= startDate)
                .And(t => t.F_ReportTime <= endDate)
                .And(t => t.F_EnabledMark == true)
                .And(t => t.F_DeleteMark == null);
            var results = _uow.GetRepository<QualityResultEntity>().IQueryable(expression).Select(t => new {
                t.F_Pid,
                t.F_ReportTime,
                //Month =t.F_ReportTime.ToDateString().Substring(0, 7),
                t.F_ItemCode,
                t.F_ItemName,
                t.F_Result,
                t.F_Flag,
                t.F_Unit,
                t.F_LowerValue,
                t.F_UpperValue
            }).ToList();
            var patVisitList = _uow.GetRepository<PatVisitEntity>().IQueryable()
                .Where(t => t.F_VisitDate >= startDate.Date && t.F_VisitDate <= endDate.Date && t.F_EnabledMark == true)
                .Select(t => t.F_Pid).Distinct().ToList();
            var patients = _uow.GetRepository<PatientEntity>().IQueryable().Where(t => patVisitList.Contains(t.F_Id)).Select(t => new
            {
                F_Pid = t.F_Id,
                t.F_Name,
                t.F_IdNo
            });
            var qualityResultList = from r in results
                                    join p in patients on r.F_Pid equals p.F_Pid
                                    select new
                                    {
                                        p.F_Name,
                                        p.F_IdNo,
                                        r.F_ReportTime,
                                        Month = r.F_ReportTime.ToDateString().Substring(0, 7),
                                        Day = r.F_ReportTime.ToDateString(),
                                        p.F_Pid,
                                        r.F_ItemCode,
                                        r.F_ItemName,
                                        r.F_Result,
                                        r.F_Flag,
                                        r.F_Unit,
                                        r.F_LowerValue,
                                        r.F_UpperValue
                                    };
            //按患者分组
            foreach (var item in qualityResultList.GroupBy(t => t.F_Pid))
            {
                //按照月份分组
                foreach (var child in item.GroupBy(t => t.Month))
                {
                    var firstRow = child.First();
                    //实验室检查
                    Data16Entity data16 = new Data16Entity
                    {
                        Field1 = firstRow.F_Name,
                        Field2 = firstRow.F_IdNo
                    };

                    bool addFlag = false;

                    //1、血常规检查日期
                    var filterRows = child.Where(t => t.F_ItemCode=="HGB" || t.F_ItemCode=="WBC" || t.F_ItemCode=="HCT" || t.F_ItemCode=="PLT");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field3 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode=="WBC");
                        if (findrow != null) data16.Field4 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode=="HGB");
                        if (findrow != null) data16.Field5 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode=="HCT");
                        if (findrow != null) data16.Field6 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode=="PLT");
                        if (findrow != null) data16.Field7 = findrow.F_Result;
                    }

                    //2、骨矿物质代谢 TCA、P、iPTH
                    filterRows = child.Where(t => t.F_ItemCode=="TCA" || t.F_ItemCode=="P" || t.F_ItemCode=="iPTH");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field8 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode=="TCA");
                        if (findrow != null) data16.Field9 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode=="P");
                        if (findrow != null) data16.Field10 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode=="iPTH");
                        if (findrow != null) data16.Field11 = findrow.F_Result;
                    }

                    //2、铁代谢 Iron、TIBC、TS、FER
                    filterRows = child.Where(t => t.F_ItemCode=="Iron" || t.F_ItemCode=="TIBC" || t.F_ItemCode=="TS" || t.F_ItemCode=="FER");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field12 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "Iron");
                        if (findrow != null) data16.Field13 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "TIBC");
                        if (findrow != null) data16.Field14 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "TS");
                        if (findrow != null) data16.Field15 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "FER");
                        if (findrow != null) data16.Field16 = findrow.F_Result;
                    }

                    //2、生化检查 UREA、CREA、TP、ALB-A、AST、ALT、TBil、TG、TCHO、LDL、HDL、GLU、K、Na、Cl、CO2
                    filterRows = child.Where(t => t.F_ItemCode == "UREA" || t.F_ItemCode == "CREA"
                    || t.F_ItemCode == "TP" || t.F_ItemCode == "ALB-A"
                    || t.F_ItemCode == "AST" || t.F_ItemCode == "ALT"
                    || t.F_ItemCode == "TBil" || t.F_ItemCode == "TG"
                    || t.F_ItemCode == "TCHO" || t.F_ItemCode == "LDL"
                    || t.F_ItemCode == "HDL" || t.F_ItemCode == "GLU"
                    || t.F_ItemCode == "K" || t.F_ItemCode == "Na"
                    || t.F_ItemCode == "Cl" || t.F_ItemCode == "CO2"
                    );
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field17 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "UREA");
                        if (findrow != null)
                        {
                            if (findrow.F_Unit == "mg/dl") data16.Field18 = findrow.F_Result;
                            if (findrow.F_Unit == "mmol/L") data16.Field19 = findrow.F_Result;
                        }
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "CREA");
                        if (findrow != null)
                        {
                            if (findrow.F_Unit == "mg/dl") data16.Field20 = findrow.F_Result;
                            if (findrow.F_Unit == "μmol/L") data16.Field21 = findrow.F_Result;
                        }
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "TP");
                        if (findrow != null) data16.Field22 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "ALB-A");
                        if (findrow != null) data16.Field23 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "AST");
                        if (findrow != null) data16.Field24 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "ALT");
                        if (findrow != null) data16.Field25 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "TBil");
                        if (findrow != null) data16.Field26 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "TG");
                        if (findrow != null) data16.Field27 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "TCHO");
                        if (findrow != null) data16.Field28 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "LDL");
                        if (findrow != null) data16.Field29 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "HDL");
                        if (findrow != null) data16.Field30 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "GLU");
                        if (findrow != null) data16.Field31 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "K");
                        if (findrow != null) data16.Field32 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "Na");
                        if (findrow != null) data16.Field33 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "Cl");
                        if (findrow != null) data16.Field34 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "CO2");
                        if (findrow != null) data16.Field35 = findrow.F_Result;
                    }

                    //1、C反应蛋白检查日期 CRP、β2-MG
                    filterRows = child.Where(t => t.F_ItemCode == "CRP" || t.F_ItemCode == "β2-MG");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field36 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "CRP");
                        if (findrow != null) data16.Field37 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "β2-MG");
                        if (findrow != null) data16.Field38 = findrow.F_Result;
                    }

                    //1、乙肝二对半检查日期 HBsAg、Anti-HBs、HBeAg、Anti-HBe、Anti-HBc
                    filterRows = child.Where(t => t.F_ItemCode == "HBsAg" || t.F_ItemCode == "Anti-HBs"
                    || t.F_ItemCode == "HBeAg" || t.F_ItemCode == "Anti-HBe" || t.F_ItemCode == "Anti-HBc");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field39 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "HBsAg");
                        if (findrow != null) data16.Field40 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "Anti-HBs");
                        if (findrow != null) data16.Field41 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "HBeAg");
                        if (findrow != null) data16.Field42 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "Anti-HBe");
                        if (findrow != null) data16.Field43 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "Anti-HBc");
                        if (findrow != null) data16.Field44 = findrow.F_Result;
                    }

                    //1、HBV检查定量日期 HBVDNA
                    filterRows = child.Where(t => t.F_ItemCode == "HBVDNA");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field45 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "HBVDNA");
                        if (findrow != null) data16.Field46 = findrow.F_Result;
                    }

                    //1、HCV检查日期 Anti-HCV
                    filterRows = child.Where(t => t.F_ItemCode == "Anti-HCV");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field47 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "Anti-HCV");
                        if (findrow != null) data16.Field48 = findrow.F_Result;
                    }

                    //1、HCV-RNA检查日期 HCVRNA
                    filterRows = child.Where(t => t.F_ItemCode == "HCVRNA");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field49 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "HCVRNA");
                        if (findrow != null) data16.Field50 = findrow.F_Result;
                    }

                    //1、艾滋病检查日期 HIV
                    filterRows = child.Where(t => t.F_ItemCode == "HIV");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field51 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "HIV");
                        if (findrow != null) data16.Field52 = findrow.F_Result;
                    }
                    //1、梅毒检查日期 梅毒
                    filterRows = child.Where(t => t.F_ItemCode == "梅毒");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        data16.Field53 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "梅毒");
                        if (findrow != null) data16.Field54 = findrow.F_Result;
                    }

                    //1、结核 结核抗体、结核菌素试验
                    filterRows = child.Where(t => t.F_ItemCode == "结核抗体" || t.F_ItemCode == "结核菌素试验");
                    if (filterRows.Count() > 0)
                    {
                        addFlag = true;
                        filterRows = filterRows.OrderByDescending(t => t.F_ReportTime);
                        //data16.Field36 = filterRows.First().Day;
                        var findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "结核抗体");
                        if (findrow != null) data16.Field55 = findrow.F_Result;
                        findrow = filterRows.FirstOrDefault(t => t.F_ItemCode == "结核菌素试验");
                        if (findrow != null) data16.Field56 = findrow.F_Result;
                    }
                    //存在检验结果，添加
                    if (addFlag) list.Add(data16);
                }
            }

            return list;
        }
        /// <summary>
        /// 17、辅助检查
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data17Entity> CreateData17(DateTime startDate, DateTime endDate)
        {
            return new List<Data17Entity>();
        }
        /// <summary>
        /// 18、患者周透析频率
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data18Entity> CreateData18(DateTime startDate, DateTime endDate)
        {
            return new List<Data18Entity>();
        }
        /// <summary>
        /// 19、透析时间例次
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data19Entity> CreateData19(DateTime startDate, DateTime endDate)
        {
            return new List<Data19Entity>();
        }
        /// <summary>
        /// 20、透析时间例数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data20Entity> CreateData20(DateTime startDate, DateTime endDate)
        {
            return new List<Data20Entity>();
        }
        /// <summary>
        /// 21、促红素
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data21Entity> CreateData21(DateTime startDate, DateTime endDate)
        {
            return new List<Data21Entity>();
        }
        /// <summary>
        /// 22、铁剂
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data22Entity> CreateData22(DateTime startDate, DateTime endDate)
        {
            return new List<Data22Entity>();
        }
        /// <summary>
        /// 23、抗高血压药
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data23Entity> CreateData23(DateTime startDate, DateTime endDate)
        {
            return new List<Data23Entity>();
        }
        /// <summary>
        /// 24、活性维生素D
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data24Entity> CreateData24(DateTime startDate, DateTime endDate)
        {
            return new List<Data24Entity>();
        }
        /// <summary>
        /// 25、钙剂
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data25Entity> CreateData25(DateTime startDate, DateTime endDate)
        {
            return new List<Data25Entity>();
        }
        /// <summary>
        /// 26、降磷药物
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data26Entity> CreateData26(DateTime startDate, DateTime endDate)
        {
            return new List<Data26Entity>();
        }
        /// <summary>
        /// 27、其它药物治疗
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Data27Entity> CreateData27(DateTime startDate, DateTime endDate)
        {
            return new List<Data27Entity>();
        }

        public void SubmitData(string keyValue)
        {
            var json = keyValue.ToJObject();
            string index = json.Value<string>("index");
            var data = json.Value<JArray>("data");

            switch (index)
            {
                case "1":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data1Entity>();
                        list1.Add(child);
                    }
                    FillDataToExcel(list1, "患者基本信息");
                    break;
                case "2":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data2Entity>();
                        list2.Add(child);
                    }
                    FillDataToExcel(list2, "原发病诊断");
                    break;
                case "3":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data3Entity>();
                        list3.Add(child);
                    }
                    FillDataToExcel(list3, "病理诊断");
                    break;
                case "4":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data4Entity>();
                        list4.Add(child);
                    }
                    FillDataToExcel(list4, "并发症诊断");
                    break;
                case "5":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data5Entity>();
                        list5.Add(child);
                    }
                    FillDataToExcel(list5, "传染病诊断");
                    break;
                case "6":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data6Entity>();
                        list6.Add(child);
                    }
                    FillDataToExcel(list6, "肿瘤诊断");
                    break;
                case "7":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data7Entity>();
                        list7.Add(child);
                    }
                    FillDataToExcel(list7, "过敏诊断");
                    break;
                case "8":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data8Entity>();
                        list8.Add(child);
                    }
                    FillDataToExcel(list8, "转归情况");
                    break;
                case "9":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data9Entity>();
                        list9.Add(child);
                    }
                    FillDataToExcel(list9, "血管通路");
                    break;
                case "10":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data10Entity>();
                        list10.Add(child);
                    }
                    FillDataToExcel(list10, "透析处方");
                    break;
                case "11":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data11Entity>();
                        list11.Add(child);
                    }
                    FillDataToExcel(list11, "血压测量");
                    break;
                case "12":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data12Entity>();
                        list12.Add(child);
                    }
                    FillDataToExcel(list12, "透析充分性");
                    break;
                case "13":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data13Entity>();
                        list13.Add(child);
                    }
                    FillDataToExcel(list13, "抗凝剂");
                    break;
                case "14":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data14Entity>();
                        list14.Add(child);
                    }
                    FillDataToExcel(list14, "干体重");
                    break;
                case "15":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data15Entity>();
                        list15.Add(child);
                    }
                    FillDataToExcel(list15, "合用其它透析模式");
                    break;
                case "16":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data16Entity>();
                        list16.Add(child);
                    }
                    FillDataToExcel(list16, "实验室检查");
                    break;
                case "17":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data17Entity>();
                        list17.Add(child);
                    }
                    FillDataToExcel(list17, "辅助检查");
                    break;
                case "18":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data18Entity>();
                        list18.Add(child);
                    }
                    FillDataToExcel(list18, "患者周透析频率");
                    break;
                case "19":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data19Entity>();
                        list19.Add(child);
                    }
                    FillDataToExcel(list19, "透析时间例次");
                    break;
                case "20":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data20Entity>();
                        list20.Add(child);
                    }
                    FillDataToExcel(list20, "透析时间例数");
                    break;
                case "21":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data21Entity>();
                        list21.Add(child);
                    }
                    FillDataToExcel(list21, "促红素");
                    break;
                case "22":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data22Entity>();
                        list22.Add(child);
                    }
                    FillDataToExcel(list22, "铁剂");
                    break;
                case "23":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data23Entity>();
                        list23.Add(child);
                    }
                    FillDataToExcel(list23, "抗高血压药");
                    break;
                case "24":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data24Entity>();
                        list24.Add(child);
                    }
                    FillDataToExcel(list24, "活性维生素D");
                    break;
                case "25":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data25Entity>();
                        list25.Add(child);
                    }
                    FillDataToExcel(list25, "钙剂");
                    break;
                case "26":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data26Entity>();
                        list26.Add(child);
                    }
                    FillDataToExcel(list26, "降磷药物");
                    break;
                case "27":
                    foreach (var item in data)
                    {
                        var child = item.ToObject<Data27Entity>();
                        list27.Add(child);
                    }
                    FillDataToExcel(list27, "其它药物治疗");
                    break;
            }
            //throw new NotImplementedException();
        }
    }
}
