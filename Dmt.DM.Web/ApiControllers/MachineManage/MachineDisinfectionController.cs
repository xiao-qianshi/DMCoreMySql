using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Application.MachineManage;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.Dm.Domain.Dto.Machine.MachineDisinfection;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.ApiControllers.MachineManage
{
    [Route("api/[controller]/[action]")]
    public class MachineDisinfectionController : BaseApiController
    {
        private readonly IMachineDisinfectionApp _machineDisinfectionApp;
        private readonly IUsersService _usersService;
        private readonly IPatVisitApp _patVisitApp;

        public MachineDisinfectionController(IMachineDisinfectionApp machineDisinfectionApp, IUsersService usersService, IPatVisitApp patVisitApp)
        {
            _machineDisinfectionApp = machineDisinfectionApp;
            _usersService = usersService;
            _patVisitApp = patVisitApp;
        }

        /// <summary>
        /// 通过治疗单ID查询消毒记录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetFormByVid(BaseInput input)
        {
            var visitEntity = await _patVisitApp.GetForm(input.KeyValue);
            if (visitEntity == null)
            {
                return BadRequest("治疗单主键有误!");
            }
            var data = new GetFormByVidOutput();
            var query = _machineDisinfectionApp.GetList().Where(t => t.F_Vid.Equals(input.KeyValue));
            if (!query.Any())
            {
                //生成记录
                var msg = _machineDisinfectionApp.CreateSingleData(input.KeyValue, _usersService.GetCurrentUserId());
                if (!string.IsNullOrEmpty(msg)) return BadRequest(msg);
            }
            var entity = query.Select(t => new
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
                t.F_OperatePerson,
                t.F_CheckPerson,
                t.F_Memo
            }).FirstOrDefault();
            if (entity != null)
            {
                data.id = entity.F_Id;
                data.Mid = entity.F_Mid;
                data.Pid = entity.F_Pid;
                data.PName = entity.F_PName;
                data.PGender = entity.F_PGender;
                data.Vid = entity.F_Vid;
                data.VisitDate = entity.F_VisitDate;
                data.VisitNo = entity.F_VisitNo;
                data.GroupName = entity.F_GroupName;
                data.ShowOrder = entity.F_ShowOrder;
                data.DialylisBedNo = entity.F_DialylisBedNo;
                data.MachineNo = entity.F_MachineNo;
                data.MachineName = entity.F_MachineName;
                data.StartTime = entity.F_StartTime;
                data.EndTime = entity.F_EndTime;
                data.WipeEndTime = entity.F_WipeEndTime;
                data.WipeStartTime = entity.F_WipeStartTime;
                data.Option1 = entity.F_Option1;
                data.Option1Value = entity.F_Option1Value;
                data.Option2 = entity.F_Option2;
                data.Option2Value = entity.F_Option2Value;
                data.Option3 = entity.F_Option3;
                data.Option4 = entity.F_Option4;
                data.Option5 = entity.F_Option5;
                data.Option6 = entity.F_Option6;
                data.Option6Value = entity.F_Option6Value;
                data.OperatePerson = entity.F_OperatePerson;
                data.CheckPerson = entity.F_CheckPerson;
                if (!string.IsNullOrEmpty(data.CheckPerson))
                {
                    var find = await _usersService.FindUserAsync(data.CheckPerson);
                    data.CheckPerson = find == null ? "" : find.F_RealName;
                }
                if (!string.IsNullOrEmpty(data.OperatePerson))
                {
                    var find = await _usersService.FindUserAsync(data.OperatePerson);
                    data.OperatePerson = find == null ? "" : find.F_RealName;
                }
                data.DialysisStartTime = visitEntity.F_DialysisStartTime;
                data.DialysisEndTime = visitEntity.F_DialysisEndTime;
            }
            return Ok(data);
        }

        /// <summary>
        /// 水路消毒签名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult Step1Sign([FromBody]Step1SignInput input)
        {
            if (string.IsNullOrEmpty(input.id))
                return BadRequest("主键ID未传值");
            if (input.StartTime == null || input.EndTime == null)
                return BadRequest("消毒起始或截至时间未传值");
            if (input.Option1.ToBool() || input.Option2.ToBool() || input.Option3.ToBool())
                return BadRequest("未选择消毒方法");
            _machineDisinfectionApp.UpdateForm(new MachineDisinfectionEntity
            {
                F_Id = input.id,
                F_StartTime = input.StartTime,
                F_EndTime = input.EndTime,
                F_Option1 = input.Option1,
                F_Option1Value = input.Option1Value,
                F_Option2 = input.Option2,
                F_Option2Value = input.Option2Value,
                F_Option3 = input.Option3,
                F_Option4 = input.Option4,
                F_Option5 = input.Option5,
                F_Memo = input.Memo,
                F_OperatePerson = _usersService.GetCurrentUserId()
            });
            return Ok("操作成功");
        }

        /// <summary>
        /// 表面擦拭消毒签名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult Step2Sign([FromBody]Step2SignInput input)
        {
            if (string.IsNullOrEmpty(input.id))
                return BadRequest("主键ID未传值");
            if (input.WipeEndTime == null || input.WipeStartTime == null)
                return BadRequest("消毒起始或截至时间未传值");
            if (input.Option6.ToBool() && string.IsNullOrEmpty(input.Option6Value))
                return BadRequest("未选择消毒药物");
            _machineDisinfectionApp.UpdateForm(new MachineDisinfectionEntity
            {
                F_Id = input.id,
                F_WipeEndTime = input.WipeEndTime,
                F_WipeStartTime = input.WipeStartTime,
                F_Option6 = input.Option6,
                F_Option6Value = input.Option6Value,
                F_CheckPerson = _usersService.GetCurrentUserId()
            });
            return Ok("操作成功");
        }

        public IActionResult GetList(GetListInput input)
        {
            var list = _patVisitApp.GetList(input.visitDate, input.visitNo).ToList();
            if (!string.IsNullOrEmpty(input.bedNo)) list = list.Where(t => t.F_DialysisBedNo.Equals(input.bedNo)).ToList();
            var groups = (input.groupNames + "").Split(',');
            list = list.Where(t => groups.Contains(t.F_GroupName))
                .Where(t => t.F_DialysisStartTime != null && t.F_DialysisEndTime != null)
                .ToList();
            var target = _machineDisinfectionApp.GetListByDate(input.visitDate, input.visitDate)
                .Select(t => new
                {
                    t.F_Id,
                    t.F_Vid,
                    t.F_StartTime,
                    t.F_EndTime,
                    t.F_Option1,
                    t.F_Option1Value,
                    t.F_Option2,
                    t.F_Option2Value,
                    t.F_Option3,
                    t.F_Option4,
                    t.F_Option5,
                    t.F_OperatePerson,
                    t.F_WipeStartTime,
                    t.F_WipeEndTime,
                    t.F_Option6,
                    t.F_Option6Value,
                    t.F_CheckPerson
                });
            var output = new GetListOutput();
            //保存操作用户字典
            //Hashtable table = new Hashtable();
            var users = _usersService.GetUserNameDict("").Select(t => new
            {
                id = t.F_Id,
                name = t.F_RealName
            }).ToList();
            foreach (var item in list)
            {
                DisinfectionInfo disinfection = new DisinfectionInfo
                {
                    vId = item.F_Id,
                    groupName = item.F_GroupName,
                    bedNo = item.F_DialysisBedNo,
                    visitNo = item.F_VisitNo.ToInt(),
                    patientName = item.F_Name,
                    dialysisStartTime = item.F_DialysisStartTime,
                    dialysisEndTime = item.F_DialysisEndTime
                };
                var find = target.FirstOrDefault(t => t.F_Vid.Equals(item.F_Id));
                if (find == null)
                {
                    disinfection.beDisinfected = false;
                }
                else
                {

                    if (string.IsNullOrEmpty(find.F_OperatePerson) || string.IsNullOrEmpty(find.F_CheckPerson))
                        disinfection.beDisinfected = false;
                    else
                        disinfection.beDisinfected = true;
                    disinfection.StartTime = find.F_StartTime;
                    disinfection.EndTime = find.F_EndTime;
                    disinfection.Option1 = find.F_Option1;
                    disinfection.Option1Value = find.F_Option1Value;
                    disinfection.Option2 = find.F_Option2;
                    disinfection.Option2Value = find.F_Option2Value;
                    disinfection.Option3 = find.F_Option3;
                    disinfection.Option4 = find.F_Option4;
                    disinfection.Option5 = find.F_Option5;
                    disinfection.Option6 = find.F_Option6;
                    disinfection.Option6Value = find.F_Option6Value;
                    disinfection.WipeStartTime = find.F_WipeStartTime;
                    disinfection.WipeEndTime = find.F_WipeEndTime;
                    disinfection.OperatePerson = find.F_OperatePerson == null ? "" : users.First(t => t.id.Equals(find.F_OperatePerson)).name;
                    disinfection.CheckPerson = find.F_CheckPerson == null ? "" : users.First(t => t.id.Equals(find.F_CheckPerson)).name;
                }
                output.items.Add(disinfection);
            }
            output.bedNo = input.bedNo;
            output.groupNames = input.groupNames;
            output.visitDate = input.visitDate;
            output.visitNo = input.visitNo;
            return Ok(output);
        }
    }
}
