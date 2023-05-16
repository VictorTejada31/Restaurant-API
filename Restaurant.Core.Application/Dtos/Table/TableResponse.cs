using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Dtos.Table
{
    public class TableResponse
    {
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

    }
}
