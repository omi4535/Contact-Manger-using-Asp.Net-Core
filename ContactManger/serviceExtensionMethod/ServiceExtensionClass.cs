using ContactManger.Filters.ActionFilters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceContract;
using services;

namespace ContactManger.serviceExtensionMethod
{
    public static class ServiceExtensionClass
    {
        public static IServiceCollection ServiceCongi(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddControllersWithViews(options =>
            {
                options.Filters.Add<GlobalLogingFilter>(0);
            })
                .AddViewOptions(options =>
                {
                    options.HtmlHelperOptions.ClientValidationEnabled = true;
                }); ;

            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ContactManger;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
            service.AddScoped<ICountryService, CountryService>();
            service.AddScoped<IPerson, PersonService>();
            service.AddDbContext<ContactMangerDBContext>(
                option => {
                    option.UseSqlServer
                            //(builder.Configuration["ConnectionStrings:DefaultCon"]));
                            (configuration.GetConnectionString("DefaultCon"));
                });
            return service;
        }
    }
}
