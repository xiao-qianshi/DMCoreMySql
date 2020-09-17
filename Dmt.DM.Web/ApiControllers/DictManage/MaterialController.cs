using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.DictManage
{
    [Route("api/[controller]/[action]")]
    public class MaterialController : BaseApiController
    {
        private readonly IMaterialApp _materialApp;

        public MaterialController(IMaterialApp materialApp)
        {
            _materialApp = materialApp;
        }

        public async Task<IActionResult> GetList([FromQuery]BaseInput input)
        {
            var data = from r in await _materialApp.GetList(input == null ? "" : input.KeyValue)
                       select new
                       {
                           id = r.F_Id,
                           code = r.F_MaterialCode,
                           name = r.F_MaterialName,
                           spec = r.F_MaterialSpec,
                           unit = r.F_MaterialUnit,
                           spell = r.F_MaterialSpell ?? "",
                           supplier = r.F_MaterialSupplier,
                           price = r.F_Charges,
                           type = r.F_MaterialType
                       };
            return Ok(data);
        }
        /// <summary>
        /// 查找单一耗材
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetForm([FromQuery]BaseInput input)
        {
            var r = await _materialApp.GetForm(input == null ? "" : input.KeyValue);
            var data = new
            {
                id = r.F_Id,
                code = r.F_MaterialCode,
                name = r.F_MaterialName,
                spec = r.F_MaterialSpec,
                unit = r.F_MaterialUnit,
                spell = r.F_MaterialSpell ?? "",
                supplier = r.F_MaterialSupplier,
                price = r.F_Charges,
                type = r.F_MaterialType
            };
            return Ok(data);
        }
        /// <summary>
        /// 根据名称、拼音、代码筛选耗材
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSelect([FromQuery]BaseInput input)
        {
            var data = from r in await _materialApp.GetList(input == null ? "" : input.KeyValue)
                       select new
                       {
                           id = r.F_Id,
                           text = r.F_MaterialName,
                           py = r.F_MaterialSpell ?? ""
                       };
            return Ok(data);
        }
        /// <summary>
        /// 根据类别筛选耗材
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSelectByType([FromQuery]BaseInput input)
        {
            var data = from r in await _materialApp.GetListByType(input == null ? "" : input.KeyValue)
                       select new
                       {
                           id = r.F_Id,
                           text = r.F_MaterialName,
                           type = r.F_MaterialType,
                           py = r.F_MaterialSpell ?? ""
                       };
            return Ok(data);
        }
        /// <summary>
        /// 根据名称、拼音、代码筛选透析器（耗材）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetDialyzerListJson([FromQuery]BaseInput input)
        {
            var data = from r in await _materialApp.GetDialyzerList(input == null ? "" : input.KeyValue)
                       select new
                       {
                           id = r.F_Id,
                           text = r.F_MaterialName,
                           py = r.F_MaterialSpell ?? ""
                       };
            return Ok(data);
        }
        /// <summary>
        ///  耗材信息分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(Pagination pagination,string keyword)
        {
            var data = new
            {
                rows = _materialApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Ok(data);
        }
    }
}
