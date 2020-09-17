using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Mapper.Dto.SystemManage.Module;

namespace Dmt.DM.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class ModuleController : BaseController
    {
        private IModuleApp _moduleApp = null;
        private readonly IMapper _mapper = null;

        public ModuleController(IModuleApp moduleApp, IMapper mapper)
        {
            _moduleApp = moduleApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetTreeSelectJson()
        {
            var data = await _moduleApp.GetList();
            var treeList = data.Select(item => new TreeSelectModel {id = item.F_Id, text = item.F_FullName, parentId = item.F_ParentId}).ToList();
            return Content(treeList.TreeSelectJson());
        }

        public async Task<IActionResult> GetTreeGridJson(string keyword)
        {
            var data = await _moduleApp.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.F_FullName.Contains(keyword));
            }
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
            var data = await _moduleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody] BaseInput input)
        {
            await _moduleApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody] BaseSubmitInput<ModuleDto> input)
        {
            ModuleEntity entity = null;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<ModuleEntity>(input.Entity);
            }
            else
            {
                entity = await _moduleApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _moduleApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }


    }
}
