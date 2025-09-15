using ServiceContract;
using services;
using Microsoft.EntityFrameworkCore;


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

            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ContactManger;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<IPerson, PersonService>();
            builder.Services.AddDbContext<ContactMangerDBContext>(
                option => {
                    option.UseSqlServer
                            //(builder.Configuration["ConnectionStrings:DefaultCon"]));
                            (builder.Configuration.GetConnectionString("DefaultCon"));
                           });
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}
