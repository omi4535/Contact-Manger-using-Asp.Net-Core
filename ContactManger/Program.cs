using ContactManger.serviceExtensionMethod;
using ContactManger.Middleware;
namespace ContactManger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.ServiceCongi(builder.Configuration);
            var app = builder.Build();
            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseExceptionHandlingMiddleware();
            }
                app.UseStaticFiles();
            Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", "Ratativa");
            //app.Logger.LogInformation("info");
            //app.Logger.LogCritical("Critical");
            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}
