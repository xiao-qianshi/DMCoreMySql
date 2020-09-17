using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.MachineManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.MachineManage.WaterMDisinfect;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.Areas.MachineManage.Controllers
{
    [Area("MachineManage")]
    public class WaterMDisinfectController : BaseController
    {
        private readonly IWaterMDisinfectApp _waterMDisinfectApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public WaterMDisinfectController(IWaterMDisinfectApp waterMDisinfectApp, IMapper mapper, IUsersService usersService)
        {
            _waterMDisinfectApp = waterMDisinfectApp;
            _mapper = mapper;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _waterMDisinfectApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            if (data.rows.Count > 0)
            {
                var users = _usersService.GetUserNameDict("").Select(t=>new{t.F_Id,t.F_RealName}).ToList();
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
            var data = await _waterMDisinfectApp.GetForm(keyValue);
            return Content(data.ToJson());
        }


        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _waterMDisinfectApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }


        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<WaterMDisinfectDto> input)
        {
            WaterMDisinfectEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<WaterMDisinfectEntity>(input.Entity);
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
                entity = await _waterMDisinfectApp.GetForm(input.KeyValue);
            }
            
            await _waterMDisinfectApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SignForm([FromBody]BaseInput input)
        {
            var entity = await _waterMDisinfectApp.GetForm(input.KeyValue);
            if (entity == null)
            {
                return Error("未找到记录");
            }
            if (entity.F_CheckPerson != null)
            {
                return Error("记录已核对签名！");

            }
            entity.F_CheckPerson = _usersService.GetCurrentUserId();
            await _waterMDisinfectApp.UpdateForm(entity);
            return Success("操作成功。");
        }


    }
}
