using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class RoleAuthorizeController : BaseController
    {
        private readonly IRoleAuthorizeApp _roleAuthorizeApp = null;
        private readonly IModuleApp _moduleApp = null;
        private readonly IModuleButtonApp _moduleButtonApp = null;

        public RoleAuthorizeController(IRoleAuthorizeApp roleAuthorizeApp, IModuleApp moduleApp, IModuleButtonApp moduleButtonApp)
        {
            _roleAuthorizeApp = roleAuthorizeApp;
            _moduleApp = moduleApp;
            _moduleButtonApp = moduleButtonApp;
        }

        public async Task<IActionResult> GetPermissionTree(string roleId)
        {
            var moduledata = await _moduleApp.GetList();
            var buttondata = await _moduleButtonApp.GetList();
            var authorizedata = new List<RoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = await _roleAuthorizeApp.GetList(roleId);
            }
            var treeList = (from item in moduledata
            let hasChildren = moduledata.Count(t => t.F_ParentId == item.F_Id) != 0
            select new TreeViewModel
            {
                id = item.F_Id,
                text = item.F_FullName,
                value = item.F_EnCode,
                parentId = item.F_ParentId,
                isexpand = true,
                complete = true,
                showcheck = true,
                checkstate = authorizedata.Count(t => t.F_ItemId == item.F_Id),
                hasChildren = true,
                img = item.F_Icon == "" ? "" : item.F_Icon
            }).ToList();

            treeList.AddRange(from item in buttondata
            let hasChildren = buttondata.Any(t => t.F_ParentId == item.F_Id)
            select new TreeViewModel
            {
                id = item.F_Id,
                text = item.F_FullName,
                value = item.F_EnCode,
                parentId = item.F_ParentId == "0" ? item.F_ModuleId : item.F_ParentId,
                isexpand = true,
                complete = true,
                showcheck = true,
                checkstate = authorizedata.Count(t => t.F_ItemId == item.F_Id),
                hasChildren = hasChildren,
                img = item.F_Icon == "" ? "" : item.F_Icon
            });
            return Content(treeList.TreeViewJson());
        }
    }
}
