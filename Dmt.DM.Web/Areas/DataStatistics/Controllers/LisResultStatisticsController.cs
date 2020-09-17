using Dmt.DM.Application.PatientManage;
using Dmt.DM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Code;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.Areas.DataStatistics.Controllers
{
    [Area("DataStatistics")]
    public class LisResultStatisticsController : BaseController
    {
        private readonly IQualityResultApp _qualityResultApp;
        private readonly IQualityItemApp _qualityItemApp;
        private readonly IPatientApp _patientApp;

        public LisResultStatisticsController(IQualityResultApp qualityResultApp, IQualityItemApp qualityItemApp, IPatientApp patientApp)
        {
            _qualityResultApp = qualityResultApp;
            _qualityItemApp = qualityItemApp;
            _patientApp = patientApp;
        }

        /// <summary>
        /// 检验结果汇总信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>

        public async Task<IActionResult> GetLisResultSumJson(string keyValue)
        {
            var json = keyValue.ToJObject();
            var startDate = json.Value<DateTime>("startDate");
            var endDate = json.Value<DateTime>("endDate");
            var itemCode = json.Value<string>("itemCode");
            var list = await _qualityResultApp.GetListByItemCode(startDate, endDate.AddDays(1), itemCode);

            var output = new LisResultStatisticsOutput();

            if (list.Count == 0) return Error("无检验数据");
            //读取分段信息
            var partitions = _qualityItemApp.GetPartitionList(itemCode).ToList();
            if (!partitions.Any()) return Error("未设置分段信息");
            var partitionList = partitions.Select(t => new
            {
                OrderNo = t.F_OrderNo,
                LowerCheck = t.F_LowerCheck,
                LowerValue = t.F_LowerValue.ToFloat(3),
                UpperCheck = t.F_UpperCheck,
                UpperValue = t.F_UpperValue.ToFloat(3),
                FilterType = (t.F_LowerValue != null && t.F_UpperValue == null) ? 1
                                : (t.F_LowerValue != null && t.F_UpperValue != null) ? 2
                                : (t.F_LowerValue == null && t.F_UpperValue != null) ? 3
                                : -1,
                FilterValue = (t.F_LowerValue != null && t.F_UpperValue == null) ? (t.F_LowerCheck ? "<=" + t.F_LowerValue.ToFloat(3) : "<" + t.F_LowerValue.ToFloat(3))    // 小于或小于等于最小值
                          : (t.F_LowerValue != null && t.F_UpperValue != null) ?
                                (!t.F_LowerCheck && !t.F_UpperCheck) ? (">" + t.F_LowerValue.ToFloat(3) + "  <" + t.F_UpperValue.ToFloat(3))    //两者之间  ，四种情况
                                    : (t.F_LowerCheck && !t.F_UpperCheck) ? (">=" + t.F_LowerValue.ToFloat(3) + "  <" + t.F_UpperValue.ToFloat(3))
                                    : (!t.F_LowerCheck && t.F_UpperCheck) ? (">" + t.F_LowerValue.ToFloat(3) + "  <=" + t.F_UpperValue.ToFloat(3))
                                    : (">=" + t.F_LowerValue.ToFloat(3) + "  <=" + t.F_UpperValue.ToFloat(3))
                          : (t.F_LowerValue == null && t.F_UpperValue != null) ? (t.F_UpperCheck ? ">=" + t.F_UpperValue.ToFloat(3) : ">" + t.F_UpperValue.ToFloat(3))  //大于或大于等于最大值
                          : ""
            }).Where(t => t.FilterType > 0).OrderBy(t => t.OrderNo).ToList();

            //pie title data
            output.Title = startDate.ToChineseDateString() + " 至 " + endDate.ToChineseDateString() + list[0].F_ItemName + "统计数据"; //+ "(" + list[0].F_ItemCode + ")"
            //var qualityCode = list[0].F_ItemCode.ToUpper();

            var detailList = from r in list
                             join p in _patientApp.GetQueryable() on r.F_Pid equals p.F_Id
                             select new
                             {
                                 r.F_Id,
                                 p.F_Name,
                                 p.F_Gender,
                                 p.F_BirthDay,
                                 r.F_ReportTime,
                                 r.F_ItemCode,
                                 r.F_ItemName,
                                 r.F_Result,
                                 r.F_Unit,
                                 r.F_Memo
                             };

            //if (qualityCode.Equals("HGB")) //血常规统计 规则
            if (true) //血常规统计 规则
            {
                foreach (var item in detailList)
                {
                    GridDataModel model = new GridDataModel
                    {
                        F_Id = item.F_Id,
                        F_Name = item.F_Name,
                        F_Gender = item.F_Gender,
                        F_BirthDay = item.F_BirthDay,
                        F_ReportTime = item.F_ReportTime,
                        F_ItemCode = item.F_ItemCode,
                        F_ItemName = item.F_ItemName,
                        F_Result = item.F_Result.ToFloat(2),
                        F_Unit = item.F_Unit,
                        F_Memo = item.F_Memo
                    };
                    if (model.F_BirthDay != null)
                    {
                        model.F_AgeDesc = ((int)((DateTime.Now - (DateTime)model.F_BirthDay).TotalDays / 365)).ToString() + "岁";
                    }
                    var find = partitionList.FirstOrDefault(
                        t => (t.FilterType == 1 && (t.LowerCheck ? t.LowerValue >= model.F_Result : t.LowerValue > model.F_Result)) ||
                            (t.FilterType == 2 && ((t.LowerCheck ? t.LowerValue <= model.F_Result : t.LowerValue < model.F_Result)
                                                  && (t.UpperCheck ? t.UpperValue >= model.F_Result : t.UpperValue > model.F_Result))) ||
                            (t.FilterType == 3 && (t.UpperCheck ? model.F_Result >= t.UpperValue : model.F_Result > t.UpperValue))
                        );
                    model.FilterValue = find?.FilterValue;
                    output.GridData.Add(model);
                }
                var rangeCount = 1;
                foreach (var item in partitionList)
                {
                    output.RangeData.Add(new KeyValuePair<int, string>(rangeCount++, item.FilterValue));
                    var findCount = output.GridData.Count(t => t.FilterValue == item.FilterValue);
                    output.PieData.Add(new KeyValuePair<string, float>(item.FilterValue, (findCount * 100 / list.Count).ToFloat(2)));
                    if (findCount > 0)
                    {
                        output.UlData.Add(new KeyValuePair<string, int>(item.FilterValue, findCount));
                    }
                }

                output.NormalRange = partitionList[0].FilterValue;

                //趋势图  最近6个月
                //var rows = 
                var trendDateStart = DateTime.Parse(DateTime.Now.AddMonths(-6).ToString("yyyy-MM-01")); //6个月前 1号
                var trendDateEnd = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")); //本月前 1号
                var rows = from r in await _qualityResultApp.GetTrendList(trendDateStart, trendDateEnd, itemCode)
                           select new
                           {
                               ReportTime = r.F_ReportTime,
                               Pid = r.F_Pid,
                               ReportMonth = r.F_ReportTime.ToDateString().Substring(0, 7),
                               Result = r.F_Result.ToFloat(2)
                           };
                //rows.OrderBy(t => t.ReportTime);
                foreach (var item in rows.GroupBy(t => t.ReportMonth))
                {
                    TrendDataModel dataModel = new TrendDataModel
                    {
                        MonthDesc = item.Key
                    };
                    List<float> templist = new List<float>();
                    foreach (var ele in item.GroupBy(t => t.Pid))
                    {
                        templist.Add(ele.OrderByDescending(t => t.ReportTime).First().Result);
                    }

                    foreach (var child in partitionList)
                    {
                        int count = 0;

                        if (child.FilterType == 1)
                        {
                            count = templist.Count(t => child.LowerCheck ? t <= child.LowerValue : t < child.LowerValue);
                        }
                        else if (child.FilterType == 2)
                        {
                            count = templist.Count(t => (child.LowerCheck ? t >= child.LowerValue : t > child.LowerValue) && (child.UpperCheck ? t <= child.UpperValue : t < child.UpperValue));
                        }
                        else if (child.FilterType == 3)
                        {
                            count = templist.Count(t => child.UpperCheck ? t >= child.LowerValue : t > child.LowerValue);
                        }
                        else
                        {
                            continue;
                        }
                        dataModel.Value.Add(new KeyValuePair<string, float>(child.FilterValue, (count * 100 / templist.Count).ToFloat(2)));
                    }
                    output.TrendData.Add(dataModel);
                }
                output.TrendData = output.TrendData.OrderBy(t => t.MonthDesc).ToList();
            }

            return Content(output.ToJson());
        }
    }

    public class LisResultStatisticsOutput
    {
        public string Title { get; set; }
        public List<KeyValuePair<string, float>> PieData { get; set; }
        public List<KeyValuePair<string, int>> UlData { get; set; }
        public List<KeyValuePair<int, string>> RangeData { get; set; }
        public List<TrendDataModel> TrendData { get; set; }
        public string NormalRange { get; set; }
        public List<GridDataModel> GridData { get; set; }
        public LisResultStatisticsOutput()
        {
            PieData = new List<KeyValuePair<string, float>>();
            UlData = new List<KeyValuePair<string, int>>();
            GridData = new List<GridDataModel>();
            RangeData = new List<KeyValuePair<int, string>>();
            TrendData = new List<TrendDataModel>();
        }
    }
    public class GridDataModel
    {
        public string F_Id { get; set; }
        public string F_Name { get; set; }
        public string F_Gender { get; set; }
        public DateTime? F_BirthDay { get; set; }
        public string F_AgeDesc { get; set; }
        public DateTime F_ReportTime { get; set; }
        public string F_ItemCode { get; set; }
        public string F_ItemName { get; set; }
        public float F_Result { get; set; }
        public string F_Unit { get; set; }
        public string Title { get; set; }
        public string F_Memo { get; set; }
        public string FilterValue { get; set; }
    }

    public class TrendDataModel
    {
        public string MonthDesc { get; set; }
        public List<KeyValuePair<string, float>> Value { get; set; }
        public TrendDataModel()
        {
            Value = new List<KeyValuePair<string, float>>();
        }

        public int Compare(TrendDataModel x, TrendDataModel y)
        {
            DateTime xDate = DateTime.Parse(x.MonthDesc + "01");
            DateTime YDate = DateTime.Parse(y.MonthDesc + "01");
            if (xDate < YDate) return -1;
            else if (xDate == YDate) return 0;
            else return 1;
        }

        public int Compare(object x, object y)
        {
            DateTime xDate = DateTime.Parse(((TrendDataModel)x).MonthDesc + "01");
            DateTime YDate = DateTime.Parse(((TrendDataModel)y).MonthDesc + "01");
            if (xDate < YDate) return -1;
            else if (xDate == YDate) return 0;
            else return 1;
        }
    }
}
