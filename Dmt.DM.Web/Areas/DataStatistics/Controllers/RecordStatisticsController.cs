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
    public class RecordStatisticsController : BaseController
    {
        private readonly IPatVisitApp _patVisitApp;
        private readonly IDialysisMachineApp _machineApp;

        public RecordStatisticsController(IPatVisitApp patVisitApp, IDialysisMachineApp machineApp)
        {
            _patVisitApp = patVisitApp;
            _machineApp = machineApp;
        }

        /// <summary>
        /// 查询今日透析数据  已透析 待透析  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRecordSummary()
        {
            //总数
            var bedCount = _machineApp.GetQueryable().Count();

            //今日记录
            var records = _patVisitApp.GetList().Where(t=>t.F_VisitDate == DateTime.Today && t.F_EnabledMark == true && t.F_DeleteMark !=true);//.FindAll(t => t.F_DialysisStartTime != null && t.F_DialysisEndTime == null)

            //透析中数量
            var usingCount = records.Count(t => t.F_DialysisStartTime != null && t.F_DialysisEndTime == null);

            //已透析数量
            var finishCount = records.Count(t => t.F_DialysisEndTime != null);

            var todoCount = records.Count(t => t.F_DialysisStartTime == null);

            var leftBedCount = bedCount - usingCount;

            leftBedCount = leftBedCount < 0 ? 0 : leftBedCount;

            //本月透析人数
            var monthStartDay = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            var monthCount = (from r in _patVisitApp.GetList()
                              where r.F_VisitDate >= monthStartDay && r.F_DialysisEndTime != null
                              select r.F_Id).Count();

            //统计最近15天的数据
            var startDate = DateTime.Now.AddDays(-14).Date;
            var endDate = DateTime.Now.Date;
            var _15daysRecords = (from r in _patVisitApp.GetList()
                                  where r.F_VisitDate >= startDate && r.F_VisitDate <= endDate && r.F_DialysisEndTime != null
                                  orderby r.F_VisitDate
                                  select new
                                  {
                                      date = r.F_VisitDate.ToDateString().Substring(5),
                                      mode = r.F_DialysisType
                                  }).ToList();
            var lineX = new List<string>();
            var lineY = new List<int>();
            var circleX = new List<string>();
            var circleY = new List<int>();
            foreach (var item in _15daysRecords)
            {
                if (lineX.IndexOf(item.date) < 0)
                {
                    lineX.Add(item.date);
                    lineY.Add(_15daysRecords.Count(t => t.date.Equals(item.date)));
                }

                if (circleX.IndexOf(item.mode) < 0)
                {
                    circleX.Add(item.mode);
                    circleY.Add(_15daysRecords.Count(t => t.mode.Equals(item.mode)));
                }
            }

            var data = new
            {
                todoCount,
                finishCount,
                usingCount,
                leftBedCount,
                bedCount,
                monthCount,
                line = new
                {
                    lineX,
                    lineY
                },
                circle = new
                {
                    circleX,
                    circleY
                }
            };

            return Content(data.ToJson());
        }

    }
}
