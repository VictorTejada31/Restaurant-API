using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Interfaces.Repository;

namespace Restaurant.Core.Application.Features.Table.Queries.GetAllById
{
    public class GetTableByIdQuery : IRequest<TableResponse>
    {
        public int Id { get; set; }
    }

    public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, TableResponse>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public GetTableByIdQueryHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<TableResponse> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            TableResponse response = _mapper.Map<TableResponse>(await _tableRepository.GetByIdAsync(request.Id));
            if (response == null) throw new Exception("Table not found");

            return response;
        }
    }
}
