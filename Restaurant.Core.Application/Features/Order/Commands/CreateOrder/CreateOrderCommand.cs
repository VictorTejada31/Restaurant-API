using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Interfaces.Repository;
using System.Text;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient
{
    public class CreateOrderCommand : IRequest
    {
        public List<int> Dishes { get; set; }
        public double SubTotal { get; set; }
        public int TableId { get; set; }

    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ITableRepository _tableRepository;
        private readonly IDishRepository _DishRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,ITableRepository tableRepository, IDishRepository dishRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _tableRepository = tableRepository;
            _DishRepository = dishRepository;
        }

        public async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {

            Domain.Entities.Tables table = await _tableRepository.GetByIdAsync(command.TableId);
            if (table == null) throw new Exception($"Table with id {command.TableId} not found");

            var dishes = await _DishRepository.GetAllAsync();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (int id in command.Dishes)
            {
                if (await _DishRepository.GetByIdAsync(id) != null)
                {
                    stringBuilder.Append($"-{id.ToString()}");
                }
                else
                {
                    throw new Exception($"Dish with id {id} not found");
                }
            }

            Domain.Entities.Order order = _mapper.Map<Domain.Entities.Order>(command);
            order.Dishes = stringBuilder.ToString();
            order.Status = 1;
            await _orderRepository.AddAsync(order);
        }
    }
}
