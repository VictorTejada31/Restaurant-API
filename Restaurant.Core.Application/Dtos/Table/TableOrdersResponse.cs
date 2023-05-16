using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Dtos.Table
{
    public class TableOrdersResponse
    {
        public int Id { get; set; }
        public ICollection<Order> Orders {  get; set; }  
    }
}
