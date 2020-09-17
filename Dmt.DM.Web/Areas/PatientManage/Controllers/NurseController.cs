using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.DialysisObservation;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class NurseController:BaseController
    {
        private readonly IDialysisObservationApp _dialysisObservationApp;
        private readonly IMapper _mapper;

        public NurseController(IDialysisObservationApp dialysisObservationApp, IMapper mapper)
        {
            _dialysisObservationApp = dialysisObservationApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string pid, DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var data = new
            {
                rows = await _dialysisObservationApp.GetList(pagination, pid ?? "", startDate?.ToDate().Date ?? DateTime.Today,
                    endDate?.ToDate().Date ?? DateTime.Today),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _dialysisObservationApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<DialysisObservationDto> input)
        {
            DialysisObservationEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<DialysisObservationEntity>(input.Entity);
            }
            else
            {
                entity = await _dialysisObservationApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _dialysisObservationApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SaveData([FromBody]SaveDataInput input)
        {
            var entity = _mapper.Map<DialysisObservationEntity>(input);
            await _dialysisObservationApp.InsertForm(entity, input.VisitId);
            return Success("操作成功。");
        }

        public IActionResult GetListByVisitJson(string visitId)
        {
            var data = _dialysisObservationApp.GetListByVisit(visitId)
                .Select(t => t)
                .ToList()
                .OrderBy(t => t.F_NurseOperatorTime);
            return Content(data.ToJson());
        }

        public IActionResult GetListJson(string pid, DateTime? startDate, DateTime? endDate)
        {
            var date1 = startDate?.ToDate().Date ?? DateTime.Today;
            var date2 = endDate?.ToDate().Date ?? DateTime.Today;
            var data = _dialysisObservationApp.GetList(pid)
                .Where(t => t.F_VisitDate >= date1 && t.F_VisitDate <= date2)
                .Select(t => t)
                .ToList()
                .OrderBy(t => t.F_NurseOperatorTime);
            return Content(data.ToJson());
        }

        public IActionResult GetSingleByVisitIdJson(string visitId)
        {
            var data = _dialysisObservationApp.GetListByVisit(visitId)
                .OrderByDescending(t => t.F_NurseOperatorTime)
                .FirstOrDefault();
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _dialysisObservationApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> CopyForm([FromBody]BaseInput input)
        {
            var entity = await _dialysisObservationApp.GetForm(input.KeyValue);
            await _dialysisObservationApp.CopyForm(entity);
            return Success("复制成功。");
        }
    }
}
