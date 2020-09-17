using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.MachineManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.MachineManage.WaterMBrief;
using Dmt.DM.Mapper.Dto.MachineManage.WaterMObservation;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.MachineManage.Controllers
{
    [Area("MachineManage")]
    public class WaterMMonitorController : BaseController
    {
        private readonly IWaterMBriefApp _waterMBriefApp;
        private readonly IWaterMObservationApp _waterMObservationApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public WaterMMonitorController(IWaterMBriefApp serviceBrief, IWaterMObservationApp serviceObservation, IMapper mapper, IUsersService usersService)
        {
            _waterMBriefApp = serviceBrief;
            _waterMObservationApp = serviceObservation;
            _mapper = mapper;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetBriefFormJson(string keyValue)
        {
            var data = await _waterMBriefApp.GetForm(keyValue);
            return Content((data??new WaterMBriefEntity()).ToJson());
        }

        public async Task<IActionResult> GetBriefFormByDateJson(DateTime recordDate)
        {
            var data = await _waterMBriefApp.GetFormByDate(recordDate);
            return Content((data??new WaterMBriefEntity()).ToJson());
        }


        [HttpPost]
        public async Task<IActionResult> DeleteBriefForm([FromBody]BaseInput input)
        {
            await _waterMBriefApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        public async Task<IActionResult> GetObservationFormJson(string keyValue)
        {
            var data = await _waterMObservationApp.GetForm(keyValue);
            return Content((data??new WaterMObservationEntity()).ToJson());
        }

        public async Task<IActionResult> GetObservationFormByDateJson(DateTime recordDate)
        {
            var data = await _waterMObservationApp.GetFormByDate(recordDate);
            return Content((data ?? new WaterMObservationEntity()).ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteObservationForm([FromBody]BaseInput input)
        {
            await _waterMObservationApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }


        #region 签名 核对
        [HttpPost]
        public async Task<IActionResult> SubmitBriefForm([FromBody]BaseSubmitInput<WaterMBriefDto> input)
        {
            WaterMBriefEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<WaterMBriefEntity>(input.Entity);
            }
            else
            {
                entity = await _waterMBriefApp.GetForm(input.KeyValue);
            }
            if (entity.F_OperatePerson == null)
            {
                var user = await _usersService.GetCurrentUserAsync();
                entity.F_OperatePerson = user.F_Id;
                entity.F_OperatePersonName = user.F_RealName;
            }
            if (entity.F_EnabledMark == null)
            {
                entity.F_EnabledMark = true;
            }
            await _waterMBriefApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SignBriefForm([FromBody]BaseInput input)
        {
            var entity = await _waterMBriefApp.GetForm(input.KeyValue);
            if (entity == null)
            {
                return Error("未找到记录");
            }
            if (entity.F_CheckPerson != null)
            {
                return Error("记录已核对签名！");

            }
            var user = await _usersService.GetCurrentUserAsync();
            entity.F_CheckPerson = user.F_Id;
            entity.F_CheckPersonName = user.F_RealName;
            await _waterMBriefApp.UpdateForm(entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitObservationForm([FromBody]BaseSubmitInput<WaterMObservationDto> input)
        {
            WaterMObservationEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<WaterMObservationEntity>(input.Entity);
                if (entity.F_OperatePerson == null)
                {
                    var user = await _usersService.GetCurrentUserAsync();
                    entity.F_OperatePerson = user.F_Id;
                    entity.F_OperatePersonName = user.F_RealName;
                }
                if (entity.F_EnabledMark == null)
                {
                    entity.F_EnabledMark = true;
                }
            }
            else
            {
                entity = await _waterMObservationApp.GetForm(input.KeyValue);
            }

            await _waterMObservationApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SignObservationForm([FromBody]BaseInput input)
        {
            var entity = await _waterMObservationApp.GetForm(input.KeyValue);
            if (entity == null)
            {
                return Error("未找到记录");
            }
            if (entity.F_CheckPerson != null)
            {
                return Error("记录已核对签名！");

            }
            var user = await _usersService.GetCurrentUserAsync();
            entity.F_CheckPerson = user.F_Id;
            entity.F_CheckPersonName = user.F_RealName;
            await _waterMObservationApp.UpdateForm(entity);
            return Success("操作成功。");
        }
        #endregion

        #region 下载
        /// <summary>
        /// 按月份下载下载
        /// </summary>
        /// <param name="keyValue">日期 yyyy-mm-dd</param>
        [HttpPost]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public IActionResult Download(string keyValue)
        {
            string filepath = _waterMBriefApp.CopyModelFile();//Server.MapPath(_waterMBriefApp.CopyModelFile());// Server.MapPath(data.F_FilePath);
            string filename = FileHelper.GetFileName(filepath);

            if (!DateTime.TryParse(keyValue, out var currentDate))
            {
                currentDate = DateTime.Today;
            }
            var startDate = currentDate.Date.AddDays(1 - currentDate.Day);
            var endDate = startDate.AddMonths(1);

            _waterMBriefApp.FillData(startDate, endDate, filepath, filename);

            var fileExt = FileHelper.GetExtension(filepath);
            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            var data = FileHelper.GetFileData(filepath);
            return File(data, memi, FileHelper.GetFileName(filepath));

        }
        #endregion
    }
}
