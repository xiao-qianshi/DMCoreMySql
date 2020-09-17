using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Application.MachineManage;
using Dmt.DM.Code;
using Dmt.Dm.Domain.Dto.Machine.WaterMDisinfect;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.ApiControllers.MachineManage
{
    [Route("api/[controller]/[action]")]
    public class WaterMDisinfectController : BaseApiController
    {
        private readonly IWaterMDisinfectApp _waterMDisinfectApp;
        private readonly IUsersService _usersService;

        public WaterMDisinfectController(IWaterMDisinfectApp waterMDisinfectApp, IUsersService usersService)
        {
            _waterMDisinfectApp = waterMDisinfectApp;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetPagedList(GetPagedListInput input)
        {
            var pagination = new Pagination
            {
                rows = input.rows,
                page = input.page,
                sidx = input.orderField ?? "F_DisinfectDate",
                sord = input.orderType
            };
            var users = _usersService.GetUserNameDict("").Select(t => new
            {
                id = t.F_Id,
                name = t.F_RealName
            }).ToList();
            var list = (await _waterMDisinfectApp.GetList(pagination, input.startDate.ToDate(), input.endDate.ToDate()))
                .Select(t => new
                {
                    id = t.F_Id,
                    disinfectDate = t.F_DisinfectDate,
                    disinfectantName = t.F_DisinfectantName,
                    disinfectantVolume = t.F_DisinfectantVolume,
                    disinfectantUnit = t.F_DisinfectantUnit,
                    disinfectType = t.F_DisinfectType,
                    option1 = t.F_Option1,
                    recyclingStartTime = t.F_RecyclingStartTime,
                    recyclingEndTime = t.F_RecyclingEndTime,
                    recyclingMinutes = t.F_RecyclingMinutes,
                    soakStartTime = t.F_SoakStartTime,
                    soakEndTime = t.F_SoakEndTime,
                    soakMinutes = t.F_SoakMinutes,
                    rinseStartTime = t.F_RinseStartTime,
                    rinseEndTime = t.F_RinseEndTime,
                    rinseMinutes = t.F_RinseMinutes,
                    option2 = t.F_Option2,
                    option3 = t.F_Option3,
                    operatePerson = t.F_OperatePerson == null ? "" : users.First(u => u.id.Equals(t.F_OperatePerson)).name,
                    checkPerson = t.F_CheckPerson == null ? "" : users.First(u => u.id.Equals(t.F_CheckPerson)).name
                });
            var data = new
            {
                rows = list,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }

        public async Task<IActionResult> GetForm(BaseInput input)
        {
            //var userApp = new UserApp();
            var entity = await _waterMDisinfectApp.GetForm(input.KeyValue);
            var data = new
            {
                id = entity.F_Id,
                disinfectDate = entity.F_DisinfectDate,
                disinfectantName = entity.F_DisinfectantName,
                disinfectantVolume = entity.F_DisinfectantVolume,
                disinfectantUnit = entity.F_DisinfectantUnit,
                disinfectType = entity.F_DisinfectType,
                option1 = entity.F_Option1,
                recyclingStartTime = entity.F_RecyclingStartTime,
                recyclingEndTime = entity.F_RecyclingEndTime,
                recyclingMinutes = entity.F_RecyclingMinutes,
                soakStartTime = entity.F_SoakStartTime,
                soakEndTime = entity.F_SoakEndTime,
                soakMinutes = entity.F_SoakMinutes,
                rinseStartTime = entity.F_RinseStartTime,
                rinseEndTime = entity.F_RinseEndTime,
                rinseMinutes = entity.F_RinseMinutes,
                option2 = entity.F_Option2,
                option3 = entity.F_Option3,
                operatePerson = entity.F_OperatePerson == null ? "" : (await _usersService.FindUserAsync(entity.F_OperatePerson))?.F_RealName,
                checkPerson = entity.F_CheckPerson == null ? "" : (await _usersService.FindUserAsync(entity.F_CheckPerson))?.F_RealName
            };
            return Ok(data);
        }

        /// <summary>
        /// 新增修改水机消毒记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitData([FromBody]SubmitDataInput input)
        {
            var userId = _usersService.GetCurrentUserId();
            WaterMDisinfectEntity entity = null;
            if (string.IsNullOrEmpty(input.id))//新建
            {
                entity = new WaterMDisinfectEntity
                {
                    F_Id = Common.GuId(),
                    F_OperatePerson = userId,
                    F_CreatorTime = DateTime.Now,
                    F_CreatorUserId = userId,
                    F_EnabledMark = true
                };
            }
            else//修改
            {
                entity = new WaterMDisinfectEntity
                {
                    F_Id = input.id,
                    F_LastModifyTime = DateTime.Now,
                    F_LastModifyUserId = userId
                };
            }
            entity.F_DisinfectDate = input.disinfectDate;
            entity.F_DisinfectantName = input.disinfectantName;
            entity.F_DisinfectantVolume = input.disinfectantVolume;
            entity.F_DisinfectantUnit = input.disinfectantUnit;
            entity.F_DisinfectType = input.disinfectType;
            entity.F_Option1 = input.option1;
            entity.F_Option2 = input.option2;
            entity.F_Option3 = input.option3;
            entity.F_RecyclingStartTime = input.recyclingStartTime;
            entity.F_RecyclingEndTime = input.recyclingEndTime;
            entity.F_SoakStartTime = input.soakStartTime;
            entity.F_SoakEndTime = input.soakEndTime;
            entity.F_RinseStartTime = input.rinseStartTime;
            entity.F_RinseEndTime = input.rinseEndTime;
            if (input.recyclingMinutes == null)
            {
                if (input.recyclingStartTime != null && input.recyclingEndTime != null)
                    entity.F_RecyclingMinutes = (input.recyclingEndTime.ToDate() - input.recyclingStartTime.ToDate()).TotalMinutes.ToFloat(1);
            }
            else
            {
                entity.F_RecyclingMinutes = input.recyclingMinutes;
            }
            if (input.soakMinutes == null)
            {
                if (input.soakStartTime != null && input.soakEndTime != null)
                    entity.F_SoakMinutes = (input.soakEndTime.ToDate() - input.soakStartTime.ToDate()).TotalMinutes.ToFloat(1);
            }
            else
            {
                entity.F_SoakMinutes = input.soakMinutes;
            }
            if (input.rinseMinutes == null)
            {
                if (input.rinseEndTime != null && input.rinseStartTime != null)
                    entity.F_RinseMinutes = (input.rinseEndTime.ToDate() - input.rinseStartTime.ToDate()).TotalMinutes.ToFloat(1);
            }
            else
            {
                entity.F_RinseMinutes = input.rinseMinutes;
            }

            if (string.IsNullOrEmpty(input.id))
            {
                _waterMDisinfectApp.InsertForm(entity);
            }
            else
            {
                _waterMDisinfectApp.UpdateForm(entity);
            }
            var data = new
            {
                id = entity.F_Id
            };
            return Ok(data);
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Sign([FromBody]BaseInput input)
        {
            var entity = await _waterMDisinfectApp.GetForm(input.KeyValue);
            if (entity == null) return BadRequest("主键有误");
            if (!string.IsNullOrEmpty(entity.F_CheckPerson)) return BadRequest("记录已签名");
            var userId = _usersService.GetCurrentUserId();
            entity.F_LastModifyTime = DateTime.Now;
            entity.F_LastModifyUserId = userId;
            entity.F_CheckPerson = userId;
            await _waterMDisinfectApp.UpdateForm(entity);
            return Ok("操作成功");
        }

    }
}
