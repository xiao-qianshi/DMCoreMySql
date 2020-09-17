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
using Dmt.DM.Mapper.Dto.SystemManage.Items;

namespace Dmt.DM.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class ItemsTypeController : BaseController
    {
        private readonly IItemsApp _itemsApp = null;
        private readonly IMapper _mapper = null;
        public ItemsTypeController(IItemsApp itemsApp, IMapper mapper)
        {
            _itemsApp = itemsApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetTreeSelectJson()
        {
            var data = await _itemsApp.GetList();
            var treeList = data.Select(item => new TreeSelectModel {id = item.F_Id, text = item.F_FullName, parentId = item.F_ParentId}).ToList();
            return Content(treeList.TreeSelectJson());
        }

        public async Task<IActionResult> GetTreeJson()
        {
            var data = await _itemsApp.GetList();
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
  
        public async Task<IActionResult> GetTreeGridJson()
        {
            var data = await _itemsApp.GetList();
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
            var data = await _itemsApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<ItemsDto> input)
        {
            ItemsEntity entity = null;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<ItemsEntity>(input.Entity);
            }
            else
            {
                entity = await _itemsApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _itemsApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _itemsApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }
    }
}
