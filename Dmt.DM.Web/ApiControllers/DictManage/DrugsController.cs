using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.DictManage
{
    [Route("api/[controller]/[action]")]
    public class DrugsController : BaseApiController
    {
        private readonly IDrugsApp _drugsApp;

        public DrugsController(IDrugsApp drugsApp)
        {
            _drugsApp = drugsApp;
        }

        /// <summary>
        /// 查询抗凝剂信息
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetHeparinTypeInfo()
        {
            var data = (await _drugsApp.GetSelectJson()).Select(t => new {
                id = t.F_Id,
                code = t.F_DrugCode,
                efficacy = t.F_DrugEfficacy,
                miniAmount = t.F_DrugMiniAmount,
                miniPackage = t.F_DrugMiniPackage,
                miniSpec = t.F_DrugMiniSpec,
                name = t.F_DrugName,
                spec = t.F_DrugSpec,
                spell = t.F_DrugSpell,
                unit = t.F_DrugUnit,
            });
            return Ok(data);
        }

        /// <summary>
        /// 根据代码、名称、拼音查询药品简要信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetList([FromQuery]BaseInput input)
        {
            var data = (from r in await _drugsApp.GetList(input == null ? "" : input.KeyValue)
                        select new
                        {
                            id = r.F_Id,
                            code = r.F_DrugCode,
                            name = r.F_DrugName,
                            spec = r.F_DrugSpec,
                            unit = r.F_DrugUnit,
                            miniAmount = r.F_DrugMiniAmount,
                            miniPackage = r.F_DrugMiniPackage,
                            miniSpec = r.F_DrugMiniSpec,
                            administration = r.F_DrugAdministration,
                            spell = r.F_DrugSpell,
                            supplier = r.F_DrugSupplier,
                            price = r.F_Charges
                        }).ToList();
            return Ok(data);
        }

        /// <summary>
        /// 查找单一药品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetForm([FromQuery]BaseInput input)
        {
            var r = await _drugsApp.GetForm(input.KeyValue);
            var data = new
            {
                id = r.F_Id,
                code = r.F_DrugCode,
                name = r.F_DrugName,
                spec = r.F_DrugSpec,
                unit = r.F_DrugUnit,
                miniAmount = r.F_DrugMiniAmount,
                miniPackage = r.F_DrugMiniPackage,
                miniSpec = r.F_DrugMiniSpec,
                administration = r.F_DrugAdministration,
                spell = r.F_DrugSpell,
                supplier = r.F_DrugSupplier,
                price = r.F_Charges
            };
            return Ok(data);
        }

        /// <summary>
        /// 根据代码、名称、拼音查询抗凝剂（药品）简要信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSelect([FromQuery]BaseInput input)
        {
            var data = from r in await _drugsApp.GetList(input == null ? "" : input.KeyValue)
                       select new
                       {
                           id = r.F_Id,
                           text = r.F_DrugName,
                           py = r.F_DrugSpell ?? ""
                       };
            return Ok(data);
        }

        /// <summary>
        /// 药品信息分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetGridJson([FromQuery]Pagination pagination,string keyword)
        {
            var data = new
            {
                rows = await _drugsApp.GetList(pagination, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }
    }
}
