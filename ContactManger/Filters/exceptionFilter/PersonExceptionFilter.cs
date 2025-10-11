using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactManger.Filters.exceptionFilter
{
    public class PersonExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<PersonExceptionFilter> _logger;
        public PersonExceptionFilter(ILogger<PersonExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            if (context.Filters.OfType<SkipFilter>().Any())
            {
                return;
            }
            _logger.LogError(context.Exception, "Route : " + context.RouteData.Values["controller"] + "." + context.RouteData.Values["action"]);
            context.ExceptionHandled = true;
            context.Result = new JsonResult("Error Occured");
        }
    }
}
