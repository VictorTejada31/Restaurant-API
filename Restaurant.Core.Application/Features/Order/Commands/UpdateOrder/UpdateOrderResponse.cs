namespace Restaurant.Core.Application.Features.Order.Commands.UpdateOrder
{
    public class UpdateOrderResponse
    {
        public int Id { get; set; }
        public IList<string> Dishes { get; set; }
        public double SubTotal { get; set; }
        public string Status { get; set; }
        public int TableId { get; set; }
    }
}
