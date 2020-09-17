using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.DictManage
{
    [Route("api/[controller]/[action]")]
    public class DialysisMachineController : BaseApiController
    {
        private readonly IDialysisMachineApp _dialysisMachineApp;

        public DialysisMachineController(IDialysisMachineApp dialysisMachineApp)
        {
            _dialysisMachineApp = dialysisMachineApp;
        }

        public async Task<IActionResult> GetCardList()
        {
            var list = await _dialysisMachineApp.GetList();
            var data = list.GroupBy(t => t.F_GroupName).Select(t => new
            {
                groupName = t.Key,
                count = t.Count(),
                items = t.Select(r => new
                {
                    id = r.F_Id,
                    bedNo = r.F_DialylisBedNo,
                    machineName = r.F_MachineName,
                    machineNo = r.F_MachineNo,
                    defaultType = r.F_DefaultType,
                    creatorTime = r.F_CreatorTime,
                    showOrder = r.F_ShowOrder
                }).OrderBy(r => r.showOrder)
            }).OrderBy(t => t.groupName);
            return Ok(data);
        }

        /// <summary>
        /// 根据分区编码查询床位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSelectJson(BaseInput input)
        {
            var data = from r in await _dialysisMachineApp.GetItemList(input == null ? "" : input.KeyValue)
                       select new
                       {
                           r.F_Id,
                           r.F_GroupName,
                           r.F_DefaultType,
                           r.F_DialylisBedNo,
                           r.F_MachineName,
                           r.F_MachineNo,
                           r.F_ShowOrder
                       };

            return Ok(data);
        }

        /// <summary>
        /// 床位信息分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetGridJson(Pagination pagination,string keyword)
        {
            //var pagination = input.pagination.ToJObject().ToObject<Pagination>();
            //var keyword = input.keyword;
            var data = new
            {
                rows = await _dialysisMachineApp.GetList(pagination, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }
        /// <summary>
        /// 查找单一床位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetFormJson([FromQuery]BaseInput input)
        {
            var data = await _dialysisMachineApp.GetForm(input.KeyValue);
            //data = data ?? app.GetMachineByBedNo(input.keyValue);
            return Ok(data);
        }

    }
}
