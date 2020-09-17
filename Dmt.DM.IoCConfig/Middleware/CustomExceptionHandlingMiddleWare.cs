using Dmt.DM.Code;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dmt.DM.IoCConfig.Middleware
{
    public class CustomExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<CustomExceptionHandlingMiddleware> _logger;
        //private IHostingEnvironment _environment;

        public CustomExceptionHandlingMiddleware(RequestDelegate next, IHostingEnvironment environment, ILogger<CustomExceptionHandlingMiddleware> logger)
        {
            this._next = next;
            _logger = logger;
            //this._environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                var features = context.Features;
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private async Task HandleException(HttpContext context, Exception e)
        {
            context.Response.ContentType = "text/json;charset=utf-8;";

            if (Regex.IsMatch( context.Request.Path.Value.ToLower(), "/api/*/*"))
            {
                var data = new ApiResultModel()
                {
                    StatusCode = 500,
                    ErrorMessage = e.Message,
                    Data = new object[] { },
                    IsSuccess = false
                };

                await context.Response.WriteAsync(data.ToJson());
            }
            else
            {
                var data = new AjaxResult
                {
                    state = "error",
                    message = e.Message
                };

                await context.Response.WriteAsync(data.ToJson());
            }

            var errorMsg =
                $"【抛出信息】：{e.Message} \r\n【异常类型】：{e.GetType().Name} \r\n【异常信息】：{e.Message} \r\n【堆栈调用】：\r\n{e.StackTrace}";
            _logger.LogError(errorMsg);
        }
    }
}
