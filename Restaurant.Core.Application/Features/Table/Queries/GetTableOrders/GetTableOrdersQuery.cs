using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Interfaces.Repository;

namespace Restaurant.Core.Application.Features.Table.Queries.GetTableOrders
{
    public class GetTableOrdersQuery : IRequest<TableOrdersResponse>
    {
        public int Id { get; set; }
    }

    public class GetTableOrdersQueryHandler : IRequestHandler<GetTableOrdersQuery, TableOrdersResponse>
    {
        private readonly ITableRepository _tableRepository;

        public GetTableOrdersQueryHandler(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }
        public async Task<TableOrdersResponse> Handle(GetTableOrdersQuery request, CancellationToken cancellationToken)
        {
            TableOrdersResponse response = await GetAllWithFilters(request.Id);
            if (response == null) throw new Exception("Table not Found");
            if (response != null && response.Orders.Count == 0) throw new Exception("This table has no orders in progress");

            return response;

        }

        private async Task<TableOrdersResponse> GetAllWithFilters(int tableId)
        {
            var tables = await _tableRepository.GetAllWithIncludesAsync(new List<string> {"Orders"});
            var list = tables.Select(table => new TableOrdersResponse
            {
                Id = table.Id,
                Orders = table.Orders
            });

            return list.Where(table => table.Id == tableId).FirstOrDefault();
        }
    }
}
