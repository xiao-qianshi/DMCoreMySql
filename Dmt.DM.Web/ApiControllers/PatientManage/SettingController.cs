using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class SettingController : BaseApiController
    {
        private readonly ISettingApp _settingApp;
        private readonly IUsersService _usersService;

        public SettingController(ISettingApp settingApp, IUsersService usersService)
        {
            _settingApp = settingApp;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetPagedGridJson(BaseInputPaged input)
        {
            var pagination = new Pagination
            {
                rows = input.rows,
                page = input.page,
                sidx = input.orderField ?? "F_CreatorTime",
                sord = input.orderType ?? "desc"
            };
            var keyword = input.keyValue;
            var data = new
            {
                rows = (await _settingApp.GetList(pagination, keyword)).Select(t => new
                {
                    t.F_AccessName,
                    t.F_BloodSpeed,
                    t.F_Ca,
                    t.F_CreatorTime,
                    t.F_DialysateTemperature,
                    t.F_DialysisType,
                    t.F_DialyzerType1,
                    t.F_DialyzerType2,
                    t.F_DilutionType,
                    t.F_EstimateHours,
                    t.F_ExchangeAmount,
                    t.F_ExchangeSpeed,
                    t.F_Hco3,
                    t.F_HeparinAddAmount,
                    t.F_HeparinAddSpeedUnit,
                    t.F_HeparinAmount,
                    t.F_HeparinType,
                    t.F_HeparinUnit,
                    t.F_Id,
                    t.F_IsDefault,
                    t.F_K,
                    t.F_LowCa,
                    t.F_Na,
                    t.F_Pid,
                    t.F_VascularAccess
                }),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }
        
        public async Task<IActionResult> GetFormJson(BaseInput input)
        {
            var t = await _settingApp.GetForm(input.KeyValue);
            var data = new
            {
                t.F_AccessName,
                t.F_BloodSpeed,
                t.F_Ca,
                t.F_CreatorTime,
                t.F_DialysateTemperature,
                t.F_DialysisType,
                t.F_DialyzerType1,
                t.F_DialyzerType2,
                t.F_DilutionType,
                t.F_EstimateHours,
                t.F_ExchangeAmount,
                t.F_ExchangeSpeed,
                t.F_Hco3,
                t.F_HeparinAddAmount,
                t.F_HeparinAddSpeedUnit,
                t.F_HeparinAmount,
                t.F_HeparinType,
                t.F_HeparinUnit,
                t.F_Id,
                t.F_IsDefault,
                t.F_K,
                t.F_LowCa,
                t.F_Na,
                t.F_Pid,
                t.F_VascularAccess
            };
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitData([FromBody]SettingEntity entity)
        {
            if (entity.F_Id == null)
            {
                entity.F_Id = Common.GuId();
                entity.F_CreatorTime = DateTime.Now;
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                await _settingApp.InsertForm(entity);
            }
            else
            {
                entity.F_LastModifyTime = DateTime.Now;
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                await _settingApp.UpdateForm(entity);
            }
            return Ok("操作成功。");
        }
    }
}
