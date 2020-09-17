using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.Evaluation;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class EvaluationController : BaseController
    {
        private readonly IEvaluationApp _evaluationApp = null;
        private readonly IMapper _mapper = null;
        private readonly IPatVisitApp _patVisitApp = null;

        public EvaluationController(IEvaluationApp evaluationApp, IMapper mapper, IPatVisitApp patVisitApp)
        {
            _evaluationApp = evaluationApp;
            _mapper = mapper;
            _patVisitApp = patVisitApp;
        }

        public async Task<IActionResult> GetFormJson(string pid, DateTime visitDate)
        {
            var data = await _evaluationApp.GetForm(pid, visitDate);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _evaluationApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormByVisitIdJson(string keyValue)
        {
            var visitRecord = await _patVisitApp.GetForm(keyValue);
            var data = await _evaluationApp.GetForm(visitRecord.F_Pid, visitRecord.F_VisitDate.ToDate());
            return Content(data.ToJson());
        }


        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<EvaluationDto> input)
        {
            EvaluationEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<EvaluationEntity>(input.Entity);
            }
            else
            {
                entity = await _evaluationApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _evaluationApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _evaluationApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> GetEvaluationImage(string keyValue)
        {
            return Content(await _evaluationApp.GetEvaluationReport(keyValue));
        }
    }
}
