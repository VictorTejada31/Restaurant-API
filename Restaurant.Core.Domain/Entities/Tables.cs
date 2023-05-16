
using Restaurant.Core.Domain.Commons;

namespace Restaurant.Core.Domain.Entities
{
    public class Tables : BaseCommomProperties
    { 
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string State { get; set; }

        //Navegation Properties
        public ICollection<Order> Orders { get; set; }
    }
}

