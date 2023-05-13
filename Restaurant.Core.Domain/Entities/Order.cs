
using Restaurant.Core.Domain.Commons;

namespace Restaurant.Core.Domain.Entities
{
    public class Order : BaseCommomProperties
    {
        public string Dishes { get; set; }
        public double SubTotal { get; set; }
        public double State { get; set; }
        public string TableId { get; set; }

        //Navegation Property
        public Table Table { get; set; }
    }
}
