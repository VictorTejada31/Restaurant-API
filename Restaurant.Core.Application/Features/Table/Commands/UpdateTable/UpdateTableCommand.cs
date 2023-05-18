using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;
using Restaurant.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurant.Core.Application.Features.Table.Commands.UpdateTable
{
    //<summary>
    // Parameters to uptate a specified table.
    //</summary>
    public class UpdateTableCommand : IRequest<Response<UpdateTableResponse>>
    {
        [SwaggerParameter(
           Description = "Table Id"
           )]
        //<example>
        // 4
        //</example>
        public int Id { get; set; }

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
