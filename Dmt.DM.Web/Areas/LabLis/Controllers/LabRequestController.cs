using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.LabLis;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.LabLis;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.LabLis.LabRequest;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.LabLis.Controllers
{
    [Area("LabLis")]
    public class LabRequestController : BaseController
    {
        private readonly ILabRequestApp _labRequestApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public LabRequestController(ILabRequestApp labRequestApp, IMapper mapper, IUsersService usersService)
        {
            _labRequestApp = labRequestApp;
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetlistJson(DateTime requestDate, int? requestStatus)
        {
            var sheets = await _labRequestApp.GetList(requestDate, requestStatus);
            var sheetitems = await _labRequestApp.GetItemsList(requestDate, sheets.Select(t => t.F_RequestId).ToList());
            //删除空单
            //sheets.Where(t=>t.F_RequestId)
            var existsRequestIds = sheetitems.Select(t => t.F_RequestId).Distinct().ToList();
            foreach (var item in sheets.Where(t => !existsRequestIds.Contains(t.F_RequestId)))
            {
                item.F_EnabledMark = false;
                item.F_DeleteMark = true;
                await _labRequestApp.UpdateForm(item);
            }
            //var data = sheets
            //var data = _labRequestApp.GetList(requestDate, requestStatus);
            var master = sheets.Where(t => existsRequestIds.Contains(t.F_RequestId));
            var data = master.GroupJoin(sheetitems, m => m.F_RequestId, c => c.F_RequestId, (m, c) => new
            {
                id = m.F_Id,
                requestDate = m.F_RequestDate,
                requestId = m.F_RequestId,
                pid = m.F_Pid,
                name = m.F_Name,
                beInfected = m.F_BeInfected,
                dialysisNo = m.F_DialysisNo,
                recordNo = m.F_RecordNo,
                patientNo = m.F_PatientNo,
                gender = m.F_Gender,
                birthDay = m.F_BirthDay,
                insuranceNo = m.F_InsuranceNo,
                IdNo = m.F_IdNo,
                maritalStatus = m.F_MaritalStatus,
                idealWeight = m.F_IdealWeight,
                height = m.F_Height,
                primaryDisease = m.F_PrimaryDisease,
                diagnosis = m.F_Diagnosis,
                barcode = m.F_Barcode,
                sampleType = m.F_SampleType,
                container = m.F_Container,
                status = m.F_Status,
                doctorName = m.F_DoctorName,
                orderTime = m.F_OrderTime,
                nurseName = m.F_NurseName,
                samplingTime = m.F_SamplingTime,
                testUserName = m.F_TestUserName,
                testTime = m.F_TestTime,
                auditUserName = m.F_AuditUserName,
                auditTime = m.F_AuditTime,
                checkUserName = m.F_CheckUserName,
                checkTime = m.F_CheckTime,
                reportUserName = m.F_ReportUserName,
                reportTime = m.F_ReportTime,
                memo = m.F_Memo,
                rows = c.Select(r => new
                {
                    enName = r.F_EnName,
                    name = r.F_Name,
                    shortName = r.F_ShortName,
                    sorter = r.F_Sorter,
                    type = r.F_Type
                }).OrderBy(r => r.sorter)
            }).OrderBy(n => n.name).ThenBy(n => n.orderTime);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 批量创建申请单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> BatchCreateSheet([FromBody]BatchCreateSheetInput input)
        {
            await _labRequestApp.CreateRecords(input.Pids, input.Itemids);
            return Success("操作成功");
        }

        /// <summary>
        /// 标本合并
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SampleMerge([FromBody]BaseInput input)
        {
            var id = await _labRequestApp.SampleMerge(input.KeyValue);
            var entity = await _labRequestApp.GetForm(id);
            var details = await _labRequestApp.GetItem(entity.F_RequestDate, entity.F_RequestId);
            var data = new
            {
                id = entity.F_Id,
                requestDate = entity.F_RequestDate,
                requestId = entity.F_RequestId,
                pid = entity.F_Pid,
                name = entity.F_Name,
                beInfected = entity.F_BeInfected,
                dialysisNo = entity.F_DialysisNo,
                recordNo = entity.F_RecordNo,
                patientNo = entity.F_PatientNo,
                gender = entity.F_Gender,
                birthDay = entity.F_BirthDay,
                insuranceNo = entity.F_InsuranceNo,
                IdNo = entity.F_IdNo,
                maritalStatus = entity.F_MaritalStatus,
                idealWeight = entity.F_IdealWeight,
                height = entity.F_Height,
                primaryDisease = entity.F_PrimaryDisease,
                diagnosis = entity.F_Diagnosis,
                barcode = entity.F_Barcode,
                sampleType = entity.F_SampleType,
                container = entity.F_Container,
                status = entity.F_Status,
                doctorName = entity.F_DoctorName,
                orderTime = entity.F_OrderTime,
                nurseName = entity.F_NurseName,
                samplingTime = entity.F_SamplingTime,
                testUserName = entity.F_TestUserName,
                testTime = entity.F_TestTime,
                auditUserName = entity.F_AuditUserName,
                auditTime = entity.F_AuditTime,
                checkUserName = entity.F_CheckUserName,
                checkTime = entity.F_CheckTime,
                reportUserName = entity.F_ReportUserName,
                reportTime = entity.F_ReportTime,
                memo = entity.F_Memo,
                rows = details.Select(r => new
                {
                    enName = r.F_EnName,
                    name = r.F_Name,
                    shortName = r.F_ShortName,
                    sorter = r.F_Sorter,
                    type = r.F_Type
                }).OrderBy(t => t.sorter)
            };
            return Success("操作成功", data);
        }

        /// <summary>
        /// 采集确认
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SampleConfirm([FromBody]SampleConfirmInput input)
        {
            var list = await _labRequestApp.GetFormByBarcode(input.Barcode);
            if (list.Count == 0)
            {
                return Error("未查询到条码号为【" + input.Barcode + "】的检验申请记录！");
            }
            var status = list[0].F_Status;
            if (status >= 3)
            {
                return Error("条码号为【" + input.Barcode + "】的检验项目已采集！");
            }
            var user = await _usersService.GetCurrentUserAsync();//
            var _samplingTime = input.SamplingTime?.ToDate() ?? DateTime.Now;
            foreach (var item in list)
            {
                item.F_Status = 3;
                item.F_SamplingTime = _samplingTime;
                item.F_NurseId = user.F_Id;
                item.F_NurseName = user.F_RealName;
                await _labRequestApp.UpdateForm(item);
            }
            return Success("操作成功");
        }

        /// <summary>
        /// 下载采样单Excel
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        public void Download([FromBody] BaseInput input)
        {
            //OrganizeApp organize = new OrganizeApp();
            //var hospitalName = organize.GetHospitalName();
            //string filepath = Server.MapPath(_labRequestApp.CopyModelFile());// Server.MapPath(data.F_FilePath);
            ////填充数据
            //_labRequestApp.FillData(ids, filepath, hospitalName);
            //string filename = FileHelper.GetFileName(filepath);
            //if (FileDownHelper.FileExists(filepath))
            //{
            //    FileDownHelper.DownLoadold(filepath, filename);
            //}
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromBody]BaseInput input)
        {
            List<LabSheetEntity> sheets = new List<LabSheetEntity>();
            List<LabSheetItemsEntity> sheetItems = new List<LabSheetItemsEntity>();
            foreach (var item in input.KeyValue.Split(','))
            {
                var entity = await _labRequestApp.GetForm(item);
                if (entity == null || entity.F_EnabledMark != true || entity.F_DeleteMark == true || !entity.F_SamplingTime.HasValue)
                {
                    continue;
                }
                var detail = await _labRequestApp.GetItem(entity.F_RequestDate, entity.F_RequestId);
                if (detail.Count == 0) continue;
                sheets.Add(entity);
                sheetItems.AddRange(detail);
            }

            var data = sheets.GroupJoin(sheetItems, m => new { m.F_RequestDate, m.F_RequestId }, d => new { d.F_RequestDate, d.F_RequestId }, (m, d) => new
            {
                m.F_Id,
                m.F_RequestDate,
                m.F_RequestId,
                m.F_Barcode,
                m.F_SampleType,
                m.F_Container,
                m.F_OrderTime,
                m.F_DoctorName,
                m.F_SamplingTime,
                m.F_NurseName,
                m.F_Pid,
                m.F_Name,
                m.F_Gender,
                m.F_BirthDay,
                m.F_MaritalStatus,
                m.F_IdealWeight,
                m.F_DialysisNo,
                m.F_IdNo,
                m.F_RecordNo,
                m.F_PatientNo,
                m.F_InsuranceNo,
                m.F_Diagnosis,
                m.F_PrimaryDisease,
                Items = d.Select(n => new
                {
                    n.F_Id,
                    n.F_Type,
                    n.F_Code,
                    n.F_Name,
                    n.F_ShortName,
                    n.F_EnName,
                    n.F_ThirdPartyCode,
                    n.F_Sorter
                })
            });

            return Success("共上传" + sheets.Count + "项", data);
        }
        /// <summary>
        /// 标本合并
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SampleSpit([FromBody]SampleSpitInput input)
        {
            await _labRequestApp.SampleSpit(input.KeyValue, input.Items);
            return Success("操作成功");
        }

        [HttpPost]
        public async Task<IActionResult> BatchDelete([FromBody]BaseInput input)
        {
            await _labRequestApp.BatchDelete(input.KeyValue);
            return Success("删除成功");
        }

        [HttpPost]
        public async Task<IActionResult> BatchCreateBarcode([FromBody]BaseInput input)
        {
            await _labRequestApp.BatchCreateBarcode(input.KeyValue);
            return Success("生成成功");
        }

        [HttpPost]
        public async Task<IActionResult> SetBarcode([FromBody]SetBarcodeInput input)
        {
            var entity = await _labRequestApp.GetForm(input.KeyValue);
            if (entity != null)
            {
                entity.F_Barcode = input.Barcode;
                await _labRequestApp.UpdateForm(entity);
            }
            return Success("操作成功");
        }

        /// <summary>
        /// 查询需打印的条码信息
        /// </summary>
        /// <param name="ids">主键IDjson字符串</param>
        /// <returns></returns>
        public async Task<IActionResult> GetBarcodeInfo(string ids)
        {
            //var json = ids.ToJArrayObject();
            var masterList = new List<LabSheetEntity>();
            var detailList = new List<LabSheetItemsEntity>();
            foreach (var item in ids.Split(','))
            {
                var id = item;
                var entity = await _labRequestApp.GetForm(id);
                if (entity == null || entity.F_Barcode.IsEmpty()) continue;
                var detail = await _labRequestApp.GetItem(entity.F_RequestDate, entity.F_RequestId);
                if (detail == null) continue;
                masterList.Add(entity);
                detailList.AddRange(detail);
            }
            var data = masterList.GroupJoin(detailList, m => m.F_RequestId, d => d.F_RequestId, (m, d) => new
            {
                id = m.F_Id,
                barcode = m.F_Barcode,
                name = m.F_Name,
                beInfected = m.F_BeInfected,
                gender = m.F_Gender,
                ageStr = m.F_BirthDay.HasValue ? ((DateTime.Now - m.F_BirthDay.ToDate()).TotalDays.ToInt() / 365).ToString() + "岁" : "",
                sampleType = m.F_SampleType,
                container = m.F_Container,
                samplingTime = m.F_SamplingTime,
                rows = d.Select(r => new
                {
                    code = r.F_Code,
                    enName = r.F_EnName,
                    IsExternal = r.F_IsExternal,
                    name = r.F_Name,
                    shortName = r.F_ShortName,
                    sorter = r.F_Sorter,
                    thirdPartyCode = r.F_ThirdPartyCode,
                    type = r.F_Type
                }).OrderBy(r => r.sorter)
            });
            return Content(data.ToJson());
        }
        /// <summary>
        /// 查找单一样本详情
        /// </summary>
        /// <param name="keyValue">申请单ID</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSampleInfo(string keyValue)
        {
            var entity = await _labRequestApp.GetForm(keyValue);
            var details = await _labRequestApp.GetItem(entity.F_RequestDate, entity.F_RequestId);
            var data = new
            {
                id = entity.F_Id,
                requestDate = entity.F_RequestDate,
                requestId = entity.F_RequestId,
                pid = entity.F_Pid,
                name = entity.F_Name,
                beInfected = entity.F_BeInfected,
                dialysisNo = entity.F_DialysisNo,
                recordNo = entity.F_RecordNo,
                patientNo = entity.F_PatientNo,
                gender = entity.F_Gender,
                birthDay = entity.F_BirthDay,
                insuranceNo = entity.F_InsuranceNo,
                IdNo = entity.F_IdNo,
                maritalStatus = entity.F_MaritalStatus,
                idealWeight = entity.F_IdealWeight,
                height = entity.F_Height,
                primaryDisease = entity.F_PrimaryDisease,
                diagnosis = entity.F_Diagnosis,
                barcode = entity.F_Barcode,
                sampleType = entity.F_SampleType,
                container = entity.F_Container,
                status = entity.F_Status,
                doctorName = entity.F_DoctorName,
                orderTime = entity.F_OrderTime,
                nurseName = entity.F_NurseName,
                samplingTime = entity.F_SamplingTime,
                testUserName = entity.F_TestUserName,
                testTime = entity.F_TestTime,
                auditUserName = entity.F_AuditUserName,
                auditTime = entity.F_AuditTime,
                checkUserName = entity.F_CheckUserName,
                checkTime = entity.F_CheckTime,
                reportUserName = entity.F_ReportUserName,
                reportTime = entity.F_ReportTime,
                memo = entity.F_Memo,
                rows = details.Select(r => new
                {
                    id = r.F_Id,
                    enName = r.F_EnName,
                    name = r.F_Name,
                    shortName = r.F_ShortName,
                    sorter = r.F_Sorter,
                    type = r.F_Type
                }).OrderBy(t => t.sorter)
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _labRequestApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public IActionResult SubmitForm(LabSheetEntity entity, string keyValue)
        {
            _labRequestApp.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _labRequestApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 批量提交新申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> BatchSign([FromBody]BatchSignInput input)
        {
            await _labRequestApp.BatchSign(input.RequestDate);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SignForm([FromBody]BaseInput input)
        {
            await _labRequestApp.SignForm(input.KeyValue);
            return Success("操作成功。");
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult ChoosePatient()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult ChoosePatientByVisit()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult ChooseLabItem()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult Sample()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult SpitForm()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult PrintBarcode()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult MatchingBarcode()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult ConfirmSample()
        {
            return View();
        }
    }
}
