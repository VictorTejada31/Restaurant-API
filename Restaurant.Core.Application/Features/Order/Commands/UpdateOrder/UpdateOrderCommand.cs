using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Features.Order.Commands.UpdateOrder;
using Restaurant.Core.Application.Interfaces.Repository;
using System.Text;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient
{
    public class UpdateOrderCommand : IRequest<UpdateOrderResponse>
    {
        public int Id { get; set; }
        public List<int> Dishes { get; set; }
        public double SubTotal { get; set; }
        public int Status { get; set; }
        public int TableId { get; set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ITableRepository _tableRepository;
        private readonly IDishRepository _DishRepository;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ITableRepository tableRepository, IDishRepository dishRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _tableRepository = tableRepository;
            _DishRepository = dishRepository;
        }

        public async Task<UpdateOrderResponse> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Order order = await _orderRepository.GetByIdAsync(command.Id);
            Domain.Entities.Tables table = await _tableRepository.GetByIdAsync(command.TableId);
            if (order == null) throw new Exception($"Order with id {command.Id} not found");
            if (table == null) throw new Exception($"Table with id {command.TableId} not found");
            if (command.Status > 2) throw new Exception($"Order Status with id {command.Status} not found");


            var dishes = await _DishRepository.GetAllAsync();
            StringBuilder stringBuilder = new StringBuilder();
            foreach(int id in command.Dishes)
            {
                if(await _DishRepository.GetByIdAsync(id) != null)
                {
                    stringBuilder.Append($"-{id.ToString()}");
                }
                else
                {
                    throw new Exception($"Dish with id {id} not found");
                }
            }

            Domain.Entities.Order orderUpdate = _mapper.Map<Domain.Entities.Order>(command);
            orderUpdate.Dishes = stringBuilder.ToString();
            var result = await _orderRepository.UpdateAsync(orderUpdate, command.Id);
            
            UpdateOrderResponse response = _mapper.Map<UpdateOrderResponse>(result);
            response.Status = GetOrderStatus(command.Status);
            response.Dishes = await GetDishes(result.Dishes);


            return response;
        }

        private async Task<List<string>> GetDishes(string dishString)
        {
            List<string> dishes = new();
            string[] dishArray = dishString.Split("-");

            foreach (var dish in dishArray)
            {
                if (dish != "")
                {
                    var _dish = await _DishRepository.GetByIdAsync(Int32.Parse(dish));
                    dishes.Add(_dish.Name);
                }
            }

            return dishes;
        }
        private string GetOrderStatus(int status)
        {
            return status == 1
                ? OrderStatus.InProgress.ToString()
                : OrderStatus.Completed.ToString();
        }

    }
}
