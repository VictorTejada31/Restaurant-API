using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Features.Table.Commands.UpdateTable
{
    public class UpdateTableCommand : IRequest<Response<UpdateTableResponse>>
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
    }

    public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, Response<UpdateTableResponse>>
    {

        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public UpdateTableCommandHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateTableResponse>> Handle(UpdateTableCommand command, CancellationToken cancellationToken)
        {

            Tables table = await _tableRepository.GetByIdAsync(command.Id);
            if (table == null) throw new ApiExeption($"Table with id {command.Id} not found.",404);

            table = _mapper.Map<Tables>(command);

            Tables tableUpdated = await _tableRepository.UpdateAsync(table, command.Id);
            UpdateTableResponse response = _mapper.Map<UpdateTableResponse>(tableUpdated);

            return new Response<UpdateTableResponse>(response);
        }
    }
}
