using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.ConclusionTemplate;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class ConclusionTemplateController : BaseController
    {
        private readonly IConclusionTemplateApp _conclusionTemplateApp = null;
        private readonly IMapper _mapper = null;

        public ConclusionTemplateController(IConclusionTemplateApp conclusionTemplateApp, IMapper mapper)
        {
            _conclusionTemplateApp = conclusionTemplateApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _conclusionTemplateApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public IActionResult GetSelectJson(string keyword)
        {
            var data = _conclusionTemplateApp.GetSelectList(keyword).Select(t => new
            {
                t.F_Title,
                t.F_Content,
                t.F_CreatorTime
            }).ToList();
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _conclusionTemplateApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<ConclusionTemplateDto> input)
        {
            ConclusionTemplateEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<ConclusionTemplateEntity>(input.Entity);
            }
            else
            {
                entity = await _conclusionTemplateApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));

            await _conclusionTemplateApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _conclusionTemplateApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Add()
        {
            return View();
        }
    }
}
