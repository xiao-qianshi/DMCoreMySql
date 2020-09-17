using Dmt.DM.Application.SystemManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.SystemManage
{
    [Route("api/[controller]/[action]")]
    public class ItemsDataController : BaseApiController
    {
        private readonly IItemsDetailApp _itemsDetailApp;

        public ItemsDataController(IItemsDetailApp itemsDetailApp)
        {
            _itemsDetailApp = itemsDetailApp;
        }

        /// <summary>
        /// 查询基础字典参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSelectJson(BaseInput input)
        {
            var data = from r in await _itemsDetailApp.GetItemList(input == null ? "" : input.KeyValue)
                       select new
                       {
                           id = r.F_ItemCode,
                           text = r.F_ItemName
                       };
            return Ok(data);
        }
        /// <summary>
        /// 查询床位分组信息
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetBedGroupInfo()
        {
            var data = from r in await _itemsDetailApp.GetItemList("BedGroup")
                       select new
                       {
                           id = r.F_ItemCode,
                           text = r.F_ItemName
                       };
            return Ok(data);
        }

        /// <summary>
        /// 查询透析方式信息
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetDialysisTypeInfo()
        {
            var data = from r in await _itemsDetailApp.GetItemList("DialysisType")
                       select new
                       {
                           id = r.F_ItemCode,
                           text = r.F_ItemName,
                           isDefault = r.F_IsDefault ?? false
                       };
            return Ok(data);
        }

        /// <summary>
        /// 查询稀释模式信息
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDilutionTypeInfo()
        {
            var list = new List<string>
            {
                "无",
                "前稀释",
                "后稀释"
            };
            return Ok(list);
        }

        /// <summary>
        /// 查询血管通路类型
        /// </summary>
        /// <returns></returns>
        public IActionResult GetVascularAccessInfo()
        {
            var list = new List<string>
            {
                "自体内瘘",
                "移植内瘘",
                "深静脉置管",
                "带隧道带涤纶套导管",
                "无隧道无涤纶套导管",
                "其他"
            };
            return Ok(list);
        }
    }
}
