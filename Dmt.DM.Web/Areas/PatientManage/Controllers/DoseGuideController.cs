using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.DoseGuide;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class DoseGuideController : BaseController
    {
        private readonly IDoseGuideApp _doseGuideApp = null;
        private readonly IMapper _mapper = null;

        public DoseGuideController(IDoseGuideApp doseGuideApp, IMapper mapper)
        {
            _doseGuideApp = doseGuideApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _doseGuideApp.GetList(pagination, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _doseGuideApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody] BaseSubmitInput<DoseGuideDto>input)
        {
            DoseGuideEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<DoseGuideEntity>(input.Entity);
            }
            else
            {
                entity = await _doseGuideApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _doseGuideApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _doseGuideApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var entity = await _doseGuideApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            await _doseGuideApp.UpdateForm(entity);
            return Success("停用成功。");
        }

        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var entity = await _doseGuideApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = true;
            await _doseGuideApp.UpdateForm(entity);
            return Success("启用成功。");
        }

    }
}
