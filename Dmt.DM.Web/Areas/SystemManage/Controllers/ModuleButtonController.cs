using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.SystemManage.ModuleButton;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class ModuleButtonController : BaseController
    {
        private readonly IModuleApp _moduleApp = null;
        private readonly IModuleButtonApp _moduleButtonApp = null;
        private readonly IMapper _mapper = null;

        public ModuleButtonController(IModuleApp moduleApp, IModuleButtonApp moduleButtonApp, IMapper mapper)
        {
            _moduleApp = moduleApp;
            _moduleButtonApp = moduleButtonApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetTreeSelectJson(string moduleId)
        {
            var data = await _moduleButtonApp.GetList(moduleId);
            var treeList = data.Select(item => new TreeSelectModel {id = item.F_Id, text = item.F_FullName, parentId = item.F_ParentId}).ToList();
            return Content(treeList.TreeSelectJson());
        }

        public async Task<IActionResult> GetTreeGridJson(string moduleId)
        {
            var data = await _moduleButtonApp.GetList(moduleId);
            var treeList = (from item in data
            let hasChildren = data.Any(t => t.F_ParentId == item.F_Id)
            select new TreeGridModel
            {
                id = item.F_Id,
                isLeaf = hasChildren,
                parentId = item.F_ParentId,
                expanded = hasChildren,
                entityJson = item.ToJson()
            }).ToList();
            return Content(treeList.TreeGridJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _moduleButtonApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<ModuleButtonDto> input)
        {
            ModuleButtonEntity entity = null;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<ModuleButtonEntity>(input.Entity);
            }
            else
            {
                entity = await _moduleButtonApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _moduleButtonApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _moduleButtonApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        public IActionResult CloneButton()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCloneButtonTreeJson()
        {
            var moduledata = await _moduleApp.GetList();
            var buttondata = await _moduleButtonApp.GetList();
            var treeList = (from item in moduledata
            let hasChildren = moduledata.Any(t => t.F_ParentId == item.F_Id)
            select new TreeViewModel
            {
                id = item.F_Id,
                text = item.F_FullName,
                value = item.F_EnCode,
                parentId = item.F_ParentId,
                isexpand = true,
                complete = true,
                hasChildren = true
            }).ToList();
            foreach (var item in buttondata)
            {
                var hasChildren = buttondata.Any(t => t.F_ParentId == item.F_Id);
                var tree = new TreeViewModel
                {
                    id = item.F_Id,
                    text = item.F_FullName,
                    value = item.F_EnCode,
                    parentId = item.F_ParentId == "0" ? item.F_ModuleId : item.F_ParentId,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    hasChildren = hasChildren
                };

                if (item.F_Icon != "")
                {
                    tree.img = item.F_Icon;
                }
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        [HttpPost]
        public async Task<IActionResult> SubmitCloneButton([FromBody]SubmitCloneButtonInput input)
        {
            await _moduleButtonApp.SubmitCloneButton(input.ModuleId, input.Ids);
            return Success("克隆成功。");
        }
    }
}
