using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Restaurant.Core.Application.Dtos.Account;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Settings;
using Restaurant.Infrastructure.Identity.Context;
using Restaurant.Infrastructure.Identity.Entities;
using Restaurant.Infrastructure.Identity.Services;
using System.Text;
using System.Text.Json.Serialization;

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
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
                
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDeniedPath";
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = configuration["JWTSettings:Audience"],
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))

                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c => {

                        c.NoResult();
                        c.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are Unauthorized."});
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = StatusCodes.Status403Forbidden;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You can't access to this end point." });
                        return c.Response.WriteAsync(result);
                    }

                };
            });
          

            #endregion

        }
    }
}