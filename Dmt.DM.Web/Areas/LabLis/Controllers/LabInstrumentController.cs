using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.LabLis;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.LabLis;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.LabLis.LabInstrument;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.LabLis.Controllers
{
    [Area("LabLis")]
    public class LabInstrumentController : BaseController
    {
        private readonly ILabInstrumentApp _labInstrumentApp;
        private readonly IMapper _mapper;

        public LabInstrumentController(ILabInstrumentApp labInstrumentApp, IMapper mapper)
        {
            _labInstrumentApp = labInstrumentApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = await _labInstrumentApp.GetList(pagination, queryJson),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 查询仪器列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetListJson()
        {
            var data = _labInstrumentApp.GetList().Select(t => new
            {
                t.F_Id,
                t.F_Code,
                t.F_CommunicateMode,
                t.F_DecodeEngine,
                t.F_IsDuplex,
                t.F_IsExternal,
                t.F_Name,
                t.F_PicPath,
                t.F_ShortName,
                t.F_Supplier,
                t.F_TestType,
                t.F_WorkStationIp
            });
            return Content(data.ToJson());
        }

        /// <summary>
        /// 查询仪器检验项目
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetItemJson(string keyValue)
        {
            var data = _labInstrumentApp.GetItem(keyValue).Select(t => new
            {
                t.F_Id,
                t.F_ChannelValue,
                t.F_CnName,
                t.F_Code,
                t.F_ConvertCoefficient,
                t.F_CriticalMaxValue,
                t.F_CriticalMinValue,
                t.F_DefaultValue,
                t.F_EnName,
                t.F_IsQualityItem,
                t.F_KeepDecimal,
                t.F_MachineId,
                t.F_Name,
                t.F_PY,
                t.F_QualityItemId,
                t.F_QualityItemName,
                t.F_ReferenceMaxValue,
                t.F_ReferenceMinValue,
                t.F_ReferenceTextValue,
                t.F_ResultType,
                t.F_Sorter,
                t.F_ValueUnit,
                t.IsHiddenItem
            }).OrderBy(t => t.F_Sorter);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetSingleItemJson(string keyValue)
        {
            var t = await _labInstrumentApp.GetSingleItem(keyValue);
            var data = new
            {
                t.F_Id,
                t.F_ChannelValue,
                t.F_CnName,
                t.F_Code,
                t.F_ConvertCoefficient,
                t.F_CriticalMaxValue,
                t.F_CriticalMinValue,
                t.F_DefaultValue,
                t.F_EnName,
                t.F_IsQualityItem,
                t.F_KeepDecimal,
                t.F_MachineId,
                t.F_Name,
                t.F_PY,
                t.F_QualityItemId,
                t.F_QualityItemName,
                t.F_ReferenceMaxValue,
                t.F_ReferenceMinValue,
                t.F_ReferenceTextValue,
                t.F_ResultType,
                t.F_Sorter,
                t.F_ValueUnit,
                t.IsHiddenItem
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除子项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteItem([FromBody]BaseInput input)
        {
            await _labInstrumentApp.DeleteItem(input.KeyValue);
            return Success("操作成功");
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _labInstrumentApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SetCommunicate([FromBody]SetCommunicateInput input)
        {
            var entity = await _labInstrumentApp.GetForm(input.KeyValue);
            entity.F_CommunicateConfig = input.Settings;
            await _labInstrumentApp.UpdateForm(entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitItemForm([FromBody]BaseSubmitInput<LabInstrumentItemDto> input)
        {
            LabInstrumentItemEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<LabInstrumentItemEntity>(input.Entity);
            }
            else
            {
                entity = await _labInstrumentApp.GetSingleItem(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            var data = await _labInstrumentApp.SubmitItemForm(entity, input.Entity);
            return Success("操作成功。", data);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<LabInstrumentDto> input)
        {
            LabInstrumentEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<LabInstrumentEntity>(input.Entity);
            }
            else
            {
                entity = await _labInstrumentApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _labInstrumentApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _labInstrumentApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var entity = await _labInstrumentApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            await _labInstrumentApp.UpdateForm(entity);
            return Success("项目(" + entity.F_Name + ")禁用成功。");
        }
        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var entity = await _labInstrumentApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = true;
            await _labInstrumentApp.UpdateForm(entity);
            return Success("项目(" + entity.F_Name + ")启用成功。");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Setting()
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
        public IActionResult ChooseInstrument()
        {
            return View();
        }

    }
}
