using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.ApiControllers
{
    [Authorize]
    [EnableCors("CorsPolicy")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
