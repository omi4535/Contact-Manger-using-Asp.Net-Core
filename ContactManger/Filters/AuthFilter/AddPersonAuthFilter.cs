using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactManger.Filters.AuthFilter
{
    public class AddPersonAuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Cookies.ContainsKey("Auth-key")){
                context.Result = new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
                return;
            } else if (context.HttpContext.Request.Cookies["Auth-key"] != "Auth001")
            {
                context.Result = new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
                return;
            }
        }

        
    }
}
