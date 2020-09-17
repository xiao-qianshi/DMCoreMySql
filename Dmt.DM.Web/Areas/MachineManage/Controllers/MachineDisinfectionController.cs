using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.MachineManage;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Domain.Entity.ReportPrint.MachineDisinfection;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.MachineManage.MachineDisinfection;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.MachineManage.Controllers
{
    [Area("MachineManage")]
    public class MachineDisinfectionController : BaseController
    {
        private readonly IMachineDisinfectionApp _machineDisinfectionApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly IPatVisitApp _patVisitApp;

        public MachineDisinfectionController(IMachineDisinfectionApp machineDisinfectionApp, IMapper mapper, IUsersService usersService, IPatVisitApp patVisitApp)
        {
            _machineDisinfectionApp = machineDisinfectionApp;
            _mapper = mapper;
            _usersService = usersService;
            _patVisitApp = patVisitApp;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var list = await _machineDisinfectionApp.GetList(pagination, keyword);
            var users = _usersService.GetUserNameDict("").Select(t => new
            {
                t.F_Id,
                t.F_RealName
            }).ToList();
            var data = new
            {
                rows = list.Select(t => new
                {
                    t.F_Id,
                    t.F_Mid,
                    t.F_Pid,
                    t.F_PName,
                    t.F_PGender,
                    t.F_Vid,
                    t.F_VisitDate,
                    t.F_VisitNo,
                    t.F_GroupName,
                    t.F_ShowOrder,
                    t.F_DialylisBedNo,
                    t.F_MachineNo,
                    t.F_MachineName,
                    t.F_StartTime,
                    t.F_EndTime,
                    t.F_WipeStartTime,
                    t.F_WipeEndTime,
                    t.F_Option1,
                    t.F_Option1Value,
                    t.F_Option2,
                    t.F_Option2Value,
                    t.F_Option3,
                    t.F_Option4,
                    t.F_Option5,
                    t.F_Option6,
                    t.F_Option6Value,
                    F_OperatePerson = t.F_OperatePerson == null
                        ? ""
                        : users.FirstOrDefault(u => u.F_Id == t.F_OperatePerson)?.F_RealName,
                    F_CheckPerson = t.F_CheckPerson == null
                        ? ""
                        : users.FirstOrDefault(u => u.F_Id == t.F_CheckPerson)?.F_RealName,
                    t.F_Memo,
                    t.F_EnabledMark,
                    t.F_CreatorTime,
                    t.F_CreatorUserId,
                    t.F_LastModifyTime,
                    t.F_LastModifyUserId,
                    t.F_DeleteTime,
                    t.F_DeleteUserId,
                    t.F_DeleteMark
                }),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _machineDisinfectionApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormTransferJson(string keyValue)
        {
            var data = await _machineDisinfectionApp.GetForm(keyValue);
            data.F_OperatePerson = data.F_OperatePerson == null
                ? ""
                : (await _usersService.FindUserAsync(data.F_OperatePerson))?.F_RealName;
            data.F_CheckPerson = data.F_CheckPerson == null
                ? ""
                : (await _usersService.FindUserAsync(data.F_CheckPerson))?.F_RealName;
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _machineDisinfectionApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 生成消毒记录报告
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
            var records = _machineDisinfectionApp.GetListByDate(input.StartDate, input.EndDate)
                .Where(t => t.F_OperatePerson != null)
                .Where(t => bedIds.Contains(t.F_Mid))
                .Select(t => new
                {
                    t.F_CheckPerson,
                    t.F_DialylisBedNo,
                    t.F_EndTime,
                    t.F_GroupName,
                    t.F_MachineName,
                    t.F_MachineNo,
                    t.F_Memo,
                    t.F_Mid,
                    t.F_OperatePerson,
                    t.F_Option1,
                    t.F_Option1Value,
                    t.F_Option2,
                    t.F_Option2Value,
                    t.F_Option3,
                    t.F_Option4,
                    t.F_Option5,
                    t.F_Option6,
                    t.F_Option6Value,
                    t.F_PGender,
                    t.F_Pid,
                    t.F_PName,
                    t.F_ShowOrder,
                    t.F_StartTime,
                    t.F_Vid,
                    t.F_VisitDate,
                    t.F_VisitNo,
                    t.F_WipeStartTime,
                    t.F_WipeEndTime
                })
                //.GroupBy(t => t.F_Mid)
                .OrderBy(t => t.F_ShowOrder).ThenBy(t => t.F_VisitDate).ThenBy(t => t.F_VisitNo)
                .ToList();
            var category = new MachineDisinfectionCategory
            {
                //StartDate = startDate,
                //EndDate = endDate
            };
            //var patVisitApp = new PatVisitApp();
            var users = _usersService.GetUserNameDict("").Select(t => new
            {
                t.F_Id,
                t.F_RealName
            }).ToList();
            foreach (var item in records)
            {
                var findrow = category.MachineInfos.FirstOrDefault(t => t.Mid == item.F_Mid);
                if (findrow == null)
                {
                    findrow = new MachineInfo
                    {
                        Mid = item.F_Mid,
                        DialylisBedNo = item.F_DialylisBedNo,
                        GroupName = item.F_GroupName,
                        MachineName = item.F_MachineName,
                        MachineNo = item.F_MachineNo,
                        StartDate = input.StartDate.ToDateString(),
                        EndDate = input.EndDate.ToDateString()
                    };
                    category.MachineInfos.Add(findrow);
                }

                var element = new DisinfectionInfo
                {
                    CheckPerson = "",
                    DialysisEndTime = "",
                    DialysisStartTime = "",
                    DisinfectType = "",
                    Memo = item.F_Memo,
                    OperatePerson = "",
                    PGender = item.F_PGender,
                    PName = item.F_PName,
                    SurfaceType = item.F_Option6Value ?? "",
                    VisitDate = item.F_VisitDate.ToDate().ToDateString(),
                    VisitNo = item.F_VisitNo.ToInt().ToString(),
                    StartTime = item.F_StartTime == null
                        ? ""
                        : item.F_StartTime.ToDate().ToDateTimeString(true).Substring(11),
                    EndTime =
                        item.F_EndTime == null ? "" : item.F_EndTime.ToDate().ToDateTimeString(true).Substring(11),
                    WipeEndTime = item.F_WipeEndTime == null
                        ? ""
                        : item.F_WipeEndTime.ToDate().ToDateTimeString(true).Substring(11),
                    WipeStartTime = item.F_WipeStartTime == null
                        ? ""
                        : item.F_WipeStartTime.ToDate().ToDateTimeString(true).Substring(11)
                };
                if (item.F_Option1 == true)
                {
                    element.DisinfectType = "热化学消毒(" + item.F_Option1Value + ")";
                }
                if (item.F_Option2 == true)
                {
                    element.DisinfectType = string.IsNullOrEmpty(element.DisinfectType) ? "化学消毒(" + item.F_Option2Value + ")" : element.DisinfectType + ";" + "化学消毒(" + item.F_Option2Value + ")";
                }
                if (item.F_Option3 == true)
                {
                    element.DisinfectType = string.IsNullOrEmpty(element.DisinfectType) ? "热消毒" : element.DisinfectType + ";" + "热消毒";
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
                if (item.F_CheckPerson != null)
                {
                    var finduser = users.FirstOrDefault(t => t.F_Id == item.F_CheckPerson);
                    element.CheckPerson = finduser == null ? "" : finduser.F_RealName;
                }
                findrow.DisinfectionInfos.Add(element);
            }
            return Content(_machineDisinfectionApp.GetImageReport(category.MachineInfos));
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<MachineDisinfectionDto> input)
        {
            //if (entity.F_OperatePerson == null)
            //{
            //    entity.F_OperatePerson = _usersService.GetCurrentUserId();
            //}
            //if (entity.F_EnabledMark == null)
            //{
            //    entity.F_EnabledMark = true;
            //}
            MachineDisinfectionEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<MachineDisinfectionEntity>(input.Entity);
            }
            else
            {
                entity = await _machineDisinfectionApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));

            if (entity.F_OperatePerson == null)
            {
                entity.F_OperatePerson = _usersService.GetCurrentUserId();
            }
            if (entity.F_EnabledMark == null)
            {
                entity.F_EnabledMark = true;
            }
            await _machineDisinfectionApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFormWipe([FromBody]BaseSubmitInput<MachineDisinfectionDto> input)
        {
            MachineDisinfectionEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<MachineDisinfectionEntity>(input.Entity);
            }
            else
            {
                entity = await _machineDisinfectionApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            if (entity.F_CheckPerson == null)
            {
                entity.F_CheckPerson = _usersService.GetCurrentUserId();
            }
            if (entity.F_EnabledMark == null)
            {
                entity.F_EnabledMark = true;
            }
            await _machineDisinfectionApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SignForm([FromBody]BaseInput input)
        {
            var entity = await _machineDisinfectionApp.GetForm(input.KeyValue);
            if (entity == null)
            {
                return Error("未找到记录");
            }
            if (entity.F_OperatePerson == null)
            {
                return Error("请先填写记录！");
            }
            if (entity.F_CheckPerson != null)
            {
                return Error("记录已签名，请核对！");
            }

            entity.F_CheckPerson = _usersService.GetCurrentUserId();
            await _machineDisinfectionApp.UpdateForm(entity);
            return Success("操作成功。");
        }
    }




}
