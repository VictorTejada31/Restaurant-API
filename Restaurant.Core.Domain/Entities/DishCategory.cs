using Restaurant.Core.Domain.Commons;

namespace Restaurant.Core.Domain.Entities
{
    public class DishCategory : BaseCommomProperties
    {
        public string Name { get; set; }

        //Navegation Property
        public ICollection<Dish> Dishes { get; set; }
    }
}
