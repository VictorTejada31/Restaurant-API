using MediatR;
using Restaurant.Core.Application.Interfaces.Repository;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurant.Core.Application.Features.Dish___Copia.Commands.DeleteOrder
{
    //<summary>
    // Parameters to delete a specified order.
    //</summary>
    public class DeleteOrderCommand : IRequest
    {
        [SwaggerParameter(
           Description = "Order Id"
           )]
        //<example>
        // 2
        //</example>
        public int Id { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            await _orderRepository.DeleteAsync(command.Id);
        }
    }
}
