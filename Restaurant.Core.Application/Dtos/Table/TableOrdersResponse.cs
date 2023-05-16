namespace Restaurant.Core.Application.Dtos.Table
{
    public class TableOrdersResponse
    {
        public int Id { get; set; }
        public ICollection<Domain.Entities.Order> Orders {  get; set; }  
    }
}
