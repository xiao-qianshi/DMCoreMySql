using AutoMapper;
using Dmt.DM.Application.LabLis;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.LabLis;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.LabLis.LabItem;

namespace Dmt.DM.Web.Areas.LabLis.Controllers
{
    [Area("LabLis")]
    public class LabItemController : BaseController
    {
        private readonly ILabItemApp _labItemApp;
        private readonly IMapper _mapper;
        
        public LabItemController(ILabItemApp labItemApp, IMapper mapper)
        {
            _labItemApp = labItemApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetSelectJson(string keyword)
        {
            var list = await _labItemApp.GetSelectOptions(keyword);
            var data = list.Select(t => t).OrderBy(t => t.Sorter)
                .Select(t => new
                {
                    id = t.Id,
                    code = t.Code,
                    container = t.Container,
                    cuvetteNo = t.CuvetteNo,
                    enName = t.EnName,
                    isExternal = t.IsExternal,
                    isPeriodic = t.IsPeriodic,
                    memo = t.Memo,
                    name = t.Name,
                    py = t.Py,
                    sampleType = t.SampleType,
                    shortName = t.ShortName,
                    sorter = t.Sorter,
                    thirdPartyCode = t.ThirdPartyCode,
                    timeInterval = t.TimeInterval,
                    type = t.Type
                });
            return Content(data.ToJson());
           
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = await _labItemApp.GetList(pagination, queryJson),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetSelectByTypeJson(string keyword)
        {
            var data = from r in await _labItemApp.GetListByType(keyword)
                       select new
                       {
                           r.F_Id,
                           r.F_Code,
                           r.F_Name,
                           r.F_EnName,
                           r.F_ShortName,
                           r.F_PY,
                           r.F_SampleType,
                           r.F_Container,
                           r.F_IsExternal
                       };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _labItemApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody] BaseSubmitInput<LabItemDto> input)
        {
            LabItemEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<LabItemEntity>(input.Entity);
            }
            else
            {
                entity = await _labItemApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _labItemApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _labItemApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var entity = await _labItemApp.GetForm(input.KeyValue);
           
            entity.F_EnabledMark = false;
            await _labItemApp.UpdateForm(entity);
            return Success("项目(" + entity.F_Name + ")禁用成功。");
        }

        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var entity = await _labItemApp.GetForm(input.KeyValue);

            entity.F_EnabledMark = true;
            await _labItemApp.UpdateForm(entity);
            return Success("项目(" + entity.F_Name + ")启用成功。");
        }
    }
}
