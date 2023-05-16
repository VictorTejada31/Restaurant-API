using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;


namespace Restaurant.Core.Application.Features.Table.Commands.CreateTableCommand
{
    public class CreateTableCommand : IRequest
    {

        public int Capacity { get; set; }
        public string Description { get; set;}

    }

    public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public CreateTableCommandHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        
        public async Task Handle(CreateTableCommand command, CancellationToken cancellationToken)
        {

            await _tableRepository.AddAsync(_mapper.Map<Tables>(command));
        }
    }
}
