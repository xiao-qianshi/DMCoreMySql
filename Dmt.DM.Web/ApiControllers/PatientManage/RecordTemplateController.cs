using Dmt.Dm.Domain.Dto.RecordTemplate;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class RecordTemplateController : BaseApiController
    {
        private readonly IRecordTemplateApp _recordTemplateApp;
        private readonly IUsersService _usersService;

        public RecordTemplateController(IRecordTemplateApp recordTemplateApp, IUsersService usersService)
        {
            _recordTemplateApp = recordTemplateApp;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetPagedList(BaseInputPaged input)
        {
            var pagination = new Pagination
            {
                rows = input.rows,
                page = input.page,
                sidx = input.orderField ?? "F_CreatorTime",
                sord = input.orderType
            };
            var list = (await _recordTemplateApp.GetList(pagination, _usersService.GetCurrentUserId())).Select(t => new
            {
                id = t.F_Id,
                title = t.F_Title,
                content = t.F_Content,
                isPrivate = t.F_IsPrivate,
                creatorTime = t.F_CreatorTime
            });
            var data = new
            {
                rows = list,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddRecordTemplate([FromBody]AddRecordTemplateInput input)
        {
            var userId = _usersService.GetCurrentUserId();
            var entity = new RecordTemplateEntity
            {
                F_Id = Common.GuId(),
                F_EnabledMark = true,
                F_CreatorTime = DateTime.Now,
                F_CreatorUserId = userId,
                F_Title = input.title,
                F_Content = input.content,
                F_IsPrivate = input.isPrivate
            };
            await _recordTemplateApp.InsertForm(entity);
            var data = new
            {
                id = entity.F_Id
            };
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecordTemplate([FromBody]BaseInput input)
        {
            await _recordTemplateApp.DeleteForm(input.KeyValue);
            return Ok("删除成功");
        }
    }
}
