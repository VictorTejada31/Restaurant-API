using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Infrastructure.Identity.Context;
using Restaurant.Infrastructure.Identity.Entities;
using Restaurant.Infrastructure.Identity.Services;

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

            services.AddTransient<IAccountService, AccountService>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
                
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDeniedPath";
            });

            services.AddAuthentication();

            #endregion

        }
    }
}