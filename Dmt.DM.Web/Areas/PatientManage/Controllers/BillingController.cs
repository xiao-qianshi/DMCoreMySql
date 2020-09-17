using AutoMapper;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.Billing;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class BillingController : BaseController
    {
        private readonly IBillingApp _billingApp;
        private readonly IPatientApp _patientApp;
        private readonly IMapper _mapper;

        public BillingController(IBillingApp billingApp, IMapper mapper, IPatientApp patientApp)
        {
            _billingApp = billingApp;
            _mapper = mapper;
            _patientApp = patientApp;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword, string patientId, DateTime startDate, DateTime endDate, int classType, int statusType)
        {
            var data = new
            {
                rows = await _billingApp.GetList(pagination, keyword, patientId, startDate, endDate, classType, statusType),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public IActionResult GetFeeModel(string keyword)
        {
            var data = new string[] {"1", "1", "1", "1", "1", "1"}.Select(t => new
            {
                F_ItemClass = keyword,
                F_Charges = 0,
                F_Amount = 1,
                F_Costs = 0
            });
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _billingApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]AddFeeInput input)
        {
            await _billingApp.CreateBatch(input);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _billingApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisableForm([FromBody]BaseInput input)
        {
            await _billingApp.DisableForm(input.KeyValue);
            return Success("费用冲减成功。");
        }

        [HttpPost]
        public async Task<IActionResult> GetBillReport([FromBody]BillReportInput input)
        {
            var report = await _billingApp.GetReport(input.PatientId, input.Keyword, input.StartDate,
                input.EndDate, input.ClassType, input.StatusType);
           
            return Content(report);
        }
    }
}
