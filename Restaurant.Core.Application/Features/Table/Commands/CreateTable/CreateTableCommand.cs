using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurant.Core.Application.Features.Table.Commands.CreateTableCommand
{
    //<summary>
    // Parameters to create a new table.
    //</summary>
    public class CreateTableCommand : IRequest
    {
        [SwaggerParameter(
           Description = "Table Capacity"
           )]
        //<example>
        // 2
        //</example>
        public int Capacity { get; set; }

        [SwaggerParameter(
           Description = "Table Description"
           )]
        //<example>
        // Table for 2 peoples.
        //</example>
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
            Tables table = _mapper.Map<Tables>(command);
            table.Status = TableStatus.Available.ToString();
            await _tableRepository.AddAsync(table);
        }
    }
}
