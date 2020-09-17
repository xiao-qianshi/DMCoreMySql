using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.Material;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class MaterialController : BaseController
    {
        private readonly IMaterialApp _materialApp;
        private readonly IMapper _mapper;

        public MaterialController(IMaterialApp materialApp, IMapper mapper)
        {
            _materialApp = materialApp;
            _mapper = mapper;
        }
        /// <summary>
        /// 返回耗材列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetListJson(string keyword)
        {
            var data = await _materialApp.GetList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 返回透析器列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSelectJson(string keyword)
        {
            var data = from r in await _materialApp.GetDialyzerList()
                       select new
                       {
                           id = r.F_Id,
                           text = r.F_MaterialName
                       };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 返回指定类别耗材列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSelectByTypeJson(string keyword)
        {
            var data = from r in await _materialApp.GetListByType(keyword)
                       select new
                       {
                           id = r.F_Id,
                           text = r.F_MaterialName
                       };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _materialApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _materialApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<MaterialDto> input)
        {
            MaterialEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<MaterialEntity>(input.Entity);
            }
            else
            {
                entity = await _materialApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _materialApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _materialApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var entity = await _materialApp.GetForm(input.KeyValue);
            if (entity != null)
            {
                entity.F_EnabledMark = false;
                await _materialApp.UpdateForm(entity);
            }
           
            return Success("耗材禁用成功。");
        }

        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var entity = await _materialApp.GetForm(input.KeyValue);
            if (entity != null)
            {
                entity.F_EnabledMark = true;
                await _materialApp.UpdateForm(entity);
            }

            return Success("耗材启用成功。");
        }
    }
}
