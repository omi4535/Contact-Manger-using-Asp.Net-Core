using ContactManger.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContract.DTO.Person;

namespace ContactManger.Filters.ActionFilters
{
    public class personActionFilter : IActionFilter
    {
        private readonly ILogger<personActionFilter> _log;
        private readonly string _TryArg;
        public personActionFilter(ILogger<personActionFilter> log,string TryArg)
        {
            _log = log;
            _TryArg = TryArg;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _log.LogInformation("On Action Executed");
            PersonController persons = (PersonController)context.Controller;
            persons.ViewData["XYZ"] = "XYZ1";
            _log.LogError("{Arg}", _TryArg);
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _log.LogInformation("On Action Executing");
            if (context.ActionArguments.ContainsKey("addReq"))
            {
                PersonAddReq? req = (PersonAddReq?)context.ActionArguments["addReq"];

                _log.LogInformation("Adding Person name :"+req.FirstName + " "+req.LastName);
            }
        }
    }
}
