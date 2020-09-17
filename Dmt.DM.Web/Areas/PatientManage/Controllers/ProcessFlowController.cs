using AutoMapper;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.ProcessFlow;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class ProcessFlowController : BaseController
    {
        private readonly IProcessFlowApp _processFlowApp;
        private readonly IMapper _mapper;
        public ProcessFlowController(IProcessFlowApp processFlowApp,IMapper mapper)
        {
            _processFlowApp = processFlowApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _processFlowApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }


        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _processFlowApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<ProcessFlowDto> input)
        {
            ProcessFlowEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<ProcessFlowEntity>(input.Entity);
            }
            else
            {
                entity = await _processFlowApp.GetForm(input.KeyValue);
            }
            await _processFlowApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            //app.DeleteForm(keyValue);
            var entity = await _processFlowApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            entity.F_DeleteMark = true;
            await _processFlowApp.UpdateForm(entity);
            return Success("删除成功。");
        }

    }
}
