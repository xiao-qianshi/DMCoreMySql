using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.Controllers
{
    [EnableCors("CorsPolicy")]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OutLogin()
        {
            //new LogApp().WriteDbLog(new LogEntity
            //{
            //    F_ModuleName = "系统登录",
            //    F_Type = DbLogType.Exit.ToString(),
            //    F_Account = OperatorProvider.Provider.GetCurrent().UserCode,
            //    F_NickName = OperatorProvider.Provider.GetCurrent().UserName,
            //    F_Result = true,
            //    F_Description = "安全退出系统",
            //});
            return RedirectToAction("Index", "Login");
            //return Content("");
        }
    }
}