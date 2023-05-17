using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;

namespace Restaurant.Core.Application.Features.Table.Queries.GetTableOrders
{
    public class GetTableOrdersQuery : IRequest<Response<TableOrdersResponse>>
    {
        public int Id { get; set; }
    }

    public class GetTableOrdersQueryHandler : IRequestHandler<GetTableOrdersQuery, Response<TableOrdersResponse>>
    {
        private readonly ITableRepository _tableRepository;

        public GetTableOrdersQueryHandler(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }
        public async Task<Response<TableOrdersResponse>> Handle(GetTableOrdersQuery request, CancellationToken cancellationToken)
        {
            TableOrdersResponse response = await GetAllWithFilters(request.Id);
            if (response == null) throw new ApiExeption("Table not Found", 404);
            if (response != null && response.Orders.Count == 0) throw new ApiExeption("This table has no orders in progress", 404);

            return new Response<TableOrdersResponse>(await GetAllWithFilters(request.Id));

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
