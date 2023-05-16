using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Enums;
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
            if (table == null) throw new Exception("Table not found");
            if(commnad.Id > 3) throw new Exception("Table Status not found");


            await ChangeTableStatus(table, commnad.Id);

        }

        private async Task ChangeTableStatus(Tables table, int statusId)
        {
            switch (statusId)
            {
                case 1:
                    table.State = TableStatus.Available.ToString();
                    break;
                case 2:
                    table.State = TableStatus.InOrderingProcess.ToString();
                    break;
                case 3:
                    table.State = TableStatus.OrderCompleted.ToString();
                    break;
            }

            await _tableRepository.UpdateAsync(table, table.Id);
        }
    }
}
