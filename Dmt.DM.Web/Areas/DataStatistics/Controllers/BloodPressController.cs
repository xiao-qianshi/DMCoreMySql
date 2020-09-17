using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.Areas.DataStatistics.Controllers
{
    [Area("DataStatistics")]
    public class BloodPressController : BaseController
    {
        private readonly IPatientApp _patientApp;
        private readonly IPatVisitApp _patVisitApp;

        public BloodPressController(IPatientApp patientApp, IPatVisitApp patVisitApp)
        {
            _patientApp = patientApp;
            _patVisitApp = patVisitApp;
        }

        public IActionResult GetBloodPressJson(string keyValue)
        {
            var json = keyValue.ToJObject();
            var startDate = json.Value<DateTime>("startDate");
            var endDate = json.Value<DateTime>("endDate");
            var pid = json.Value<string>("pid");

            //查询治疗记录
            var data = (from r in _patVisitApp.GetList().Where(t=>t.F_Pid == pid && t.F_DialysisEndTime != null && t.F_VisitDate >= startDate && t.F_VisitDate <= endDate && t.F_EnabledMark != false) //, startDate, endDate
                        orderby r.F_VisitDate
                        select new
                        {
                            date = r.F_VisitDate.ToDateString().Substring(5),
                            ssy = r.F_SystolicPressure.ToFloat(2),
                            szy = r.F_DiastolicPressure.ToFloat(2)
                        }).ToList();
            return Content(data.ToJson());
        }


        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult Summary()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult SummaryAll()
        {
            return View();
        }

        /// <summary>
        /// 周中血压统计
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSummaryJson(string keyValue)
        {
            BloodPressOutput output = new BloodPressOutput();

            var json = keyValue.ToJObject();
            var startDate = json.Value<DateTime>("startDate");
            var endDate = json.Value<DateTime>("endDate");
            var pid = json.Value<string>("pid");

            var list = from r in _patVisitApp.GetList().Where(t => t.F_Pid == pid && t.F_DialysisEndTime != null && t.F_VisitDate >= startDate && t.F_VisitDate <= endDate && t.F_EnabledMark != false)
                       join p in _patientApp.GetQueryable() on r.F_Pid equals p.F_Id
                       select new
                       {
                           id = r.F_Id,
                           visitDate = r.F_VisitDate.ToDate(),
                           pid = p.F_Id,
                           name = p.F_Name,
                           dayOfWeek = r.F_VisitDate.ToDate().DayOfWeek == DayOfWeek.Sunday ? 7
                                       : r.F_VisitDate.ToDate().DayOfWeek == DayOfWeek.Monday ? 1
                                       : r.F_VisitDate.ToDate().DayOfWeek == DayOfWeek.Tuesday ? 2
                                       : r.F_VisitDate.ToDate().DayOfWeek == DayOfWeek.Wednesday ? 3
                                       : r.F_VisitDate.ToDate().DayOfWeek == DayOfWeek.Thursday ? 4
                                       : r.F_VisitDate.ToDate().DayOfWeek == DayOfWeek.Friday ? 5
                                       : r.F_VisitDate.ToDate().DayOfWeek == DayOfWeek.Saturday ? 6
                                       : 0,
                           ssy = r.F_SystolicPressure.ToFloat(2),
                           szy = r.F_DiastolicPressure.ToFloat(2)
                       };

            var count = list.Count();
            //无数据返回
            if (count == 0) return Content(output.ToJson());
            var firstDay = list.OrderBy(t => t.visitDate).First().visitDate.Date;
            var firstMonday = firstDay.AddDays(
                firstDay.DayOfWeek == DayOfWeek.Monday ? 0
                : firstDay.DayOfWeek == DayOfWeek.Tuesday ? -1
                : firstDay.DayOfWeek == DayOfWeek.Wednesday ? -2
                : firstDay.DayOfWeek == DayOfWeek.Thursday ? -3
                : firstDay.DayOfWeek == DayOfWeek.Friday ? -4
                : firstDay.DayOfWeek == DayOfWeek.Saturday ? -5
                : firstDay.DayOfWeek == DayOfWeek.Sunday ? -6
                : 0
                );
            var computerList = from r in list
                               select new
                               {
                                   weekIndex = ((int)((r.visitDate - firstMonday).TotalDays)) / 7 + 1,
                                   sort = Math.Abs(r.dayOfWeek.ToFloat(1) - 3.5),
                                   r.id,
                                   r.pid,
                                   r.name,
                                   r.ssy,
                                   r.szy,
                                   desc = r.ssy + " / " + r.szy
                               };


            foreach (var item in computerList.GroupBy(t => t.weekIndex))
            {
                output.WeekList.Add(item.Key);

                foreach (var child in item.GroupBy(t => t.pid))
                {
                    if (output.Patients.IndexOf(child.Key) < 0)
                    {
                        output.Patients.Add(child.Key);
                    }
                    var choosedItem = child.OrderBy(t => t.sort).FirstOrDefault();
                    if (choosedItem != null)
                    {
                        var temp = new BloodPressDatas
                        {
                            WeekIndex = item.Key,
                            Id = choosedItem.id,
                            Pid = choosedItem.pid,
                            Name = choosedItem.name
                        };
                        if (choosedItem.ssy == 0f)
                        {
                            continue;
                        }
                        else
                        {
                            temp.Ssy = choosedItem.ssy.ToString();
                            temp.Szy = choosedItem.szy.ToString();
                            temp.Desc = choosedItem.desc;
                            if (choosedItem.ssy >= 160)
                            {
                                temp.Range = ">160";
                            }
                            else if (choosedItem.ssy >= 140)
                            {
                                temp.Range = "140-160";
                            }
                            else
                            {
                                temp.Range = "<140";
                            }
                        }
                        output.WeekDatas.Add(temp);
                    }
                }
            }
            output.WeekList.Sort();
            return Content(output.ToJson());
        }

        /// <summary>
        /// 血压汇总统计
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSummaryAllJson(string keyValue)
        {
            var json = keyValue.ToJObject();
            var startDate = json.Value<DateTime>("startDate");
            var endDate = json.Value<DateTime>("endDate");
            var itemCode = json.Value<string>("itemCode") ?? "";
            var list = _patVisitApp.GetList().Where(t =>
                    (itemCode == "" || t.F_Pid == itemCode) && t.F_DialysisEndTime != null && t.F_VisitDate >= startDate &&
                    t.F_VisitDate <= endDate && t.F_EnabledMark != false)
                .Select(t => new
                {
                    t.F_Id,
                    t.F_Pid,
                    t.F_SystolicPressure,
                    t.F_VisitDate,
                    t.F_DiastolicPressure
                }).ToList();

            var output = new PressStatisticsOutput();

            if (!list.Any()) return Error("无血压数据");
            //pie title data
            output.Title = startDate.ToChineseDateString() + " 至 " + endDate.ToChineseDateString() + "血压数据"; //+ "(" + list[0].F_ItemCode + ")"

            var detailList = from r in list
                             join p in _patientApp.GetQueryable() on r.F_Pid equals p.F_Id
                             where r.F_SystolicPressure != null
                             select new
                             {
                                 r.F_Id,
                                 p.F_Name,
                                 p.F_Gender,
                                 p.F_BirthDay,
                                 F_VisitDate = r.F_VisitDate.ToDate(),
                                 F_SystolicPressure = r.F_SystolicPressure.ToFloat(2),
                                 F_DiastolicPressure = r.F_DiastolicPressure.ToFloat(2)
                             };

            foreach (var item in detailList)
            {
                PressGridDataModel model = new PressGridDataModel
                {
                    F_Id = item.F_Id,
                    F_Name = item.F_Name,
                    F_Gender = item.F_Gender,
                    F_BirthDay = item.F_BirthDay,
                    F_VisitDate = item.F_VisitDate,
                    F_DiastolicPressure = item.F_DiastolicPressure,
                    F_SystolicPressure = item.F_SystolicPressure
                };
                if (model.F_BirthDay != null)
                {
                    model.F_AgeDesc = ((int)((DateTime.Now - (DateTime)model.F_BirthDay).TotalDays / 365)).ToString() + "岁";
                }

                if (model.F_SystolicPressure >= 160)
                {
                    model.FilterValue = ">160";
                }
                else if (model.F_SystolicPressure >= 140)
                {
                    model.FilterValue = "140-160";
                }
                else
                {
                    model.FilterValue = "<140";
                }
                output.GridData.Add(model);
            }
            output.RangeData.Add(new KeyValuePair<int, string>(1, "<140"));
            output.RangeData.Add(new KeyValuePair<int, string>(2, "140-160"));
            output.RangeData.Add(new KeyValuePair<int, string>(3, ">160"));

            output.NormalRange = "<140";

            var findCount = output.GridData.Count(t => t.F_SystolicPressure < 140);
            output.PieData.Add(new KeyValuePair<string, float>("<140", (findCount * 100 / list.Count).ToFloat(2)));
            if (findCount > 0)
            {
                output.UlData.Add(new KeyValuePair<string, int>("<140", findCount));
            }

            findCount = output.GridData.Count(t => t.F_SystolicPressure >= 140 && t.F_SystolicPressure < 160);
            output.PieData.Add(new KeyValuePair<string, float>("140-160", (findCount * 100 / list.Count).ToFloat(2)));
            if (findCount > 0)
            {
                output.UlData.Add(new KeyValuePair<string, int>("140-160", findCount));
            }

            findCount = output.GridData.Count(t => t.F_SystolicPressure >= 160);
            output.PieData.Add(new KeyValuePair<string, float>(">160", (findCount * 100 / list.Count).ToFloat(2)));
            if (findCount > 0)
            {
                output.UlData.Add(new KeyValuePair<string, int>(">160", findCount));
            }

            //趋势图  最近6个月
            //var rows = 
            var trendDateStart = DateTime.Parse(DateTime.Now.AddMonths(-6).ToString("yyyy-MM-01")); //6个月前 1号
            var trendDateEnd = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")); //本月前 1号
            var rows = from r in _patVisitApp.GetList().Where(t => t.F_DialysisEndTime != null && t.F_VisitDate >= trendDateStart &&
                    t.F_VisitDate <= trendDateEnd && t.F_EnabledMark != false)//patVisit.GetListByDateRange(null, trendDateStart, trendDateEnd)
                       select new
                       {
                           ReportTime = r.F_VisitDate,
                           Pid = r.F_Pid,
                           ReportMonth = r.F_VisitDate.ToDateString().Substring(0, 7),
                           Result = r.F_SystolicPressure.ToFloat(2)
                       };
            //rows.OrderBy(t => t.ReportTime);
            foreach (var item in rows.GroupBy(t => t.ReportMonth))
            {
                PressTrendDataModel dataModel = new PressTrendDataModel
                {
                    MonthDesc = item.Key
                };
                List<float> templist = new List<float>();
                foreach (var ele in item.GroupBy(t => t.Pid))
                {
                    templist.Add(ele.OrderByDescending(t => t.ReportTime).First().Result);
                }

                float count1 = 0f;
                float count2 = 0f;
                float count3 = 0f;
                //float count4 = 0f;
                //float count5 = 0f;
                foreach (var ele in templist)
                {
                    if (ele < 140) count1 = count1 + 1;
                    else if (ele < 160) count2 = count2 + 1;
                    else if (ele >= 160) count3 = count3 + 1;
                    //else if (ele >= 10 && ele < 12) count4 = count4 + 1;
                    //else if (ele > 12) count5 = count5 + 1;
                }
                dataModel.Value.Add(new KeyValuePair<string, float>("<140", (count1 * 100 / templist.Count).ToFloat(2)));
                dataModel.Value.Add(new KeyValuePair<string, float>("140-160", (count2 * 100 / templist.Count).ToFloat(2)));
                dataModel.Value.Add(new KeyValuePair<string, float>(">160", (count3 * 100 / templist.Count).ToFloat(2)));
                //dataModel.Value.Add(new KeyValuePair<string, float>("10-12", (count4 * 100 / templist.Count).ToFloat(2)));
                //dataModel.Value.Add(new KeyValuePair<string, float>(">12", (count5 * 100 / templist.Count).ToFloat(2)));

                output.TrendData.Add(dataModel);

            }
            output.TrendData = output.TrendData.OrderBy(t => t.MonthDesc).ToList();
            return Content(output.ToJson());
        }

    }

    public class BloodPressOutput
    {
        public List<int> WeekList { get; set; }
        public List<string> Ranges { get; set; }
        public List<string> Patients { get; set; }
        public List<BloodPressDatas> WeekDatas { get; set; }
        public BloodPressOutput()
        {
            WeekList = new List<int>();
            WeekDatas = new List<BloodPressDatas>();
            Ranges = new List<string>();
            Ranges.AddRange(new string[]
            {
                "<140","140-160",">160"
            });
            Patients = new List<string>();
        }
    }

    public class BloodPressDatas
    {
        public int WeekIndex { get; set; }
        public string Id { get; set; }
        public string Pid { get; set; }
        public string Name { get; set; }
        public string Ssy { get; set; }
        public string Szy { get; set; }
        public string Desc { get; set; }
        public string Range { get; set; }
    }


    public class PressStatisticsOutput
    {
        public string Title { get; set; }
        public List<KeyValuePair<string, float>> PieData { get; set; }
        public List<KeyValuePair<string, int>> UlData { get; set; }
        public List<KeyValuePair<int, string>> RangeData { get; set; }
        public List<PressTrendDataModel> TrendData { get; set; }
        public string NormalRange { get; set; }
        public List<PressGridDataModel> GridData { get; set; }
        public PressStatisticsOutput()
        {
            PieData = new List<KeyValuePair<string, float>>();
            UlData = new List<KeyValuePair<string, int>>();
            GridData = new List<PressGridDataModel>();
            RangeData = new List<KeyValuePair<int, string>>();
            TrendData = new List<PressTrendDataModel>();
        }
    }
    public class PressGridDataModel
    {
        public string F_Id { get; set; }
        public string F_Name { get; set; }
        public string F_Gender { get; set; }
        public DateTime? F_BirthDay { get; set; }
        public string F_AgeDesc { get; set; }
        public DateTime F_VisitDate { get; set; }
        public float F_SystolicPressure { get; set; }
        public float F_DiastolicPressure { get; set; }
        public string F_Desc { get; set; }
        public string F_Memo { get; set; }
        public string FilterValue { get; set; }

    }

    public class PressTrendDataModel
    {
        public string MonthDesc { get; set; }
        public List<KeyValuePair<string, float>> Value { get; set; }
        public PressTrendDataModel()
        {
            Value = new List<KeyValuePair<string, float>>();
        }
    }
}
