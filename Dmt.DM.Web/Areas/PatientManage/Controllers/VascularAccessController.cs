using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.VascularAccess;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class VascularAccessController : BaseController
    {
        private readonly IVascularAccessApp _vascularAccessApp;
        private readonly IMapper _mapper;

        public VascularAccessController(IVascularAccessApp vascularAccessApp, IMapper mapper)
        {
            _vascularAccessApp = vascularAccessApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetListJson(string keyword)
        {
            var list = await _vascularAccessApp.GetList(keyword);
            return Content(list.ToJson());

        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _vascularAccessApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<VascularAccessDto> input)
        {
            VascularAccessEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<VascularAccessEntity>(input.Entity);
            }
            else
            {
                entity = await _vascularAccessApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _vascularAccessApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _vascularAccessApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DisableForm([FromBody]BaseInput input)
        {
            var entity = await _vascularAccessApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            await _vascularAccessApp.UpdateForm(entity);
            return Success("停用成功。");
        }

        [HttpPost]
        public async Task<IActionResult> EnabledForm([FromBody]BaseInput input)
        {
            var entity = await _vascularAccessApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            entity.F_EnabledMark = true;
            //entity.F_FirstUseTime = DateTime.Now;
            await _vascularAccessApp.UpdateForm(entity);
            return Success("启用成功。");
        }


        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SavePics(IFormFile reportFile, string id)
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
                    filePath = Path.Combine(filePath, serialNo + Path.GetExtension(reportFile.FileName));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await reportFile.CopyToAsync(stream);
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
            return Success(data.ToJson());
        }

       
        public IActionResult GetVascularAccessPicPath(string keyValue)
        {
            var fileName = "";
            var filePath = Path.Combine(AppConsts.AppRootPath, "upload", "VascularAccess", keyValue);//Request.PhysicalApplicationPath + "pic\\upload\\VascularAccess\\" + keyValue;
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
                filePath = filePath.Replace(AppConsts.AppRootPath,"").Replace("\\","/").TrimStart('/')
            };
            return Content(data.ToJson());
        }


    }
}
