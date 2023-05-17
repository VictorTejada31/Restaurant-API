using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;

namespace Restaurant.Core.Application.Features.Table.Queries.GetAllTables
{
    public class GetAllTablesQuery : IRequest<Response<IList<TableResponse>>>
    {
    }

    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTablesQuery, Response<IList<TableResponse>>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public GetAllTablesQueryHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }


        public async Task<Response<IList<TableResponse>>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var tables = _mapper.Map<IList<TableResponse>>(await _tableRepository.GetAllAsync());
            if (tables == null || tables.Count == 0) throw new ApiExeption("Tables not found", 404);

            return new Response<IList<TableResponse>>(tables);
        }
    }
}
