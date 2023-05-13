
using Restaurant.Core.Domain.Commons;

namespace Restaurant.Core.Domain.Entities
{
    public class Dish : BaseCommomProperties
    {
      public string Name { get; set; }
      public double Price { get; set; }
      public int PeopleAmount { get; set; }
      public string Ingredients { get; set; }
      public int DishCategoryId { get; set; }

      //Navegation Property
      public DishCategory DishCategory { get; set; }

    }
}

