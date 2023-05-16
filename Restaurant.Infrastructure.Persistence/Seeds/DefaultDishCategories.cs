using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;


namespace Restaurant.Infrastructure.Persistence.Seeds
{
    public static class DefaultDishCategories
    {

        public static async Task AddAsync(IDishCategoryRepository _dishCategory)
        {
            DishCategory drink = new() { Name = "Drink"};
            DishCategory main = new() { Name = "Main Dish" };
            DishCategory entree = new() { Name = "Entree" };

            var dishes = await _dishCategory.GetAllAsync(); 

            if(dishes.All(dish => dish.Name != drink.Name))
            {
                await _dishCategory.AddAsync(drink);
                await _dishCategory.AddAsync(main);
                await _dishCategory.AddAsync(entree);

            }



        }
    }
}
