using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.QualityResult;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class QualityResultController:BaseController
    {
        private readonly IQualityResultApp _qualityResultApp;
        private readonly IPatientApp _patientApp;
        private readonly IQualityItemApp _qualityItemApp;
        private readonly IUsersService _usersService;

        public QualityResultController(
            IQualityResultApp qualityResultApp,
            IPatientApp patientApp,
            IUsersService usersService,
            IQualityItemApp qualityItemApp
            )
        {
            _patientApp = patientApp;
            _qualityResultApp = qualityResultApp;
            _usersService = usersService;
            _qualityItemApp = qualityItemApp;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string queryJson)
        {
            var patientId = string.Empty;
            var resultType = string.Empty;
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!queryJson.IsEmpty())
            {
                var queryParms = queryJson.ToJObject();
                patientId = queryParms["patientId"]?.Value<string>();
                resultType = queryParms["resultType"]?.Value<string>();
                startDate = queryParms["startDate"]?.Value<DateTime>();
                endDate = queryParms["endDate"]?.Value<DateTime>();
            }
            var data = new
            {
                rows = await _qualityResultApp.GetList(pagination, patientId, resultType,startDate, endDate),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetListJson(string patientId, DateTime startDate, DateTime endDate, string itemId)
        {
            var list = await _qualityResultApp.GetList(patientId, itemId, startDate, endDate.AddDays(1));
            var data = list
                .OrderByDescending(t => t.F_ReportTime)
                .ThenBy(t=>t.F_ItemType)
                .Select(t=>t);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _qualityResultApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]SubmitFormInput input)
        {
            foreach (var item in input.Items)
            {
                var find = await _qualityItemApp.GetForm(item.ItemId);
                if (find == null) continue;
                var entity = new QualityResultEntity
                {
                    F_Pid = input.PatientId,
                    F_ItemId = item.ItemId,
                    F_ItemType = find.F_ItemType,
                    F_ItemCode = find.F_ItemCode,
                    F_ItemName = find.F_ItemName,
                    F_ReportTime = item.ReportTime?.ToDate()??DateTime.Now,
                    F_Result = item.Result,
                    F_Flag = item.Flag,
                    F_Memo = item.Memo,
                    F_UpperValue = find.F_UpperValue,
                    F_LowerValue = find.F_LowerValue,
                    F_Unit = find.F_Unit,
                    F_LowerCriticalValue = find.F_LowerCriticalValue,
                    F_UpperCriticalValue = find.F_UpperCriticalValue,
                    F_ResultType = find.F_ResultType
                };
                await _qualityResultApp.SubmitForm(entity, null);
            }
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _qualityResultApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisableForm([FromBody]BaseInput input)
        {
            var entity = await _qualityResultApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            await _qualityResultApp.UpdateForm(entity);
            return Success("停用成功。");
        }

        [HttpPost]
        public async Task<IActionResult> EnabledForm([FromBody]BaseInput input)
        {
            var entity = await _qualityResultApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = true;
            await _qualityResultApp.UpdateForm(entity);
            return Success("启用成功。");
        }
    }
}
