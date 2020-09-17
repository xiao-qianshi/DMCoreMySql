using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class MedicalRecordController : BaseApiController
    {
        private readonly IMedicalRecordApp _medicalRecordApp;
        private readonly IPatientApp _patientApp;
        private readonly IUsersService _usersService;

        public MedicalRecordController(IMedicalRecordApp medicalRecordApp, IPatientApp patientApp, IUsersService usersService)
        {
            _medicalRecordApp = medicalRecordApp;
            _patientApp = patientApp;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetPagedList(BaseInputPaged input)
        {
            if (string.IsNullOrEmpty(input.orderType)) input.orderType = "desc";
            PatientEntity patient = null;
            if (string.IsNullOrEmpty(input.keyValue))
            {
                patient = await _patientApp.GetRandom();
            }
            else
            {
                patient = await _patientApp.GetForm(input.keyValue);
            }
            if (patient == null) return BadRequest("患者ID有误");

            var pagination = new Pagination
            {
                rows = input.rows,
                page = input.page,
                sidx = input.orderField ?? "F_ContentTime",
                sord = input.orderType
            };
            Hashtable table = new Hashtable();
            var list = await _medicalRecordApp.GetListByPid(pagination, patient.F_Id);
          
            foreach (var item in list)
            {
                if (!table.ContainsKey(item.F_CreatorUserId))
                {
                    var user = await _usersService.FindUserAsync(item.F_CreatorUserId);
                    table.Add(item.F_CreatorUserId, user?.F_RealName ?? "");
                }
                item.F_CreatorUserId = table[item.F_CreatorUserId] + "";
            }
            var data = new
            {
                patient = new
                {
                    name = patient.F_Name,
                    gender = patient.F_Gender,
                    ageStr = patient.F_BirthDay == null ? "" : ((DateTime.Now - patient.F_BirthDay.ToDate()).TotalDays.ToInt() / 365).ToString() + "岁",
                    maritalStatus = patient.F_MaritalStatus,
                    beInfected = "+".Equals(patient.F_Tp) || "+".Equals(patient.F_Hiv) || "+".Equals(patient.F_HBsAg) || "+".Equals(patient.F_HBeAg) || "+".Equals(patient.F_HBeAb),//阳性患者判断规则
                    dialysisNo = patient.F_DialysisNo,
                    primaryDisease = patient.F_PrimaryDisease,
                    diagnosis = patient.F_Diagnosis,
                    headIcon = patient.F_HeadIcon
                },
                rows = list.OrderByDescending(t => t.F_ContentTime).Select(t => new
                {
                    id = t.F_Id,
                    title = t.F_Title,
                    content = t.F_Content,
                    contentTime = t.F_ContentTime,
                    isAudit = t.F_AuditFlag,
                    creatorUser = t.F_CreatorUserId
                }),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }
    }
}
