using Dmt.DM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.MachineManage;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Domain.Entity.ReportPrint.MachineProcess;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.MachineManage.MachineProcess;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.Areas.MachineManage.Controllers
{
    [Area("MachineManage")]
    public class MachineProcessController : BaseController
    {
        private readonly IMachineProcessApp _machineProcessApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly IPatVisitApp _patVisitApp;


        public MachineProcessController(IMachineProcessApp machineProcessApp, IMapper mapper, IUsersService usersService, IPatVisitApp patVisitApp)
        {
            _machineProcessApp = machineProcessApp;
            _mapper = mapper;
            _usersService = usersService;
            _patVisitApp = patVisitApp;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var json = keyword.ToJObject();
            var visitDate = json.Value<DateTime>("visitDate").Date;
            var visitNo = json.Value<int>("visitNo");

            var data = new
            {
                rows = await _machineProcessApp.GetList(pagination, visitDate, visitNo),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _machineProcessApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<MachineProcessDto> input)
        {
            MachineProcessEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<MachineProcessEntity>(input.Entity);
            }
            else
            {
                entity = await _machineProcessApp.GetForm(input.KeyValue);
            }
            if (entity.F_OperatePerson == null)
            {
                entity.F_OperatePerson = _usersService.GetCurrentUserId();
            }
            if (entity.F_OperateTime == null)
            {
                entity.F_OperateTime = DateTime.Now;
            }
            await _machineProcessApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _machineProcessApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 生成运行日志报告
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">截至日期</param>
        /// <param name="ids">床号ID，以逗号分隔</param>
        /// <param name="isMerge">是否合并打印</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody]CreateReportInput input)
        {
            //选择的床位Id
            var bedIds = input.Ids.Split(',');
            var records = _machineProcessApp.GetListByDate(input.StartDate, input.EndDate)
                .Where(t => t.F_OperatePerson != null)
                .Where(t => bedIds.Contains(t.F_Mid))
                .Select(t => new
                {
                    t.F_DialylisBedNo,
                    t.F_GroupName,
                    t.F_MachineName,
                    t.F_MachineNo,
                    t.F_Memo,
                    t.F_Mid,
                    t.F_OperatePerson,
                    t.F_OperateTime,
                    t.F_Option1,
                    t.F_Option2,
                    t.F_Option3,
                    t.F_Option4,
                    t.F_Option5,
                    t.F_Option6,
                    t.F_PGender,
                    t.F_Pid,
                    t.F_PName,
                    t.F_ShowOrder,
                    t.F_Vid,
                    t.F_VisitDate,
                    t.F_VisitNo
                })
                .OrderBy(t => t.F_ShowOrder).ThenBy(t => t.F_VisitDate).ThenBy(t => t.F_VisitNo)
                .ToList();
            var category = new MachineProcessCategory
            {
            };
            var users = _usersService.GetUserNameDict("").Select(t=>new {t.F_Id,t.F_RealName}).ToList();
            foreach (var item in records)
            {
                var findrow = category.ProcessSummeryInfos.FirstOrDefault(t => t.Mid == item.F_Mid);
                if (findrow == null)
                {
                    findrow = new ProcessSummeryInfo
                    {
                        Mid = item.F_Mid,
                        DialylisBedNo = item.F_DialylisBedNo,
                        GroupName = item.F_GroupName,
                        MachineName = item.F_MachineName,
                        MachineNo = item.F_MachineNo,
                        StartDate = input.StartDate.ToDateString(),
                        EndDate = input.EndDate.ToDateString()
                    };
                    category.ProcessSummeryInfos.Add(findrow);
                }
                var element = new ProcessItem
                {
                    OperateTime = "",
                    DialysisEndTime = "",
                    DialysisStartTime = "",
                    Memo = item.F_Memo,
                    OperatePerson = "",
                    PGender = item.F_PGender,
                    PName = item.F_PName,
                    VisitDate = item.F_VisitDate.ToDate().ToDateString(),
                    VisitNo = item.F_VisitNo.ToInt().ToString()
                };
                element.OperateTime = item.F_OperateTime == null ? "" : item.F_OperateTime.ToDate().ToDateTimeString(true).Substring(11);
                element.Option1 = item.F_Option1 ?? false;
                element.Option2 = item.F_Option2 ?? false;
                element.Option3 = item.F_Option3 ?? false;
                element.Option4 = item.F_Option4 ?? false;
                if (element.Option4 == true)
                {
                    element.Option5 = item.F_Option5 ?? "";
                    element.Option6 = item.F_Option6 ?? "";
                }
                var visitRecord = await _patVisitApp.GetForm(item.F_Vid);
                if (visitRecord != null)
                {
                    element.DialysisStartTime = visitRecord.F_DialysisStartTime == null ? "" : visitRecord.F_DialysisStartTime.ToDate().ToDateTimeString(true).Substring(11);
                    element.DialysisEndTime = visitRecord.F_DialysisEndTime == null ? "" : visitRecord.F_DialysisEndTime.ToDate().ToDateTimeString(true).Substring(11);
                }
                if (item.F_OperatePerson != null)
                {
                    var finduser = users.FirstOrDefault(t => t.F_Id == item.F_OperatePerson);
                    element.OperatePerson = finduser == null ? "" : finduser.F_RealName;
                }
                findrow.ProcessItems.Add(element);
            }
            return Content(_machineProcessApp.GetImageReport(category.ProcessSummeryInfos));
        }
    }
}
