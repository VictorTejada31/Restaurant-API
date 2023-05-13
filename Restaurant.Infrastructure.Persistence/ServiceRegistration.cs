using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Infrastructure.Persistence.Contexts;
using Restaurant.Infrastructure.Persistence.Repositories;

namespace Restaurant.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfras(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context Config

            if(configuration.GetValue<bool>("InMemoryDatabse"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("RestaurantInMemoryDB"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), m => m
                .MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }

            #endregion

            #region Service Config

            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<IDishRepository,DishRepository>();
            services.AddTransient<IDishCategoryRepository, DishCategoryRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();


            #endregion
        }
    }
}
