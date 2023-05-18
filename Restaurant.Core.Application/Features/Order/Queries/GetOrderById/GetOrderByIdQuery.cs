using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById
{
    //<summary>
    // Parameters to a specified order.
    //</summary>
    public class GetOrderByIdQuery : IRequest<Response<OrderResponse>>
    {
        [SwaggerParameter(
           Description = "Order Id"
           )]
        //<example>
        // 2
        //</example>
        public int Id { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IDishRepository _dishRepository;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper, IDishRepository dishRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _dishRepository = dishRepository;
        }
        public async Task<Response<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Order order = await _orderRepository.GetByIdAsync(request.Id);
            if (order == null ) throw new ApiExeption($"Dish with id {request.Id} not found", 404);
            OrderResponse response = _mapper.Map<OrderResponse>(await _orderRepository.GetByIdAsync(request.Id));
            
          
            response.Dishes = await GetDishes(order);
            response.Status = GetOrderStatus(order.Status);

            return new Response<OrderResponse>(response);
        }

        private async Task<List<string>> GetDishes(Domain.Entities.Order order)
        {
            List<string> dishes = new();
            string[] dishArray = order.Dishes.Split("-");

            foreach (var dish in dishArray)
            {
                if (dish != "")
                {
                    var _dish = await _dishRepository.GetByIdAsync(Int32.Parse(dish));
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
