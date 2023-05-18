using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Features.Order.Commands.UpdateOrder;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient
{
    //<summary>
    // Parameters to update order.
    //</summary>
    public class UpdateOrderCommand : IRequest<Response<UpdateOrderResponse>>
    {
        [SwaggerParameter(
           Description = "Order Id"
           )]
        //<example>
        // 2
        //</example>
        public int Id { get; set; }

        [SwaggerParameter(
           Description = "Dish id"
           )]

        //<example>
        // {2,1}
        //</example>

        public List<int> Dishes { get; set; }

        [SwaggerParameter(
           Description = "Order Sub Total"
           )]

        //<example>
        // 6.00
        //</example>

        public double SubTotal { get; set; }

        [SwaggerParameter(
            Description = "Status Id"
            )]

        //<example>
        // 2
        //</example>

        public int Status { get; set; }

        [SwaggerParameter(
            Description = "Table Id"
            )]

        //<example>
        // 2
        //</example>
        public int TableId { get; set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<UpdateOrderResponse>>
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

        public async Task<Response<UpdateOrderResponse>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Order order = await _orderRepository.GetByIdAsync(command.Id);
            Domain.Entities.Tables table = await _tableRepository.GetByIdAsync(command.TableId);
            if (order == null) throw new ApiExeption($"Order with id {command.Id} not found", 404);
            if (table == null) throw new ApiExeption($"Table with id {command.TableId} not found", 404);
            if (command.Status > 2) throw new ApiExeption($"Order Status with id {command.Status} not found", 404);


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
                    throw new ApiExeption($"Dish with id {id} not found",404);
                }
            }

            Domain.Entities.Order orderUpdate = _mapper.Map<Domain.Entities.Order>(command);
            orderUpdate.Dishes = stringBuilder.ToString();
            var result = await _orderRepository.UpdateAsync(orderUpdate, command.Id);
            
            UpdateOrderResponse response = _mapper.Map<UpdateOrderResponse>(result);
            response.Status = GetOrderStatus(command.Status);
            response.Dishes = await GetDishes(result.Dishes);


            return new Response<UpdateOrderResponse>(response);
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
