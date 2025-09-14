using ServiceContract;
using services;


namespace ContactManger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews()
                 .AddViewOptions(options =>
                 {
                     options.HtmlHelperOptions.ClientValidationEnabled = true;
                 }); ;
            builder.Services.AddSingleton<ICountryService,CountryService>();
            builder.Services.AddSingleton<IPerson, PersonService>();
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}
