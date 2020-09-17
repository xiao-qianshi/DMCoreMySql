using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.Areas.DataStatistics.Controllers
{
    [Area("DataStatistics")]
    public class DialysisRecordController : BaseController
    {
        private readonly IPatVisitApp _patVisit;
        private readonly IDrugsApp _drugsApp;

        public DialysisRecordController(IPatVisitApp patVisit, IDrugsApp drugsApp)
        {
            _patVisit = patVisit;
            _drugsApp = drugsApp;
        }


        public ActionResult GetRecordCountJson(string keyValue)
        {
            DialysisRecordOutput output = new DialysisRecordOutput();
            var json = keyValue.ToJObject();
            var startDate = json.Value<DateTime>("startDate");
            var endDate = json.Value<DateTime>("endDate");
            var pid = json.Value<string>("pid");

            //查询治疗记录

            var visitQuery = _patVisit.GetList().Where(t =>
                t.F_Pid == pid && t.F_DialysisEndTime != null && t.F_VisitDate >= startDate &&
                t.F_VisitDate <= endDate && t.F_EnabledMark != false);
            var list = (from r in visitQuery
                        //join d in _drugsApp.GetSelectJson() on r.F_HeparinType equals d.F_Id
                        //into temp
                        //from t in temp.DefaultIfEmpty()
                        orderby r.F_VisitDate
                        select new
                        {
                            dialysisType = r.F_DialysisType,
                            id = r.F_Id,
                            r.F_AccessName,
                            r.F_BedNo,
                            r.F_BirthDay,
                            r.F_BloodSpeed,
                            r.F_DialysisBedNo,
                            r.F_DialysisEndTime,
                            r.F_DialysisNo,
                            r.F_DialysisStartTime,
                            r.F_DialysisType,
                            r.F_DialyzerType1,
                            r.F_DialyzerType2,
                            r.F_Gender,
                            r.F_GroupName,
                            //r.F_HeparinType,
                            //t == null? "" : t.F_DrugName,
                            //F_HeparinType = t == null ? "" : t.F_DrugName,
                            r.F_InpNo,
                            r.F_Machine,
                            r.F_Name,
                            r.F_PatientSourse,
                            r.F_Pid,
                            r.F_VascularAccess,
                            r.F_VisitDate,
                            r.F_VisitNo
                        }).ToList();

            output.PatientId = pid;
            output.TotalCount = list.Count;

            output.Rows = list;

            //List<KeyValuePair<string, int>> data = new List<KeyValuePair<string, int>>();
            foreach (var item in list.GroupBy(t => t.dialysisType))
            {
                DialysisRecordItem dialysisRecordItem = new DialysisRecordItem
                {
                    DialysisType = item.Key,
                    Count = item.Count(),
                    Percent = (((float)item.Count()) / output.TotalCount).ToFloat(2)
                };
                output.Items.Add(dialysisRecordItem);
            }

            foreach (var item in list.GroupBy(t => t.F_VisitDate))
            {
                DialysisRecordGroup recordItem = new DialysisRecordGroup
                {
                    VisitDate = item.Key.ToDateString().Substring(5),
                };

                foreach (var ele in item.GroupBy(t => t.dialysisType))
                {
                    recordItem.Value.Add(new KeyValuePair<string, int>(
                        ele.Key, ele.Count()
                        ));
                }
                output.GroupItems.Add(recordItem);
            }
            return Content(output.ToJson());
        }
    }

    public class DialysisRecordOutput
    {
        public int TotalCount { get; set; }
        public string PatientId { get; set; }
        public List<DialysisRecordItem> Items { get; set; }
        public List<DialysisRecordGroup> GroupItems { get; set; }
        /// <summary>
        /// 明细数据
        /// </summary>
        public object Rows { get; set; }
        public DialysisRecordOutput()
        {
            Items = new List<DialysisRecordItem>();
            GroupItems = new List<DialysisRecordGroup>();
        }

    }
    /// <summary>
    /// 透析模式分组
    /// </summary>
    public class DialysisRecordItem
    {
        public string DialysisType { get; set; }
        public int Count { get; set; }
        public float Percent { get; set; }
    }

    /// <summary>
    /// 日期分组
    /// </summary>
    public class DialysisRecordGroup
    {
        public string VisitDate { get; set; }
        //public int Count { get; set; }
        public List<KeyValuePair<string, int>> Value { get; set; }
        public DialysisRecordGroup()
        {
            Value = new List<KeyValuePair<string, int>>();
        }
    }

}
