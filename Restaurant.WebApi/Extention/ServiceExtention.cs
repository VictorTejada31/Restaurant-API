using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Restaurant.WebApi.Extention
{
    public static class ServiceExtention
    {
        public static void AddSwaggerGenExtention(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory,"*.xml",SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFiles => options.IncludeXmlComments(xmlFiles));
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Restaurant Api",
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Name = "Victor Enmanuel Tejada Zabala",
                        Email = "victorenmanuel28@gmail.com"
                    }
                });

                options.DescribeAllParametersInCamelCase();
            });
        }
        public static void AddApiVertioningExtention(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
        }
    }
}
