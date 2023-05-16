using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Interfaces.Repository;
using System.Text;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient
{
    public class CreateOrderCommand : IRequest
    {
        public string Dishes { get; set; }
        public double SubTotal { get; set; }
        public double State { get; set; }
        public string TableId { get; set; }

    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Order order = _mapper.Map<Domain.Entities.Order>(command);
            await _orderRepository.AddAsync(order);  
        }
    }
}
