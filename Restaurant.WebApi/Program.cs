
using Microsoft.AspNetCore.Identity;
using Restaurant.Infrastructure.Identity.Entities;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Core.Application;
using Restaurant.Infrastructure.Identity;
using Restaurant.Infrastructure.Persistence.Repositories;
using Restaurant.Infrastructure.Identity.Seeds;
using Restaurant.Infrastructure.Persistence.Seeds;
using Restaurant.WebApi.Extention;
using Restaurant.Core.Application.Interfaces.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationLayer();
builder.Services.AddIndentityInfras(builder.Configuration);
builder.Services.AddPersistenceInfras(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenExtention();
builder.Services.AddApiVertioningExtention();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHealthChecks();
builder.Services.AddSession();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var defaultDishCategories = services.GetRequiredService<IDishCategoryRepository>();

    await DefaultRoles.AddAsync(roleManager);
    await DefaultAdminUser.AddAsync(userManager);
    await DefaultWaiterUser.AddAsync(userManager);
    await DefaultDishCategories.AddAsync(defaultDishCategories);
    

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSwaggerGenExtention();
app.UseErrorHandlingMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseHealthChecks("/Health");
app.UseApiVersioning();
app.MapControllers();

app.Run();
