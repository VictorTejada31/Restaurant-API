using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Dtos.Order
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Dishes { get; set; }
        public double SubTotal { get; set; }
        public double State { get; set; }
        public Tables Table { get; set; }
    }
}
