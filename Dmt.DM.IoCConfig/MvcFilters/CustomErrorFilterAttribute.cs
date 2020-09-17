using Microsoft.AspNetCore.Mvc.Filters;

namespace Dmt.DM.IoCConfig.MvcFilters
{
    public class CustomErrorFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            
            var result = context.Result;
            var exception = context.Result;
            context.ExceptionHandled = true;
            base.OnException(context);
        }
    }
}
