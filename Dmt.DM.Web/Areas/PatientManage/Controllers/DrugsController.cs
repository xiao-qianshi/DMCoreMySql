using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.Drugs;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class DrugsController: BaseController
    {
        private readonly IDrugsApp _drugsApp = null;
        private readonly IMapper _mapper = null;

        public DrugsController(IDrugsApp drugsApp, IMapper mapper)
        {
            _drugsApp = drugsApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetListJson(string keyword)
        {
            var data = await _drugsApp.GetList(keyword);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _drugsApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetSelectJson(string keyword)
        {
            var data = from r in await _drugsApp.GetSelectJson()
                       select new
                       {
                           id = r.F_Id,
                           text = r.F_DrugName
                       };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _drugsApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<DrugsDto> input)
        {
            DrugsEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<DrugsEntity>(input.Entity);
            }
            else
            {
                entity = await _drugsApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _drugsApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _drugsApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var entity = await _drugsApp.GetForm(input.KeyValue);
            if (entity != null)
            {
                entity.F_EnabledMark = false;
                await _drugsApp.UpdateForm(entity);
            }
            return Success("禁用成功。");
        }
        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var entity = await _drugsApp.GetForm(input.KeyValue);
            if (entity != null)
            {
                entity.F_EnabledMark = true;
                await _drugsApp.UpdateForm(entity);
            }
            return Success("启用成功。");
        }

    }
}
