using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    public class PatientController : BaseApiController
    {
        private readonly IPatientApp _patientApp;

        public PatientController(IPatientApp patientApp)
        {
            _patientApp = patientApp;
        }

        /// <summary>
        /// 获取二维码内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetBarcodeStr(BaseInput input)
        {
            var entity = await _patientApp.GetForm(input.KeyValue);
            if (entity == null)
            {
                return BadRequest("患者主键有误！");
            }
            var data = new
            {
                barcodeStr = input.KeyValue
            };
            return Ok(data);
        }

        /// <summary>
        /// 查询干体重
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetIdeaWeight(BaseInput input)
        {
            var data = new
            {
                ideaWeight = await _patientApp.GetIdeaWeight(input.KeyValue)
            };
            return Ok(data);
        }

        /// <summary>
        /// 查询患者总人数
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetTotalCount()
        {
            var data = new
            {
                count = await _patientApp.GetCount()
            };
            return Ok(data);
        }

        /// <summary>
        /// 分页查询患者信息(简要)
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字(透析号，姓名，病历号)</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPagedBrieflyList(BaseInputPaged input)
        {
            var pagination = new Pagination
            {
                rows = input.rows,
                page = input.page,
                sidx = input.orderField ?? "F_PY",
                sord = input.orderType
            };
            var keyword = input.keyValue;
            var data = new
            {
                rows = (await _patientApp.GetList(pagination, keyword)).Select(t => new
                {
                    t.F_Id,
                    t.F_Name,
                    t.F_DialysisNo,
                    t.F_RecordNo,
                    t.F_PatientNo,
                    t.F_Gender,
                    t.F_BirthDay,
                    F_AgeStr = t.F_BirthDay == null ? "" : ((DateTime.Now - t.F_BirthDay.ToDate()).TotalDays.ToInt() / 365).ToString() + "岁",
                    t.F_InsuranceNo,
                    t.F_IdNo,
                    t.F_MaritalStatus,
                    t.F_IdealWeight,
                    t.F_PhoneNo,
                    F_BeInfected = "+".Equals(t.F_Tp) || "+".Equals(t.F_Hiv) || "+".Equals(t.F_HBsAg) || "+".Equals(t.F_HBeAg) || "+".Equals(t.F_HBeAb),//阳性患者判断规则
                    t.F_CardNo,
                    t.F_PY,
                    F_HeadIcon = t.F_HeadIcon ?? ""
                }),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }

        /// <summary>
        /// 分页查询患者信息
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字(透析号，姓名，病历号)</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPagedGridJson(BaseInputPaged input)
        {
            var pagination = new Pagination
            {
                rows = input.rows,
                page = input.page,
                sidx = input.orderField ?? "F_PY",
                sord = input.orderType
            };
            var keyword = input.keyValue;
            var data = new
            {
                rows = (await _patientApp.GetList(pagination, keyword)).Select(t => new
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
                    F_HeadIcon = t.F_HeadIcon ?? ""
                }),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }

        /// <summary>
        /// 分页查询患者信息
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字(透析号，姓名，病历号)</param>
        /// <returns></returns>
        public async Task<IActionResult> GetGridJson(BasePagedInput input)
        {
            var pagination = input.pagination.ToJObject().ToObject<Pagination>();
            pagination.sidx = "F_PY asc";
            var keyword = input.keyword;
            var data = new
            {
                rows = (await _patientApp.GetList(pagination, keyword)).Select(t => new
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
                    F_HeadIcon = t.F_HeadIcon ?? ""
                }),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Ok(data);
        }

        /// <summary>
        /// 根据主键值查询
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetFormJson(BaseInput input)
        {
            var t = await _patientApp.GetForm(input.KeyValue);
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
                //t.F_Contacts,
                //t.F_Contacts2,
                t.F_Trasfer,
                //t.F_PhoneNo,
                //t.F_PhoneNo2,
                F_Contacts = t.F_Contacts + " " + t.F_PhoneNo + " " + t.F_Contacts2 + " " + t.F_PhoneNo2,
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
            return Ok(data);
        }

        /// <summary>
        /// 患者信息刷选
        /// </summary>
        /// <param name="keyValue">关键字(透析号，姓名，病历号)</param>
        /// <returns>主键、姓名、病历号、拼音码、干体重</returns>
        public async Task<IActionResult> GetSelectList(BaseInput input)
        {
            var data = from r in await _patientApp.GetListFilter(input.KeyValue)
                       select new
                       {
                           id = r.F_Id,
                           name = r.F_Name,
                           dialysisNo = r.F_DialysisNo,
                           py = r.F_PY
                       };
            data = data.OrderBy(r => r.name);
            return Ok(data);
        }

        /// <summary>
        /// 患者信息刷选
        /// </summary>
        /// <param name="keyValue">关键字(透析号，姓名，病历号)</param>
        /// <returns>主键、姓名、病历号、拼音码、干体重</returns>
        public async Task<IActionResult> GetSelectJson(BaseInput input)
        {
            var data = from r in await _patientApp.GetListFilter(input.KeyValue)
                       select new
                       {
                           id = r.F_Id,
                           name = r.F_Name,
                           recordNo = r.F_DialysisNo,
                           py = r.F_PY,
                           idealWeight = r.F_IdealWeight
                       };
            data = data.OrderBy(r => r.name);
            return Ok(data);
        }

        /// <summary>
        /// 根据透析号（整数）、卡号、主键ID查询患者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetFormJsonById(BaseInput input)
        {
            var t = await _patientApp.GetFormById(input.KeyValue);
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
            return Ok(data);
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UploadImage(IFormFile file, string id)
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
            if (!id.IsEmpty())
            {
                var entity = await _patientApp.GetForm(id);
                if (entity != null)
                {
                    entity.F_HeadIcon = headFileName.Replace(AppConsts.AppRootPath, "");
                    await _patientApp.UpdateForm(entity);
                }
            }
            return Ok("操作成功");

        }
    }
}
