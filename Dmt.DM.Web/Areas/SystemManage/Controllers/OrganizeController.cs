using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dmt.DM.Mapper.Dto.SystemManage.Organize;

namespace Dmt.DM.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class OrganizeController : BaseController
    {
        private readonly IOrganizeApp _organizeApp = null;
        private readonly IMapper _mapper = null;

        public OrganizeController(IOrganizeApp organizeApp, IMapper mapper)
        {
            _organizeApp = organizeApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetTreeSelectJson()
        {
            var data = await _organizeApp.GetList();
            var treeList = data.Select(item => new TreeSelectModel {id = item.F_Id, text = item.F_FullName, parentId = item.F_ParentId, data = item}).ToList();
            return Content(treeList.TreeSelectJson());
        }

        public async Task<IActionResult> GetTreeJson()
        {
            var data = await _organizeApp.GetList();
            var treeList = (from item in data
            let hasChildren = data.Any(t => t.F_ParentId == item.F_Id)
            select new TreeViewModel
            {
                id = item.F_Id,
                text = item.F_FullName,
                value = item.F_EnCode,
                parentId = item.F_ParentId,
                isexpand = true,
                complete = true,
                hasChildren = hasChildren
            }).ToList();
            return Content(treeList.TreeViewJson());
        }

        public async Task<IActionResult> GetTreeGridJson(string keyword)
        {
            var data = await _organizeApp.GetList();
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
            var data = await _organizeApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<OrganizeDto> input)
        {
            OrganizeEntity entity = null;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<OrganizeEntity>(input.Entity);
            }
            else
            {
                entity = await _organizeApp.GetForm(input.KeyValue);
            }
            await _organizeApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _organizeApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }
    }
}
