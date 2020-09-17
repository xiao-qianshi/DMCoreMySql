using AutoMapper;
using Dmt.DM.Application.LabLis;
using Dmt.DM.Code;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.LabLis.LabTest;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.LabLis.Controllers
{
    [Area("LabLis")]
    public class LabTestController : BaseController
    {
        private readonly ILabTestApp _labTestApp;
        private readonly ILabRequestApp _labRequestApp;
        private readonly IMapper _mapper;

        public LabTestController(ILabTestApp labTestApp, ILabRequestApp labRequestApp, IMapper mapper)
        {
            _labTestApp = labTestApp;
            _labRequestApp = labRequestApp;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AssignSample([FromBody]AssignSampleInput input)
        {
            var sheets = await _labRequestApp.GetFormByBarcode(input.Barcode);
            if (!sheets.Any())
            {
                return Error("未查询到样本信息！");
            }
            else if (sheets.Any(t => t.F_Status < 3))
            {
                return Error("标本未采样，不能进行编号操作！");
            }
            else if (sheets.Any(t => t.F_Status > 3))
            {
                return Error("标本已编号！");
            }
            else if (sheets.All(t => t.F_Status != 3))
            {
                return Error("未查询到样本信息！");
            }
            //校验是否在此仪器上处理 未完善
            //return Error("无此仪器上的处理项目");
            var findSheet = sheets.FirstOrDefault(t => t.F_Status == 3);
            var data = await _labTestApp.AssignSample(input.InstrumentId, input.TestDate?.ToDate() ?? DateTime.Today, findSheet?.F_Id);
            return Success("操作成功", data);
        }

       public async Task<IActionResult> GetListJson(string instrumentId, DateTime? testDate)
        {
            var list = await _labTestApp.GetList(instrumentId, testDate?.ToDate() ?? DateTime.Today);
            return Content(list.ToJson());
        }

        public IActionResult GetDetailedListJson(string instrumentId, DateTime? testDate)
        {
            var data = _labTestApp.GetDetailedList(instrumentId, testDate?.ToDate() ?? DateTime.Today);
            return Content(data);
        }

        public IActionResult GetResultListJson(string keyValue)
        {
            var entity = _labTestApp.GetForm(keyValue);
            var list = _labTestApp.GetReport(entity.F_TestId);
            var data = list.Select(t => new
            {
                t.F_Id,
                t.F_ItemId,
                t.F_Code,
                t.F_CreatorTime,
                t.F_IsCritical,
                t.F_LowRef,
                t.F_MethodName,
                t.F_Name,
                t.F_Result,
                t.F_ResultText,
                t.F_ShortName,
                t.F_Sorter,
                t.F_TestId,
                t.F_Unit,
                t.F_UpperRef,
                t.F_Flag
            }).OrderBy(t => t.F_Sorter);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitItemData([FromBody]SubmitItemDataInput input)
        {
            var data = await _labTestApp.SaveItem(input.TestId, input.Items);
            return Success("保存成功", data);
        }

        [HttpPost]
        public async Task<IActionResult> AuditTest([FromBody]BaseInput input)
        {
            var entity = _labTestApp.GetForm(input.KeyValue);
            var message = await _labTestApp.AuditTest(entity);
            return Success("保存成功", input.KeyValue);
        }

        /// <summary>
        /// 样本签收页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult Assign()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult AddItem()
        {
            return View();
        }
    }
}
