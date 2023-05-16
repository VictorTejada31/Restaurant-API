using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Dtos.Order
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public IList<string> Dishes { get; set; }
        public double SubTotal { get; set; }
        public string Status { get; set; }
        public int TableId { get; set; }
    }
}
