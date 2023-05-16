namespace Restaurant.Core.Application.Features.Table.Commands.UpdateTable
{
    public class UpdateTableResponse
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
    }
}
