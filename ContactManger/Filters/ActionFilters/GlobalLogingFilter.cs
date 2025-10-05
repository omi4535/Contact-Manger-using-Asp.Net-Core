using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ContactManger.Filters.ActionFilters
{
    public class GlobalLogingFilter : IActionFilter
    {
        private readonly ILogger<GlobalLogingFilter> _log;
        public GlobalLogingFilter(ILogger<GlobalLogingFilter> log)
        {
            _log = log;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

            _log.LogInformation("{ControllerName}.{ActionName}", context.RouteData.Values["controller"].ToString(), context.RouteData.Values["action"].ToString());
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach(var a in context.ActionArguments)
            {
                _log.LogInformation("{key} : {value}",a.Key,a.Value);
            }
            

        }
    }
}
