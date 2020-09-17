using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.DialysisMachine;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class DialysisMachineController : BaseController
    {
        private readonly IDialysisMachineApp _dialysisMachineApp = null;
        private readonly IMapper _mapper = null;

        public DialysisMachineController(IDialysisMachineApp dialysisMachineApp, IMapper mapper)
        {
            _dialysisMachineApp = dialysisMachineApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetSelectJson(string enCode)
        {
            var data = from r in await _dialysisMachineApp.GetItemList(enCode)
                       select new
                       {
                           id = r.F_DialylisBedNo,
                           text = r.F_DialylisBedNo + "(" + r.F_DefaultType + ")"
                       };

            return Content(data.ToJson());
        }

        public IActionResult GetGroupListJson(string enCode)
        {
            var data = _dialysisMachineApp.GetQueryable().Select(t => new
            {
                t.F_Id,
                t.F_DefaultType,
                t.F_DialylisBedNo,
                t.F_GroupName,
                t.F_MachineName,
                t.F_MachineNo,
                t.F_ShowOrder
            }).GroupBy(t => t.F_GroupName);
            return Content(data.ToJson());
        }


        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _dialysisMachineApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _dialysisMachineApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<DialysisMachineDto> input)
        {
            DialysisMachineEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<DialysisMachineEntity>(input.Entity);
            }
            else
            {
                entity = await _dialysisMachineApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _dialysisMachineApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _dialysisMachineApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }


        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var entity = await _dialysisMachineApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            await _dialysisMachineApp.UpdateForm(entity);
            return Success("床位禁用成功。");
        }

        [HttpPost]
        public IActionResult ModifyDialysisType(string bedId, string dialysisType)
        {
            //var entity = _dialysisMachineApp.GetForm(bedId);
            //if (entity == null) return Error("床位ID有误!");
            ////修改排班数据
            //DialysisScheduleApp dialysisScheduleApp = new DialysisScheduleApp();
            //dialysisScheduleApp.ChangeDialysisType(bedId, dialysisType);
            ////修改治疗单数据
            //PatVisitApp patVisitApp = new PatVisitApp();
            //patVisitApp.ChangeDialysisType(entity.F_GroupName, entity.F_DialylisBedNo, dialysisType);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var entity = await _dialysisMachineApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = true;
            await _dialysisMachineApp.UpdateForm(entity);
            return Success("床位启用成功。");
        }
    }
}
