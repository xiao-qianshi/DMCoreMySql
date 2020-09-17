using Dmt.DM.Code;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.Controllers
{
    [Authorize]
    [EnableCors("CorsPolicy")]
    public abstract class BaseController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult Form()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult Details()
        {
            return View();
        }
        protected virtual IActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual IActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual IActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }
    }
}
