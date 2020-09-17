using AutoMapper;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.BillingModel;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class BillingModelController : BaseController
    {
        private readonly IBillingModelApp _billingModelApp;
        private readonly IMapper _mapper;

        public BillingModelController(IBillingModelApp billingModelApp, IMapper mapper)
        {
            _billingModelApp = billingModelApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string dialysisType, string vascularAccess)
        {
            var data = new
            {
                rows = await _billingModelApp.GetList(pagination, dialysisType, vascularAccess),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _billingModelApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<BillingModelDto> input)
        {
            BillingModelEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<BillingModelEntity>(input.Entity);
            }
            else
            {
                entity = await _billingModelApp.GetForm(input.KeyValue);
            }
            await _billingModelApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _billingModelApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var entity = await _billingModelApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            await _billingModelApp.UpdateForm(entity);
            return Success("禁用成功。");
        }
        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var entity = await _billingModelApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = true;
            await _billingModelApp.UpdateForm(entity);
            return Success("启用成功。");
        }
    }
}
