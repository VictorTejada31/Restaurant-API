﻿
using Restaurant.WebApi.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Restaurant.WebApi.Extention
{
    public static class AppExtention
    {
        public static void UseSwaggerGenExtention(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("swagger/v1/swagger.json", "Restaurant");
                options.DefaultModelRendering(ModelRendering.Model);
            });
        }

        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
