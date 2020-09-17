using Dmt.DM.Application.PatientManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.DictManage
{
    [Route("api/[controller]/[action]")]
    public class TreatmentController : BaseApiController
    {
        private readonly ITreatmentApp _treatmentApp;

        public TreatmentController(ITreatmentApp treatmentApp)
        {
            _treatmentApp = treatmentApp;
        }

        public async Task<IActionResult> GetList(BaseInput input)
        {
            var data = from r in await _treatmentApp.GetList(input == null ? "" : input.KeyValue)
                       select new
                       {
                           id = r.F_Id,
                           code = r.F_TreatmentCode,
                           name = r.F_TreatmentName,
                           spec = r.F_TreatmentSpec,
                           unit = r.F_TreatmentUnit,
                           spell = r.F_TreatmentSpell ?? "",
                           price = r.F_Charges
                       };
            return Ok(data);
        }

        /// <summary>
        /// 查找单一耗材
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetForm(BaseInput input)
        {
            var r = await _treatmentApp.GetForm(input == null ? "" : input.KeyValue);
            var data = new
            {
                id = r.F_Id,
                code = r.F_TreatmentCode,
                name = r.F_TreatmentName,
                spec = r.F_TreatmentSpec,
                unit = r.F_TreatmentUnit,
                spell = r.F_TreatmentSpell ?? "",
                price = r.F_Charges
            };
            return Ok(data);
        }

        /// <summary>
        /// 根据名称、拼音、代码筛选耗材
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSelect(BaseInput input)
        {
            var data = from r in await _treatmentApp.GetList(input == null ? "" : input.KeyValue)
                       select new
                       {
                           id = r.F_Id,
                           text = r.F_TreatmentName,
                           py = r.F_TreatmentSpell ?? ""
                       };
            return Ok(data);
        }
    }
}
