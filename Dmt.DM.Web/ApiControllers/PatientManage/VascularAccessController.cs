using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class VascularAccessController : BaseApiController
    {
        private readonly IVascularAccessApp _vascularAccessApp;
        private readonly IUsersService _usersService;

        public VascularAccessController(IVascularAccessApp vascularAccessApp, IUsersService usersService)
        {
            _vascularAccessApp = vascularAccessApp;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetPagedGridJson(BaseInputPaged input)
        {
            var pagination = new Pagination
            {
                rows = input.rows,
                page = input.page,
                sidx = input.orderField ?? "F_CreatorTime",
                sord = input.orderType ?? "desc"
            };
            var keyword = input.keyValue;
            var data = new
            {
                rows = (await _vascularAccessApp.GetList(pagination, keyword)).Select(t => new
                {
                    t.F_Id,
                    t.F_BloodSpeed,
                    t.F_BloodSpeed_Idea,
                    t.F_BodyPart,
                    t.F_Complication,
                    t.F_CreatorTime,
                    t.F_CreatorUserId,
                    t.F_DisabledRemark,
                    t.F_DiscardTime,
                    t.F_EnabledMark,
                    t.F_FirstUseTime,
                    t.F_Memo,
                    t.F_OperateTime,
                    t.F_Pid,
                    t.F_Type,
                    t.F_VascularAccess,
                    t.F_PicPath
                }),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }

        public async Task<IActionResult> GetFormJson(BaseInput input)
        {
            var t = await _vascularAccessApp.GetForm(input.KeyValue);
            var data = new
            {
                t.F_Id,
                t.F_BloodSpeed,
                t.F_BloodSpeed_Idea,
                t.F_BodyPart,
                t.F_Complication,
                t.F_CreatorTime,
                t.F_CreatorUserId,
                t.F_DisabledRemark,
                t.F_DiscardTime,
                t.F_EnabledMark,
                t.F_FirstUseTime,
                t.F_Memo,
                t.F_OperateTime,
                t.F_Pid,
                t.F_Type,
                t.F_VascularAccess,
                t.F_PicPath
            };
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitData([FromBody]VascularAccessEntity entity)
        {
            if (entity.F_Id == null)
            {
                entity.F_Id = Common.GuId();
                entity.F_CreatorTime = DateTime.Now;
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                await _vascularAccessApp.InsertForm(entity);
            }
            else
            {
                entity.F_LastModifyTime = DateTime.Now;
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                await _vascularAccessApp.UpdateForm(entity);
            }
            return Ok("操作成功。");
        }

        /// <summary>
        /// 上传图像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string id)
        {
            var filePath = "";
            if (id != null)
            {
                var entity = await _vascularAccessApp.GetForm(id);
                if (entity != null)
                {
                    filePath = Path.Combine(AppConsts.AppRootPath, "upload", "VascularAccess", id);
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
                    entity.F_PicPath = filePath;
                    await _vascularAccessApp.UpdateForm(entity);
                }
            }
            var data = new
            {
                pathUrl = filePath
            };
            return Ok(data);
        }
    }
}
