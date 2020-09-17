using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.Mapper.Dto.SystemManage.Role;

namespace Dmt.DM.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class RoleController : BaseController
    {
        private readonly IRoleApp _roleApp = null;
        private readonly IRoleAuthorizeApp _roleAuthorizeApp = null;
        private readonly IModuleApp _moduleApp = null;
        private readonly IModuleButtonApp _moduleButtonApp = null;
        private readonly IMapper _mapper = null;

        public RoleController(IRoleApp roleApp,
            IRoleAuthorizeApp roleAuthorizeApp, 
            IModuleApp moduleApp,
            IModuleButtonApp moduleButtonApp,
            IMapper mapper
            )
        {
            _roleApp = roleApp;
            _roleAuthorizeApp = roleAuthorizeApp;
            _moduleApp = moduleApp;
            _moduleButtonApp = moduleButtonApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetGridJson(string keyword)
        {
            var data = await _roleApp.GetList(keyword);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _roleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]RoleSubmitFormInput input)
        {
            RoleEntity entity = null;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<RoleEntity>(input.Entity);
            }
            else
            {
                entity = await _roleApp.GetForm(input.KeyValue);
                entity.CheckArgumentIsNull(nameof(entity));
            }
            
            await _roleApp.SubmitForm(entity, input.PermissionIds.Split(','), input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _roleApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }
        
    }
}
