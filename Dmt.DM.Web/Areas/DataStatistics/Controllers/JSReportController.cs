using Dmt.DM.Application.DataReport;
using Dmt.DM.Code;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.DataStatistics.JSReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;

namespace Dmt.DM.Web.Areas.DataStatistics.Controllers
{
    [Area("DataStatistics")]
    public class JSReportController :BaseController
    {
        private readonly IDataReportJSApp _dataReportJsApp;
        private readonly IUsersService _usersService;

        public JSReportController(IDataReportJSApp dataReportJsApp, IUsersService usersService)
        {
            _dataReportJsApp = dataReportJsApp;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetListJson(string month)
        {
            var data = await _dataReportJsApp.GetList(month);
            return Content(data.ToJson());
        }

        public IActionResult GetMonthOptionsJson()
        {
            Hashtable table = new Hashtable();
            var currentDate = DateTime.Today;
            for (int i = -6; i <= 6; i++)
            {
                table.Add(i, currentDate.AddMonths(i).ToDateString().Substring(0, 7));
            }
            return Content(table.ToJson());
        }

        [HttpPost]
        public IActionResult SubmitData([FromBody]SubmitDataInput input)
        {
            var rootPath = AppConsts.AppRootPath;
            var userId = _usersService.GetCurrentUserId();
            var arr = input.Ids.Split(',');

            var data = new
            {
                arr,
                month = input.Month,
                userId,
                rootPath
            };
            foreach (var patientId in data.arr)
            {
                var doc = _dataReportJsApp.CreateRecord(data.month, patientId);
                var fileName = Common.CreateNo() + ".xml";

                var filePath = Path.Combine(rootPath, "DataReport", "JS", input.Month);
                if (!FileHelper.IsExistDirectory(filePath))
                {
                    FileHelper.CreateDirectory(filePath);
                }

                doc.Save(Path.Combine(filePath, fileName));
                _dataReportJsApp.SubmitData(patientId, data.month, fileName, filePath.Replace(rootPath, "").TrimStart('\\'), data.userId);
            }


            //Task.Run(() =>
            //{
            //    //var count = 0;
            //    var data = new
            //    {
            //        arr,
            //        month = input.Month,
            //        userId,
            //        rootPath
            //    };
            //    foreach (var patientId in data.arr)
            //    {
            //        var doc = _dataReportJsApp.CreateRecord(data.month, patientId);
            //        var fileName = Common.CreateNo() + ".xml";

            //        var filePath = Path.Combine(rootPath, "DataReport", "JS" + input.Month);
            //        if (!FileHelper.IsExistDirectory(filePath))
            //        {
            //            FileHelper.CreateDirectory(filePath);
            //        }

            //        doc.Save(Path.Combine(filePath, fileName));
            //        _dataReportJsApp.SubmitData(patientId, data.month, fileName, filePath.Replace(rootPath, ""), data.userId);
            //    }
               
            //    //NotifyPublishService.Instance.SendTasksMessageAsync((new NotifyModel
            //    //{
            //    //    Title = "数据上报",
            //    //    Content = "已生成" + data.arr.Length + "个数据文件！"
            //    //}).ToJson());
            //});

            //NotifyPublishService.Instance.SendTasksMessageAsync((new NotifyModel
            //{
            //    Title = "数据上报",
            //    Content = "正在生成" + arr.Length + "个人的数据文件"
            //}).ToJson());
            return Success("操作成功");
        }

        private void SaveResult(string month, string rootPath, string userId, string patientId)
        {
            var doc = _dataReportJsApp.CreateRecord(month, patientId);
            var fileName = Common.CreateNo() + ".xml";

            var filePath = Path.Combine(rootPath, "DataReport", "JS" + month);
            if (!FileHelper.IsExistDirectory(filePath))
            {
                FileHelper.CreateDirectory(filePath);
            }
            doc.Save(Path.Combine(filePath, fileName));
            _dataReportJsApp.SubmitData(patientId, month, fileName, filePath, userId);
        }

        [HttpPost]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public ActionResult Download(string ids)
        {
            var basePath = Path.Combine(AppConsts.AppRootPath, "DataReport", "JS");
            var doc = _dataReportJsApp.CombineXmlFile(ids, basePath);
            if (!FileHelper.IsExistDirectory(basePath))
            {
                FileHelper.CreateDirectory(basePath);
            }

            var downloadPath = Path.Combine(basePath, "download");
            if (!FileHelper.IsExistDirectory(downloadPath))
            {
                FileHelper.CreateDirectory(downloadPath);
            }
            var fileName = Common.CreateNo() + ".xml";
            var fullPath = Path.Combine(downloadPath, fileName);
            doc.Save(fullPath);
            var fileExt = FileHelper.GetExtension(fullPath);
            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            var data = FileHelper.GetFileData(fullPath);
            return File(data, memi, FileHelper.GetFileName(fullPath));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteData([FromBody]BaseInput input)
        {
            var arr = input.KeyValue.Split(',');
            foreach (var item in arr)
            {
               await _dataReportJsApp.DeleteForm(item);
            }
            return Success("操作成功");
        }
    }
}
