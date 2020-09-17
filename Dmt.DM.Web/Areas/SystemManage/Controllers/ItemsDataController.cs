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
using Dmt.DM.Mapper.Dto.SystemManage.ItemsDetail;

namespace Dmt.DM.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class ItemsDataController : BaseController
    {
        private readonly IItemsDetailApp _itemsDetailApp = null;
        private readonly IMapper _mapper = null;
        public ItemsDataController(IItemsDetailApp itemsDetailApp, IMapper mapper)
        {
            _itemsDetailApp = itemsDetailApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetGridJson(string itemId, string keyword)
        {
            var data = await _itemsDetailApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetSelectJson(string enCode)
        {
            var data = from r in await _itemsDetailApp.GetItemList(enCode)
                select new
                {
                    id = r.F_ItemCode,
                    text = r.F_ItemName
                };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetDefaultDialysisTypeJson()
        {
            var data = await _itemsDetailApp.GetDefaultDialysisType();
            return Content(data);
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _itemsDetailApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<ItemsDetailDto> input)
        {
            ItemsDetailEntity entity = null;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<ItemsDetailEntity>(input.Entity);
            }
            else
            {
                entity = await _itemsDetailApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _itemsDetailApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _itemsDetailApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }
    }
}
