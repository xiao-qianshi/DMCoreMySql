using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.MachineManage;
using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Domain.Entity.ReportPrint;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.MachineManage.WaterMLog;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.MachineManage.Controllers
{
    [Area("MachineManage")]
    public class WaterMLogController : BaseController
    {
        private readonly IWaterMLogApp _waterMLogApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly IOrganizeApp _organizeApp;

        public WaterMLogController(IWaterMLogApp waterMLogApp, IMapper mapper, IUsersService usersService, IOrganizeApp organizeApp)
        {
            _waterMLogApp = waterMLogApp;
            _mapper = mapper;
            _usersService = usersService;
            _organizeApp = organizeApp;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _waterMLogApp.GetList(pagination, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            if (data.rows.Count > 0)
            {
                var users = _usersService.GetUserNameDict("").Select(t => new {t.F_Id, t.F_RealName});
                foreach (var item in data.rows)
                {
                    if (item.F_OperatePerson != null)
                    {
                        var find = users.FirstOrDefault(t => t.F_Id == item.F_OperatePerson);
                        if (find != null)
                        {
                            item.F_OperatePerson = find.F_RealName;
                        }
                    }
                    if (item.F_CheckPerson != null)
                    {
                        var find = users.FirstOrDefault(t => t.F_Id == item.F_CheckPerson);
                        if (find != null)
                        {
                            item.F_CheckPerson = find.F_RealName;
                        }
                    }
                }
            }
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _waterMLogApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _waterMLogApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<WaterMLogDto> input)
        {
            WaterMLogEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<WaterMLogEntity>(input.Entity);
                if (entity.F_OperatePerson == null)
                {
                    entity.F_OperatePerson = _usersService.GetCurrentUserId();
                }
                if (entity.F_EnabledMark == null)
                {
                    entity.F_EnabledMark = true;
                }
            }
            else
            {
                entity = await _waterMLogApp.GetForm(input.KeyValue);
            }
         
            await _waterMLogApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SignForm([FromBody]BaseInput input)
        {
            var entity = await _waterMLogApp.GetForm(input.KeyValue);
            if (entity == null)
            {
                return Error("未找到记录");
            }
            if (entity.F_CheckPerson != null)
            {
                return Error("记录已核对签名！");

            }
            entity.F_CheckPerson = _usersService.GetCurrentUserId();
            await _waterMLogApp.UpdateForm(entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> CreateReportJson([FromBody]BaseInput input)
        {
            var waterMLog = new WaterMLog();
          
            waterMLog.HospialName = await _organizeApp.GetHospitalName();
            waterMLog.HospialLogo = await _organizeApp.GetHospitalLogo();
            var json = input.KeyValue.ToJObject();
            var startDate = json.Value<DateTime>("startDate");
            var endDate = json.Value<DateTime>("endDate").AddDays(1);
            var rows = await _waterMLogApp.GetList(startDate, endDate);
            //操作者
            var operatePersonIds = rows.Select(t => t.F_OperatePerson).Distinct();
            //核对者
            var checkPersonIds = rows.Select(t => t.F_CheckPerson).Distinct();
            var unionIds = operatePersonIds.Union(checkPersonIds).Distinct().ToList();
            var filterUsers = _usersService.GetUserNameDict("")
                .Where(t => unionIds.Contains(t.F_Id))
                .Select(t =>
                new
                {
                    Id = t.F_Id,
                    RealName = t.F_RealName
                }).ToList();

            waterMLog.StartDate = startDate.ToDateString();
            waterMLog.EndDate = endDate.AddDays(-1).ToDateString();
            waterMLog.PrintDate = DateTime.Now.Date.ToDateString();
            foreach (var item in rows.OrderBy(t => t.F_LogDate))
            {
                item.F_OperatePerson = item.F_OperatePerson == null
                    ? ""
                    : filterUsers.FirstOrDefault(t => t.Id == item.F_OperatePerson)?.RealName;
                item.F_CheckPerson = item.F_CheckPerson == null
                    ? ""
                    : filterUsers.FirstOrDefault(t => t.Id == item.F_CheckPerson)?.RealName;
                waterMLog.Items.Add(new WaterMLogPrint
                {
                    Id = item.F_Id,
                    LogDate = item.F_LogDate.ToDateString(),
                    Value1 = item.F_Value1 == null ? "" : item.F_Value1.ToFloat(2).ToString(),
                    Value2 = item.F_Value2 == null ? "" : item.F_Value2.ToFloat(2).ToString(),
                    Value3 = item.F_Value3 == null ? "" : item.F_Value3.ToFloat(2).ToString(),
                    Value4 = item.F_Value4 == null ? "" : item.F_Value4.ToFloat(2).ToString(),
                    Value5 = item.F_Value5 == null ? "" : item.F_Value5.ToFloat(2).ToString(),
                    Value6 = item.F_Value6 == null ? "" : item.F_Value6.ToFloat(2).ToString(),
                    Value7 = item.F_Value7 == null ? "" : item.F_Value7.ToFloat(2).ToString(),
                    Value8 = item.F_Value8 == null ? "" : item.F_Value8.ToFloat(2).ToString(),
                    Value9 = item.F_Value9 == null ? "" : item.F_Value9.ToFloat(2).ToString(),
                    Option1 = item.F_Option1.ToBool() ? "√" : "",
                    Option2 = item.F_Option2.ToBool() ? "√" : "",
                    Option3 = item.F_Option3.ToBool() ? "√" : "",
                    Option4 = item.F_Option4.ToBool() ? "√" : "",
                    Option5 = item.F_Option5.ToBool() ? "完成" : "",
                    Option6 = item.F_Option6.ToBool() ? "正常" : "",
                    OperatePerson = item.F_OperatePerson,
                    CheckPerson = item.F_OperatePerson,
                    Memo = item.F_Memo
                });
            }
            return Content(_waterMLogApp.GetReport(waterMLog));

        }
    }
}
