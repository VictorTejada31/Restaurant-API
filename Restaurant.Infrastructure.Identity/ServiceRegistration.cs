using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Infrastructure.Identity.Context;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity
{
    public  static class ServiceRegistration
    {
       public static void AddIndentityInfras(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context

            if (configuration.GetValue<bool>("InMemoryDatabse"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("RestaurantInMemoryDB"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("IdentityConnection"), m => m
                .MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }

            #endregion

            #region Identity

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
                

            services.AddAuthentication();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDeniedPath";
            });

            #endregion

        }
    }
}