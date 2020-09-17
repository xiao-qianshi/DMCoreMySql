using Dmt.DM.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

namespace Dmt.DM.Web.Controllers
{
    [EnableCors("CorsPolicy")]
    public class HomeController : Controller
    {
        //注入内存缓存
        //private readonly IMemoryCache _memoryCache = null;
        //private IHttpContextAccessor _httpContext = null;
        //private readonly ClaimsIdentity _claimsIdentity = null;
        //private readonly IUsersService _usersService = null;
        public HomeController(/*IMemoryCache memoryCache*/ILogger<HomeController> log)
        {
            //_memoryCache = memoryCache
            log.LogInformation("测试");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
