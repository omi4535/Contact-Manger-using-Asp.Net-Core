using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactManger.Filters.ResultFilters
{
    public class CountryResultFilter : IAsyncResultFilter
    {
        private readonly string method;

        public CountryResultFilter(string Method)
        {
            method = Method;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            context.HttpContext.Response.Headers["Before_Result_Filter"]= DateTime.Now.ToString();
            await next();
            context.HttpContext.Response.Headers["After_Result_Filter"] = DateTime.Now.ToString();
            //context.HttpContext.Response.StatusCode = 300;
        }
    }
}
