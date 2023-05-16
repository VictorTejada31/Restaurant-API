namespace Restaurant.Core.Application.Features.Dish.Commands.UpdateDish
{
    public class UpdateDishResponse
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int PeopleAmount { get; set; }
        public List<string> Ingredients { get; set; }
        public string DishCategory { get; set; }
    }
}
