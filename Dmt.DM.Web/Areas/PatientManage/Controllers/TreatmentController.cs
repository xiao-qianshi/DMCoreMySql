using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.Treatment;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class TreatmentController : BaseController
    {
        private readonly ITreatmentApp _treatmentApp;
        private readonly IMapper _mapper;

        public TreatmentController(ITreatmentApp treatmentApp, IMapper mapper)
        {
            _treatmentApp = treatmentApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetSelectJson(string keyword)
        {
            var data = from r in await _treatmentApp.GetList(keyword)
                       select new
                       {
                           id = r.F_Id,
                           text = r.F_TreatmentName
                       };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetListJson(string keyword)
        {
            var data = (from r in await _treatmentApp.GetList(keyword)
                        select new
                        {
                            r.F_TreatmentCode,
                            r.F_TreatmentName,
                            r.F_TreatmentSpec,
                            r.F_TreatmentSpell,
                            r.F_TreatmentUnit,
                            r.F_Charges,
                            r.F_Id
                        }).ToList();
            return Content(data.ToJson());
        }
  
        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _treatmentApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _treatmentApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<TreatmentDto> input)
        {
            TreatmentEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<TreatmentEntity>(input.Entity);
            }
            else
            {
                entity = await _treatmentApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _treatmentApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _treatmentApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }


        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var entity = await _treatmentApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            await _treatmentApp.UpdateForm(entity);
            return Success("治疗项目(" + entity.F_TreatmentName + ")禁用成功。");
        }
        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var entity = await _treatmentApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = true;
            await _treatmentApp.UpdateForm(entity);
            return Success("治疗项目(" + entity.F_TreatmentName + ")启用成功。");
        }

    }
}
