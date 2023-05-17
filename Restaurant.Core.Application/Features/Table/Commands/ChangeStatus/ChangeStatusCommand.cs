using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Features.Table.Commands.ChangeStatus
{
    public class ChangeStatusCommand : IRequest
    {
        public int Id { get; set; }
        public int StatusId { get; set; }

    }

    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public ChangeStatusCommandHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }
        public async Task Handle(ChangeStatusCommand commnad, CancellationToken cancellationToken)
        {
            Tables table = await _tableRepository.GetByIdAsync(commnad.Id);
            if (table == null) throw new ApiExeption("Table not found",404);
            if(commnad.Id > 3) throw new ApiExeption("Table Status not found",404);


            await ChangeTableStatus(table, commnad.Id);

        }

        private async Task ChangeTableStatus(Tables table, int statusId)
        {
            switch (statusId)
            {
                case 1:
                    table.Status = TableStatus.Available.ToString();
                    break;
                case 2:
                    table.Status = TableStatus.InOrderingProcess.ToString();
                    break;
                case 3:
                    table.Status = TableStatus.OrderCompleted.ToString();
                    break;
            }

            await _tableRepository.UpdateAsync(table, table.Id);
        }
    }
}
