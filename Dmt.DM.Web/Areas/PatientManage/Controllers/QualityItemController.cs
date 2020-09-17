using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.QualityItem;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class QualityItemController : BaseController
    {
        private readonly IQualityItemApp _qualityItemApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public QualityItemController(IQualityItemApp qualityItemApp, IMapper mapper, IUsersService usersService)
        {
            _qualityItemApp = qualityItemApp;
            _mapper = mapper;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string queryJson)
        {
            var itemType = string.Empty;
            var keyword = string.Empty;
            if (!string.IsNullOrWhiteSpace(queryJson))
            {
                var queryParam = queryJson.ToJObject();
                itemType = queryParam["timeType"]?.Value<string>();
                keyword = queryParam["keyword"]?.Value<string>();
            }

            var data = new
            {
                rows = await _qualityItemApp.GetList(pagination, itemType, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
           return Content(data.ToJson());
        }

        public async Task<IActionResult> GetSelectJson()
        {
            var data = (await _qualityItemApp.GetList()).Select(t => new
            {
                t.Id,
                Code = t.ItemCode,
                Name = t.ItemName
            });
            return Content(data.ToList().ToJson());
        }

        public async Task<IActionResult> GetPartitionSelectJson()
        {
            var data = (await _qualityItemApp.GetPartitedList()).Select(t => new
            {
                t.Id,
                Code = t.ItemCode,
                Name = t.ItemName
            });
            return Content(data.ToList().ToJson());
        }

        public async Task<IActionResult> GetListJson()
        {
            var data = await _qualityItemApp.GetList();
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _qualityItemApp.GetForm(keyValue);
            return Content(data.ToJson());
        }


        public IActionResult GetPartitionListJson(string keyword)
        {
            var data =  _qualityItemApp.GetPartitionList(keyword).Select(t => new
            {
                orderNo = t.F_OrderNo,
                lowerCheck = t.F_LowerCheck,
                lowerValue = t.F_LowerValue,
                upperCheck = t.F_UpperCheck,
                upperValue = t.F_UpperValue
            }).ToList().OrderBy(r => r.orderNo);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]SubmitFormInput input)
        {
            QualityItemEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<QualityItemEntity>(input.Entity);
            }
            else
            {
                entity = await _qualityItemApp.GetForm(input.KeyValue);
            }

            var partitions = input.Partitions == null
                ? new List<QualityItemPartitionEntity>()
                : _mapper.Map<List<QualityItemPartitionEntity>>(input.Partitions);
            await _qualityItemApp.SubmitForm(entity, partitions, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _qualityItemApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisableForm([FromBody]BaseInput input)
        {
            var entity = await _qualityItemApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            await _qualityItemApp.UpdateForm(entity);
            return Success("停用成功。");
        }

        [HttpPost]
        public async Task<IActionResult> EnabledForm([FromBody]BaseInput input)
        {
            var entity = await _qualityItemApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = true;
            await _qualityItemApp.UpdateForm(entity);
            return Success("启用成功。");
        }
    }
}
