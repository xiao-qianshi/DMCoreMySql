using Dmt.Dm.Domain.Dto.Machine.MachineProcess;
using Dmt.DM.Application;
using Dmt.DM.Application.MachineManage;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.MachineManage
{
    [Route("api/[controller]/[action]")]
    public class MachineProcessController : BaseApiController
    {
        private readonly IMachineProcessApp _machineProcessApp;
        private readonly IPatVisitApp _patVisitApp;
        private readonly IDialysisMachineApp _dialysisMachineApp;
        private readonly IUsersService _usersService;

        public MachineProcessController(IMachineProcessApp machineProcessApp, IPatVisitApp patVisitApp, IDialysisMachineApp dialysisMachineApp, IUsersService usersService)
        {
            _machineProcessApp = machineProcessApp;
            _patVisitApp = patVisitApp;
            _dialysisMachineApp = dialysisMachineApp;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetFilterList(GetFilterListInput input)
        {
            var bedInfo = await _dialysisMachineApp.GetForm(input.keyValue);
            var list = _patVisitApp.GetList()//input.startDate.ToDate(), input.endDate.ToDate(), bedInfo.F_GroupName, bedInfo.F_DialylisBedNo, true
                .Where(t=>t.F_VisitDate >= input.startDate.ToDate() 
                          && t.F_VisitDate <= input.endDate.ToDate()
                          && t.F_GroupName == bedInfo.F_GroupName 
                          && t.F_DialysisBedNo == bedInfo.F_DialylisBedNo
                          && t.F_DialysisStartTime != null
                          && t.F_DialysisEndTime != null
                        )
                .Select(t => new
                {
                    vid = t.F_Id,
                    pid = t.F_Pid,
                    dialysisNo = t.F_DialysisNo,
                    patientName = t.F_Name,
                    patientGender = t.F_Gender,
                    visitDate = t.F_VisitDate,
                    visitNo = t.F_VisitNo,
                    groupName = t.F_GroupName,
                    dialysisBedNo = t.F_DialysisBedNo,
                    dialysisStartTime = t.F_DialysisStartTime,
                    dialysisEndTime = t.F_DialysisEndTime
                }).ToList();
            var processes = _machineProcessApp.GetList(input.startDate.ToDate(), input.endDate.ToDate(), input.keyValue)
                .Select(t => new
                {
                    id = t.F_Id,
                    vid = t.F_Vid,
                    operatePerson = t.F_OperatePerson,
                    operateTime = t.F_OperateTime,
                    option1 = t.F_Option1,
                    option2 = t.F_Option2,
                    option3 = t.F_Option3,
                    option4 = t.F_Option4,
                    option5 = t.F_Option5,
                    option6 = t.F_Option6,
                    memo = t.F_Memo
                }).ToList();
            var data = new
            {
                machine = new
                {
                    groupName = bedInfo.F_GroupName,
                    bedNo = bedInfo.F_DialylisBedNo,
                    showOrder = bedInfo.F_ShowOrder,
                    machineName = bedInfo.F_MachineName,
                    machineNo = bedInfo.F_MachineNo,
                    defaultType = bedInfo.F_DefaultType
                },
                rows = list.GroupJoin(processes, v => v.vid, p => p.vid, (v, p) => new
                {
                    v.vid,
                    isProcessed = p.Count() > 0,
                    //v.dialysisBedNo,
                    v.patientName,
                    v.patientGender,
                    v.visitDate,
                    v.visitNo,
                    v.dialysisStartTime,
                    v.dialysisEndTime,
                    processItem = p.FirstOrDefault()
                }).Select(t => t).OrderBy(t => t.visitDate).ThenBy(t => t.visitNo)
            };
            return Ok(data);
        }

        public async Task<IActionResult> GetForm(BaseInput input)
        {
            var visitEntity = await _patVisitApp.GetForm(input.KeyValue);
            if (visitEntity == null)
            {
                return BadRequest("透析记录主键有误");
            }
            var entity = _machineProcessApp.GetFormByVid(input.KeyValue);
            if (entity == null)
            {
                //生成数据
                var bedInfo = await _dialysisMachineApp.GetForm(visitEntity.F_GroupName, visitEntity.F_DialysisBedNo);
                entity = new MachineProcessEntity
                {
                    F_Id = Common.GuId(),
                    F_Mid = bedInfo.F_Id,
                    F_Pid = visitEntity.F_Pid,
                    F_DialylisNo = visitEntity.F_DialysisNo,
                    F_PName = visitEntity.F_Name,
                    F_PGender = visitEntity.F_Gender,
                    F_Vid = visitEntity.F_Id,
                    F_VisitDate = visitEntity.F_VisitDate,
                    F_VisitNo = visitEntity.F_VisitNo,
                    F_GroupName = bedInfo.F_GroupName,
                    F_ShowOrder = bedInfo.F_ShowOrder,
                    F_DialylisBedNo = bedInfo.F_DialylisBedNo,
                    F_MachineName = bedInfo.F_MachineName,
                    F_MachineNo = bedInfo.F_MachineNo,
                    F_EnabledMark = true,
                    F_CreatorTime = DateTime.Now,
                    F_CreatorUserId = _usersService.GetCurrentUserId()
                };
                _machineProcessApp.InsertForm(entity);
            }
            var data = new
            {
                id = entity.F_Id,
                operatePerson = entity.F_OperatePerson == null ? "" : (await _usersService.FindUserAsync(entity.F_OperatePerson))?.F_RealName,
                operateTime = entity.F_OperateTime,
                option1 = entity.F_Option1,
                option2 = entity.F_Option2,
                option3 = entity.F_Option3,
                option4 = entity.F_Option4,
                option5 = entity.F_Option5,
                option6 = entity.F_Option6,
                memo = entity.F_Memo
            };
            return Ok(data);
        }

        [HttpPost]
        public IActionResult SubmitData([FromBody]SubmitDataInput input)
        {
            var entity = new MachineProcessEntity
            {
                F_Id = input.id,
                F_Option1 = input.option1,
                F_Option2 = input.option2,
                F_Option3 = input.option3,
                F_Option4 = input.option4,
                F_Option5 = input.option5,
                F_Option6 = input.option6,
                F_Memo = input.memo,
                F_OperatePerson = _usersService.GetCurrentUserId(),
                F_OperateTime = DateTime.Now
            };
            _machineProcessApp.UpdateForm(entity);
            return Ok("操作成功");
        }
    }
}
