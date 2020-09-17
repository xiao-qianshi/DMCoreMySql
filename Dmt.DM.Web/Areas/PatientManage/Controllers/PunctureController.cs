using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.Puncture;
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
    public class PunctureController : BaseController
    {
        private readonly IPunctureApp _punctureApp;
        private readonly IPatVisitApp _patVisitApp;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public PunctureController(IPunctureApp punctureApp, IPatVisitApp patVisitApp, IUsersService usersService, IMapper mapper)
        {
            _punctureApp = punctureApp;
            _patVisitApp = patVisitApp;
            _usersService = usersService;
            _mapper = mapper;
        }


        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var list = (from c in await _punctureApp.GetList(pagination, keyword)
                        join u in _usersService.GetUserNameDict("") on c.F_Nurse equals u.F_Id
                        select new
                        {
                            c.F_Id,
                            c.F_OperateTime,
                            c.F_Distance1,
                            c.F_Distance2,
                            c.F_Point1,
                            c.F_Point2,
                            c.F_Pid,
                            c.F_Memo,
                            c.F_LastModifyUserId,
                            c.F_LastModifyTime,
                            c.F_EnabledMark,
                            F_Nurse = u.F_RealName
                        }).ToList();

            var data = new
            {
                rows = list,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }


        public IActionResult GetFormJson(string keyValue)
        {
            var data = _punctureApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetListJson(string keyValue)
        {
            var data = (from r in await _punctureApp.GetList(keyValue)
                        join u in _usersService.GetUserNameDict("") on r.F_Nurse equals u.F_Id
                        select new
                        {
                            F_OperateTime = r.F_OperateTime.ToDateTimeString(true),
                            r.F_Point1,
                            r.F_Point2,
                            F_Memo = r.F_Memo ?? "",
                            u.F_RealName,
                            F_IsSuccess = r.F_IsSuccess ?? true,
                            r.F_Id
                        }).ToList();
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<PunctureDto> input)
        {
            PunctureEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<PunctureEntity>(input.Entity);
            }
            else
            {
                entity = await _punctureApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _punctureApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 获取最近的穿刺记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetRecentJson(string keyValue)
        {
            var data = await _punctureApp.GetList(keyValue, 15);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SaveData([FromBody]SaveDataInput input)
        {
            var patVisitEntity = await _patVisitApp.GetForm(input.VisitId);
            var userId = _usersService.GetCurrentUserId();
            var entity = new PunctureEntity
            {
                F_Pid = patVisitEntity.F_Pid,
                F_Nurse = userId,
                F_OperateTime = DateTime.Now,
                F_Point1 = input.Point1,
                F_Point2 = input.Point2,
                F_Memo = input.Memo,
                F_IsSuccess = input.IsSuccess,
                F_EnabledMark = true
            };
            await _punctureApp.InsertForm(entity);
            //更新治疗单 穿刺者信息
            patVisitEntity.F_PuncturePerson = userId;
            await _patVisitApp.UpdateForm(patVisitEntity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _punctureApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisableForm([FromBody]BaseInput input)
        {
            var entity = await _punctureApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            entity.F_LastModifyTime=DateTime.Now;
            entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
            await _punctureApp.UpdateForm(entity);
            return Success("停用成功。");
        }

        [HttpPost]
        public async Task<IActionResult> EnabledForm([FromBody]BaseInput input)
        {
            var entity = await _punctureApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = true;
            entity.F_LastModifyTime = DateTime.Now;
            entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
            await _punctureApp.UpdateForm(entity);
            return Success("启用成功。");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SavePics(IFormFile reportFile, string id)
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
                filePath = Path.Combine(filePath, serialNo + Path.GetExtension(reportFile.FileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await reportFile.CopyToAsync(stream);
                    stream.Flush();
                }
                filePath = filePath.Replace(AppConsts.AppRootPath, "").Replace("\\", "/").TrimStart('/');
            }
            var data = new
            {
                pathUrl = filePath
            };
            return Success(data.ToJson());
        }

        [HttpGet]
        public IActionResult GetPuncturePicPath(string keyValue)
        {
            var fileName = "";
            var filePath = Path.Combine(AppConsts.AppRootPath, "upload", "Puncture", keyValue);//Request.PhysicalApplicationPath + "pic\\upload\\VascularAccess\\" + keyValue;
            if (FileHelper.IsExistDirectory(filePath))
            {
                var rows = FileHelper.GetFileNamesWithoutPath(filePath);
                if (rows.Length > 0)
                {
                    fileName = (from f in rows
                        orderby f
                        select f).ToList().LastOrDefault();
                }
            }
            else
            {
                filePath = "";
            }
            var data = new
            {
                fileName,
                filePath = filePath.Replace(AppConsts.AppRootPath, "").Replace("\\", "/").TrimStart('/')
            };
            return Content(data.ToJson());
        }

    }
}
