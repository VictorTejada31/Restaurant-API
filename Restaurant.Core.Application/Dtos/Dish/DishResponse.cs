
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Dtos.Dish
{
    public class DishResponse
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int PeopleAmount { get; set; }
        public List<Domain.Entities.Ingredient> Ingredients { get; set; }
        public int DishCategoryId { get; set; }
    }
}
