using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.MedicalRecord;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class MedicalRecordController : BaseController
    {
        private readonly IMedicalRecordApp _medicalRecordApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public MedicalRecordController(IMedicalRecordApp medicalRecordApp, IMapper mapper, IUsersService usersService)
        {
            _medicalRecordApp = medicalRecordApp;
            _mapper = mapper;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _medicalRecordApp.GetList(pagination, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _medicalRecordApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<MedicalRecordDto> input)
        {
            MedicalRecordEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<MedicalRecordEntity>(input.Entity);
                if (entity.F_AuditFlag == null)
                {
                    entity.F_AuditFlag = false;
                }
            }
            else
            {
                entity = await _medicalRecordApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _medicalRecordApp.SubmitForm(entity, input.KeyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _medicalRecordApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }


        public IActionResult GetListJson(string patientId, DateTime? startDate, DateTime? endDate)
        {
            var list = _medicalRecordApp.GetList(patientId, startDate, endDate).Select(t => new
            {
                t.F_Id,
                t.F_ContentTime,
                t.F_Title,
                t.F_AuditFlag,
                t.F_CreatorUserId,
                t.F_AuditTime
            }).Take(50).ToList();
            var userIds = list.Select(t => t.F_CreatorUserId).Distinct();
            var users = _usersService.GetUserNameDict(null).Where(t => userIds.Contains(t.F_Id))
                .Select(t => new {t.F_Id, t.F_RealName});
            var data = list.Join(users, l => l.F_CreatorUserId, u => u.F_Id, (l, u) => new
            {
                id = l.F_Id,
                contentTime = l.F_ContentTime,
                title = l.F_Title,
                auditFlag = l.F_AuditFlag,
                userId = l.F_CreatorUserId,
                auditTime = l.F_AuditTime,
                userName = u.F_RealName
            });
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> CheckMedicalRecord([FromBody]BaseInput input)
        {
            var entity = await _medicalRecordApp.GetForm(input.KeyValue);
            
            entity.F_AuditFlag = true;
            entity.F_AuditTime = DateTime.Now;
            entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
            await _medicalRecordApp.UpdateForm(entity);
            return Success("病历(" + entity.F_Title + ")提交成功。");
        }

        public async Task<IActionResult> GetPrintContent(string keyValue)
        {
            var data = new
            {
                content = await _medicalRecordApp.GetReport(keyValue)
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Print()
        {
            return View();
        }

    }
}
