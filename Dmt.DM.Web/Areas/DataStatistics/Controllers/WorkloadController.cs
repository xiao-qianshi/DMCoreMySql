using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.Areas.DataStatistics.Controllers
{
    [Area("DataStatistics")]
    public class WorkloadController : BaseController
    {
        private readonly IPatVisitApp _patVisitApp;
        private readonly IUsersService _usersService;

        public WorkloadController(IPatVisitApp patVisitApp, IUsersService usersService)
        {
            _patVisitApp = patVisitApp;
            _usersService = usersService;

            
        }

        public IActionResult GetRecordCountJson(string keyValue)
        {
            WorkloadOutput output = new WorkloadOutput();
            var json = keyValue.ToJObject();
            var startDate = json.Value<DateTime>("startDate");
            var endDate = json.Value<DateTime>("endDate");
            var pid = json.Value<string>("pid");
            var users = _usersService.GetUserNameDict("").Select(t=>new {t.F_Id,t.F_RealName}).ToList();

            //查询治疗记录   穿刺  核对  上机  下机
            //var list = _patVisitApp.GetList()
            //    .Where(t => t.F_VisitDate >= startDate && t.F_VisitDate <= endDate)
            //    .Select(r => new
            //    {
            //        r.F_PuncturePerson,
            //        r.F_StartPerson,
            //        r.F_CheckPerson,
            //        r.F_EndPerson
            //    });
            var query = _patVisitApp.GetList()
                .Where(t => t.F_VisitDate >= startDate && t.F_VisitDate <= endDate && t.F_DeleteMark != true);
            query = query.Where(t =>
                t.F_PuncturePerson != null || t.F_StartPerson != null || t.F_CheckPerson != null ||
                t.F_EndPerson != null);
            if (!pid.IsEmpty())
                query = query.Where(t =>
                    t.F_StartPerson == pid || t.F_EndPerson == pid || t.F_CheckPerson == pid ||
                    t.F_PuncturePerson == pid);
            var list = query.Select(r => new
            {
                r.F_PuncturePerson,
                r.F_StartPerson,
                r.F_CheckPerson,
                r.F_EndPerson
            }).ToList();

            foreach (var item in list)
            {
                if (item.F_PuncturePerson != null)
                {
                    if (!string.IsNullOrEmpty(pid))
                    {
                        if (!pid.Equals(item.F_PuncturePerson)) continue;
                    }
                    output.GroupItems.PunctureSum++;
                    var find = output.Items.FirstOrDefault(t => t.UId.Equals(item.F_PuncturePerson));
                    if (find == null)
                    {
                        find = new WorkloadItem
                        {
                            UId = item.F_PuncturePerson,
                            UName = users.FirstOrDefault(t => t.F_Id.Equals(item.F_PuncturePerson))?.F_RealName ?? ""
                        };
                        output.Items.Add(find);
                    }
                    find.PunctureCount++;
                }
                if (item.F_StartPerson != null)
                {
                    if (!string.IsNullOrEmpty(pid))
                    {
                        if (!pid.Equals(item.F_StartPerson)) continue;
                    }
                    output.GroupItems.StarteSum++;
                    var find = output.Items.FirstOrDefault(t => t.UId.Equals(item.F_StartPerson));
                    if (find == null)
                    {
                        find = new WorkloadItem
                        {
                            UId = item.F_StartPerson,
                            UName = users.FirstOrDefault(t => t.F_Id.Equals(item.F_StartPerson))?.F_RealName ?? ""
                        };
                        output.Items.Add(find);
                    }
                    find.StarteCount++;
                }
                if (item.F_CheckPerson != null)
                {
                    if (!string.IsNullOrEmpty(pid))
                    {
                        if (!pid.Equals(item.F_CheckPerson)) continue;
                    }
                    output.GroupItems.CheckSum++;
                    var find = output.Items.FirstOrDefault(t => t.UId.Equals(item.F_CheckPerson));
                    if (find == null)
                    {
                        find = new WorkloadItem
                        {
                            UId = item.F_CheckPerson,
                            UName = users.FirstOrDefault(t => t.F_Id.Equals(item.F_CheckPerson))?.F_RealName ?? ""
                        };
                        output.Items.Add(find);
                    }
                    find.CheckCount++;
                }
                if (item.F_EndPerson != null)
                {
                    if (!string.IsNullOrEmpty(pid))
                    {
                        if (!pid.Equals(item.F_EndPerson)) continue;
                    }
                    output.GroupItems.EndSum++;
                    var find = output.Items.FirstOrDefault(t => t.UId.Equals(item.F_EndPerson));
                    if (find == null)
                    {
                        find = new WorkloadItem
                        {
                            UId = item.F_EndPerson,
                            UName = users.FirstOrDefault(t => t.F_Id.Equals(item.F_EndPerson))?.F_RealName ?? ""
                        };
                        output.Items.Add(find);
                    }
                    find.EndCount++;
                }
            }

            return Content(output.ToJson());
        }


    }

    public class WorkloadOutput
    {
        public List<WorkloadItem> Items { get; set; }
        public WorkloadSum GroupItems { get; set; }

        public WorkloadOutput()
        {
            Items = new List<WorkloadItem>();
            GroupItems = new WorkloadSum();
        }

    }
    /// <summary>
    /// 明细
    /// </summary>
    public class WorkloadItem
    {
        public string UId { get; set; }
        public string UName { get; set; }
        public int PunctureCount { get; set; }
        public int StarteCount { get; set; }
        public int CheckCount { get; set; }
        public int EndCount { get; set; }
    }

    /// <summary>
    /// 汇总
    /// </summary>
    public class WorkloadSum
    {
        public int PunctureSum { get; set; }
        public int StarteSum { get; set; }
        public int CheckSum { get; set; }
        public int EndSum { get; set; }
    }
}
