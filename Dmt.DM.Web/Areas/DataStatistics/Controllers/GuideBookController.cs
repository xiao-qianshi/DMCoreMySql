using System;
using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Web.Areas.DataStatistics.Controllers
{
    [Area("DataStatistics")]
    public class GuideBookController : BaseController
    {
        private readonly IBookApp _bookApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public GuideBookController(IBookApp bookApp, IMapper mapper, IUsersService usersService)
        {
            _bookApp = bookApp;
            _mapper = mapper;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _bookApp.GetList(pagination, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _bookApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SavePics(IFormFile reportFile, int id)
        {
            var serialNo = Common.CreateNo();
            var targetPath = Path.Combine(AppConsts.AppRootPath, "GuideBook");
            FileHelper.CreateDirectory(targetPath);
            var fileName = reportFile.FileName/*Path.Combine(targetPath, serialNo + Path.GetExtension(reportFile.FileName))*/;
            var fileExtension = Path.GetExtension(reportFile.FileName);//.TrimStart('.').ToUpper();
            var fileSize = reportFile.Length;
            //var fileName = Path.Combine(targetPath, serialNo + Path.GetExtension(reportFile.FileName));
            using (var stream =
                new FileStream(Path.Combine(targetPath, serialNo + fileExtension), FileMode.Create))
            {
                await reportFile.CopyToAsync(stream);
                stream.Flush();
            }

            var pathUrl = Path.Combine(targetPath, serialNo + fileExtension).Replace(AppConsts.AppRootPath, "")
                .Replace("\\", "/").TrimStart('/');
            var book = new BookEntity
            {
                F_FileIndex = serialNo + fileExtension,
                F_FileSize = FileHelper.ToFileSize(fileSize),
                F_BookName = fileName,
                F_FileType = fileExtension.TrimStart('.'),
                F_FilePath = pathUrl,//Path.Combine(targetPath, serialNo + fileExtension),
                F_UploadDate = DateTime.Now,
                F_EnabledMark = true,
                F_UploadPerson =""
            };
            await _bookApp.SubmitForm(book, null);

            var data = new
            {
                pathUrl
            };
            return Success(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _bookApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }
    }
}
