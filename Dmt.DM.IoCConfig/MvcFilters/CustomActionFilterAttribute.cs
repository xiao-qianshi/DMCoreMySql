using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Dmt.DM.Code;

namespace Dmt.DM.IoCConfig.MvcFilters
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
            {
                var data = new OkObjectResult(new ApiResultModel
                {
                    StatusCode = okObjectResult?.StatusCode ?? 200,
                    Data = okObjectResult?.Value,
                    IsSuccess = true,
                    ErrorMessage = ""
                });
                context.Result = data;
            }
            else if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                var data = new BadRequestObjectResult(new ApiResultModel
                {
                    StatusCode = badRequestObjectResult?.StatusCode ?? 500,
                    Data = new object[] { },
                    IsSuccess = false,
                    ErrorMessage = (badRequestObjectResult?.Value ?? "").ToString()
                });
                context.Result = data;
            }
            base.OnActionExecuted(context);
        }
    }
}
