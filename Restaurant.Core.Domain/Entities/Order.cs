
using Restaurant.Core.Domain.Commons;

namespace Restaurant.Core.Domain.Entities
{
    public class Order : BaseCommomProperties
    {
        public string Dishes { get; set; }
        public double SubTotal { get; set; }
        public int Status { get; set; }
        public int TableId { get; set; }

        //Navegation Property
        public Tables Table { get; set; }
    }
}
