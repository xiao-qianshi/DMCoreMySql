using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Mapper.Dto.SystemManage.Role;

namespace Dmt.DM.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class DutyController : BaseController
    {
        private readonly IDutyApp _dutyApp = null;
        private readonly IMapper _mapper = null;
        public DutyController(IDutyApp dutyApp, IMapper mapper)
        {
            _dutyApp = dutyApp;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGridJson(string keyword)
        {
            var data = await _dutyApp.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _dutyApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody] BaseSubmitInput<RoleDto> input)
        {
            RoleEntity entity = null;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<RoleEntity>(input.Entity);
            }
            else
            {
                entity = await _dutyApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _dutyApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _dutyApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }
    }
}
