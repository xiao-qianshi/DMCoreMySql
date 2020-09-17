using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Mapper.Dto.SystemManage.Area;

namespace Dmt.DM.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class AreaController : BaseController
    {
        private readonly IAreaApp _areaApp = null;
        private readonly IMapper _mapper = null;

        public AreaController(IAreaApp areaApp,IMapper mapper)
        {
            _areaApp = areaApp;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> GetTreeSelectJson()
        {
            var data = await _areaApp.GetList();
            var treeList = data.Select(item => new TreeSelectModel {id = item.F_Id, text = item.F_FullName, parentId = item.F_ParentId}).ToList();
            return Content(treeList.TreeSelectJson());
        }
        public async Task<IActionResult> GetTreeGridJson(string keyword)
        {
            var data = await _areaApp.GetList();
            var treeList = new List<TreeGridModel>();
            foreach (AreaEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) != 0;
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = true;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            }
            return Content(treeList.TreeGridJson());
        }
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _areaApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<AreaDto> input)
        {
            AreaEntity entity = null;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<AreaEntity>(input.Entity);
            }
            else
            {
                entity = await _areaApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _areaApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _areaApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }
    }
}