using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Interfaces.Repository;
using System.Text;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient
{
    public class UpdateOrderCommand : IRequest<OrderResponse>
    {
        public int Id { get; set; }
        public string Dishes { get; set; }
        public double SubTotal { get; set; }
        public double State { get; set; }
        public string TableId { get; set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Order order = await _orderRepository.GetByIdAsync(command.Id);
            if (order == null) throw new Exception($"Order with id {command.Id} not found");


            await _orderRepository.UpdateAsync(_mapper.Map<Domain.Entities.Order>(command),command.Id);
            var orders = await _orderRepository.GetAllWithIncludesAsync(new List<string> { "Table" });
            OrderResponse response = _mapper.Map<OrderResponse>(orders.FirstOrDefault(order => order.Id == command.Id));

            return response;
        }


    }
}
