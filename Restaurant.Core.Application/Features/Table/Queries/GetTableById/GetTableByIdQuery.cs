using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;

namespace Restaurant.Core.Application.Features.Table.Queries.GetAllById
{
    public class GetTableByIdQuery : IRequest<Response<TableResponse>>
    {
        public int Id { get; set; }
    }

    public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, Response<TableResponse>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public GetTableByIdQueryHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<Response<TableResponse>> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            TableResponse response = _mapper.Map<TableResponse>(await _tableRepository.GetByIdAsync(request.Id));
            if (response == null) throw new ApiExeption("Table not found", 404);

            return new Response<TableResponse>(response);
        }
    }
}
