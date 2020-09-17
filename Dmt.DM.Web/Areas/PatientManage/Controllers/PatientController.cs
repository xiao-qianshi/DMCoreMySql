using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.Patient;
using Dmt.DM.Mapper.Dto.PatientManage.TransferLog;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class PatientController: BaseController
    {
        private readonly IPatientApp _patientApp = null;
        private readonly IPatVisitApp _patVisitApp = null;
        private readonly IMapper _mapper = null;
        private readonly IMonthlySummaryApp _monthlySummaryApp = null;
        
        public PatientController(IPatientApp patientApp,IMapper mapper, IPatVisitApp patVisitApp, IMonthlySummaryApp monthlySummaryApp)
        {
            _patientApp = patientApp;
            _mapper = mapper;
            _patVisitApp = patVisitApp;
            _monthlySummaryApp = monthlySummaryApp;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _patientApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
 
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _patientApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 查找单一患者信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSinglePatient(string keyValue)
        {
            var t = await _patientApp.GetForm(keyValue);
            var data = new
            {
                t.F_Id,
                t.F_Name,
                t.F_DialysisNo,
                t.F_RecordNo,
                t.F_PatientNo,
                t.F_Gender,
                t.F_BirthDay,
                F_AgeStr = t.F_BirthDay == null ? "" : ((DateTime.Now - t.F_BirthDay.ToDate()).TotalDays.ToInt() / 365).ToString() + "岁",
                t.F_Charges,
                t.F_InsuranceNo,
                t.F_IdNo,
                t.F_MaritalStatus,
                t.F_IdealWeight,
                t.F_Height,
                t.F_DialysisStartTime,
                t.F_PrimaryDisease,
                t.F_Diagnosis,
                t.F_Address,
                t.F_InsuranceType,
                t.F_Contacts,
                t.F_Contacts2,
                t.F_Trasfer,
                t.F_PhoneNo,
                t.F_PhoneNo2,
                t.F_BloodAbo,
                t.F_BloodRh,
                t.F_Tp,
                t.F_Hiv,
                t.F_HBsAg,
                t.F_HBsAb,
                t.F_HBcAb,
                t.F_HBeAg,
                t.F_HBeAb,
                t.F_HCVAb,
                F_BeInfected = "+".Equals(t.F_Tp) || "+".Equals(t.F_Hiv) || "+".Equals(t.F_HBsAg) || "+".Equals(t.F_HBeAg) || "+".Equals(t.F_HBeAb),//阳性患者判断规则
                t.F_MedicalHistory,
                t.F_CardNo,
                t.F_PY,
                t.F_HeadIcon
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetSelectJson(string keyValue)
        {
            var list = await _patientApp.GetSelectOptions(keyValue);
            var data = list.Select(t => new
            {
                id = t.Id,
                name = t.Name,
                recordNo = t.RecordNo,
                py = t.Py,
                idealWeight = t.IdealWeight,
                beInfected = t.BeInfected
            }).OrderBy(t=>t.py);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 通过指定日期的就诊记录查询患者选择项
        /// </summary>
        /// <param name="visitDate"></param>
        /// <param name="groupName"></param>
        /// <param name="visitNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSelectByVisitRecordJson(DateTime? visitDate, string groupName, int? visitNo)
        {
            var visitDateTemp = visitDate?.ToDate().Date ?? DateTime.Today;
            var visitNoTemp = visitNo?.ToInt() ?? 0;
            var records = _patVisitApp.GetList()
                .Where(t=>t.F_VisitDate == visitDateTemp && t.F_VisitNo == visitNoTemp && t.F_GroupName == groupName)
                .Select(t => new
            {
                Id = t.F_Pid,
                VisitDate = t.F_VisitDate,
                VisitNo = t.F_VisitNo,
                GroupName = t.F_GroupName,
                BedNo = t.F_DialysisBedNo
            }).Distinct().ToList();
            var cacheData = await _patientApp.GetSelectOptions(null);
          
            var data = cacheData.Join(records, p => p.Id, r => r.Id, (p, v) => new
            {
                id = p.Id,
                name = p.Name,
                recordNo = p.RecordNo,
                py = p.Py,
                idealWeight = p.IdealWeight,
                beInfected = p.BeInfected,
                visitDate = v.VisitDate,
                visitNo = v.VisitNo,
                groupName = v.GroupName,
                bedNo = v.BedNo
            }).OrderBy(n => n.visitNo).ThenBy(n => n.groupName).ThenBy(n => n.bedNo);
            return Content(data.ToJson());
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJsonById(string dialysisNo)
        {
            var data = await _patientApp.GetFormById(dialysisNo);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetIdeaWeightJsonById(string keyValue)
        {
            var data = await _patientApp.GetIdeaWeight(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<PatientDto>input)
        {
            PatientEntity entity = null;
            if (input.KeyValue.IsEmpty())
            {
                try
                {
                    entity = _mapper.Map<PatientEntity>(input.Entity);
                }
                catch (Exception ex)
                {
                    var s = ex.Message;
                }
                
            }
            else
            {
                entity = await _patientApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _patientApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        #region 月小结
        [HttpGet]
        public async Task<IActionResult> GetMonthlySummaryJson(string patientId, string month)
        {
            var data = await _monthlySummaryApp.GetForm(patientId, month);
            return Content(data.ToJson());
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetTransferListJson(string patientId)
        {
            var data = (await _patientApp.GetTransferList(patientId)).Select(t => new
            {
                t.F_Id,
                t.F_ExitReason,
                t.F_ExitType,
                t.F_LocationOut,
                t.F_Memo,
                t.F_Pid,
                t.F_Status,
                t.F_TransferDate,
                t.F_TransferReason
            }).OrderBy(t => t.F_TransferDate);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTransfer([FromBody]BaseSubmitInput<TransferLogDto> input)
        {
            //患者ID为空
            input.KeyValue.CheckArgumentIsNull(nameof(input.KeyValue));
            var entity = _mapper.Map<TransferLogEntity>(input.Entity);
            entity.CheckArgumentIsNull(nameof(entity));
            await _patientApp.SubmitTransfer(entity, input.KeyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTransfer([FromBody]BaseInput input)
        {
            await _patientApp.DeleteTransfer(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> ModifyIdeaWeightForm([FromBody]ModifyIdeaWeightInput input)
        {
            var entity = await _patientApp.GetForm(input.Pid);
            if (entity == null) return Success("操作成功。");
            entity.F_IdealWeight = input.IdeaWeight;
            await _patientApp.UpdateForm(entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _patientApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var entity = await _patientApp.GetForm(input.KeyValue);
            if (entity == null) return Error("患者ID有误！");
            entity.Modify(input.KeyValue);
            entity.F_EnabledMark = false;
            await _patientApp.UpdateForm(entity);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var entity = await _patientApp.GetForm(input.KeyValue);
            if (entity == null) return Error("患者ID有误！");
            entity.Modify(input.KeyValue);
            entity.F_EnabledMark = true;
            await _patientApp.UpdateForm(entity);
            return Success("账户启用成功。");
        }

        [HttpPost]
        //[AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SaveAs(IFormFile file, string pid)
        {
            var targetPath = Path.Combine(AppConsts.AppRootPath, "upload", "patient");
            var targetHeadIconPath = Path.Combine(targetPath, "headIcon");
            FileHelper.CreateDirectory(targetPath);
            FileHelper.CreateDirectory(targetHeadIconPath);
            var serialNo = Common.CreateNo();
            var fileName = Path.Combine(targetPath, serialNo + Path.GetExtension(file.FileName));
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Flush();
            }

            var headFileName = Path.Combine(targetHeadIconPath, serialNo + Path.GetExtension(file.FileName));
            Ext.CutForSquare(fileName, headFileName, 400, 90);
            if (!pid.IsEmpty())
            {
                var entity = await _patientApp.GetForm(pid);
                if (entity != null)
                {
                    entity.F_HeadIcon = headFileName.Replace(AppConsts.AppRootPath, "");
                    await _patientApp.UpdateForm(entity);
                }
            }
            return Success("上传图片成功");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDetails()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult WeightDetails()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Summery()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Choose()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Barcode()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Transfer()
        {
            return View();
        }
    }
}
