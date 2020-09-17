using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.DataStatistics.Controllers
{
    [Area("DataStatistics")]
    public class DataReportController : BaseController
    {
        private readonly IDataReportApp _reportApp;

        public DataReportController(IDataReportApp reportApp)
        {
            _reportApp = reportApp;
        }

        public IActionResult GetDataReportJson(string keyValue)
        {
            var data = _reportApp.CreateData(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]

        public IActionResult SubmitDataSet(string keyValue)
        {
            _reportApp.SubmitData(keyValue);
            return Success("操作成功。");
        }

        //[HttpPost]
        //public void Download(string keyValue)
        //{
        //    string filepath = _reportApp.CopyModelFile();// Server.MapPath(data.F_FilePath);
        //    //GetFileName
        //    string filename = FileHelper.GetFileName(filepath);
        //    if (FileDownHelper.FileExists(filepath))
        //    {
        //        FileDownHelper.DownLoadold(filepath, filename);
        //    }
        //}

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Download(string keyValue)
        {
            var filepath = _reportApp.CopyModelFile();// Server.MapPath(data.F_FilePath);

            var fileExt = FileHelper.GetExtension(filepath);
            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            var data = FileHelper.GetFileData(filepath);
            return File(data, memi, FileHelper.GetFileName(filepath));
        }

    }
}
