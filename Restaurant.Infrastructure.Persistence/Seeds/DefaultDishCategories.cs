using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Repositories;

namespace Restaurant.Infrastructure.Persistence.Seeds
{
    public static class DefaultDishCategories
    {
        public static async void AddAsync(DishCategoryRepository repository)
        {
            DishCategory drink = new() { Name = "Drink"};
            DishCategory main = new() { Name = "Main Dish" };
            DishCategory entree = new() { Name = "Entree" };

            var dishes = await repository.GetAllAsync(); 

            if(dishes.All(dish => dish.Id != drink.Id))
            {
                await repository.AddAsync(drink);
                await repository.AddAsync(main);
                await repository.AddAsync(entree);

            }



        }
    }
}
