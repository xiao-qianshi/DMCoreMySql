using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.Dm.Domain.Dto.Puncture;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class PunctureController : BaseApiController
    {
        private readonly IPunctureApp _punctureApp;
        private readonly IUsersService _usersService;
        private readonly IPatientApp _patientApp;
        private readonly IPatVisitApp _patVisitApp;

        public PunctureController(IPunctureApp punctureApp, IUsersService usersService, IPatientApp patientApp, IPatVisitApp patVisitApp)
        {
            _punctureApp = punctureApp;
            _usersService = usersService;
            _patientApp = patientApp;
            _patVisitApp = patVisitApp;
        }

        /// <summary>
        /// 今日就诊-治疗单详情-穿刺历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetHistoryList(BaseInput input)
        {
            var output = new GetHistoryListOutput();

            var filePath = Path.Combine(AppConsts.AppRootPath, "upload", "Puncture", input.KeyValue);
            if (FileHelper.IsExistDirectory(filePath))
            {
                output.imagePath = FileHelper.GetFileNamesWithoutPath(filePath).OrderBy(t => t).LastOrDefault();
            }
            var list = await _punctureApp.GetList(input.KeyValue, 30);
            var table = new Hashtable();
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.F_Nurse))
                {
                    if (!table.ContainsKey(item.F_Nurse))
                    {
                        var find = await _usersService.FindUserAsync(item.F_Nurse);
                        table.Add(item.F_Nurse, find?.F_RealName ?? "");
                    }
                }
                var puncture = new PunctureItem
                {
                    id = item.F_Id,
                    point1 = item.F_Point1,
                    point2 = item.F_Point1,
                    memo = item.F_Memo,
                    operateTime = item.F_OperateTime,
                    isSucess = item.F_IsSuccess ?? true,
                    nurseId = item.F_Nurse,
                    nurseName = string.IsNullOrEmpty(item.F_Nurse) ? "" : table[item.F_Nurse].ToString()
                };
                output.punctureItems.Add(puncture);
            }
            return Ok(output);
        }

        /// <summary>
        /// 今日就诊-治疗单详情-新建、修改穿刺记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Route("SubmitData"), HttpPost]
        public async Task<IActionResult> SubmitData([FromBody]SubmitDataInput input)
        {
            if (input == null || string.IsNullOrEmpty(input.patientId))
            {
                return BadRequest("患者ID未传值！");
            }
            var userId = _usersService.GetCurrentUserId();
            if (string.IsNullOrEmpty(input.id))//新建
            {
                var entity = new PunctureEntity
                {
                    F_Id = Common.GuId(),
                    F_CreatorTime = System.DateTime.Now,
                    F_CreatorUserId = userId,
                    F_Nurse = userId,
                    F_OperateTime = input.operateTime ?? System.DateTime.Now,
                    F_IsSuccess = input.isSucess,
                    F_EnabledMark = true,
                    F_Point1 = input.point1,
                    F_Point2 = input.point2,
                    F_Pid = input.patientId
                };

                var patient = await _patientApp.GetForm(entity.F_Pid);
                if (patient != null)
                {
                    var visitDate = entity.F_OperateTime.ToDate().Date;
                    var patVisit = _patVisitApp.GetList().FirstOrDefault(t=>t.F_Pid == patient.F_Id && t.F_VisitDate == visitDate);//patient.F_Id, visitDate
                    if (patVisit != null)
                    {
                        //更新治疗单 穿刺者信息
                        patVisit.F_PuncturePerson = userId;
                        await _patVisitApp.UpdateForm(patVisit);
                    }
                    else
                    {
                        return BadRequest(patient.F_Name + "(" + visitDate.ToDateString() + ")无透析记录！");
                    }
                }
                else
                {
                    return BadRequest("患者ID有误！");
                }
                await _punctureApp.InsertForm(entity);
                return Ok(entity.F_Id);
            }
            else
            {
                var entity = new PunctureEntity
                {
                    F_LastModifyTime = DateTime.Now,
                    F_LastModifyUserId = userId
                };
                await _punctureApp.UpdateForm(entity);
                return Ok(entity.F_Id);
            }
        }

        /// <summary>
        /// 上传穿刺图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string id)
        {
            var filePath = "";
            if (id != null)
            {
                filePath = Path.Combine(AppConsts.AppRootPath, "upload", "Puncture", id);//Request.PhysicalApplicationPath + "pic\\upload\\Puncture\\" + id;// + "\\" + reportFile.FileName;
                if (!FileHelper.IsExistDirectory(filePath))
                {
                    FileHelper.CreateDirectory(filePath);
                }
                var serialNo = Common.CreateNo();
                filePath = Path.Combine(filePath, serialNo + Path.GetExtension(file.FileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Flush();
                }
                filePath = filePath.Replace(AppConsts.AppRootPath, "").Replace("\\", "/").TrimStart('/');
            }
            var data = new
            {
                pathUrl = filePath
            };
            return Ok(data);
        }
    }
}
