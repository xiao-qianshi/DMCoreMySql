using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.Infection;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class InfectionController : BaseController
    {
        private readonly IInfectionApp _infectionApp = null;
        private readonly IMapper _mapper = null;
        private readonly IUsersService _usersService = null;

        public InfectionController(IInfectionApp infectionApp, IMapper mapper, IUsersService usersService)
        {
            _infectionApp = infectionApp;
            _mapper = mapper;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _infectionApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            var users = _usersService.GetUserNameDict(null).Select(t => new
            {
                t.F_Id, t.F_RealName
            });
            foreach (var item in data.rows)
            {
                var user = users.FirstOrDefault(t => t.F_Id == item.F_RecordPerson);
                if (user != null)
                {
                    item.F_RecordPerson = user.F_RealName;
                }
            }
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _infectionApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<InfectionDto> input)
        {
            InfectionEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<InfectionEntity>(input.Entity);

                if (entity.F_RecordPerson == null)
                {
                    entity.F_RecordPerson = _usersService.GetCurrentUserId();
                }

                entity.F_EnabledMark = true;
            }
            else
            {
                entity = await _infectionApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _infectionApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _infectionApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }


        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var entity = await _infectionApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            await _infectionApp.UpdateForm(entity);
            return Success("禁用成功。");
        }
        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var entity = await _infectionApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = true;
            await _infectionApp.UpdateForm(entity);
            return Success("启用成功。");
        }

    }
}
