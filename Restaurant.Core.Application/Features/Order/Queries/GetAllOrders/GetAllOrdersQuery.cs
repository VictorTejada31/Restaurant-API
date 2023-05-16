using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Repository;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient
{
    public class GetAllOrdersQuery : IRequest<IList<OrderResponse>>
    {

    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IList<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDishRepository _dishRepository;
        public GetAllOrdersQueryHandler(IOrderRepository orderRepository,  IDishRepository dishRepository)
        {
            _orderRepository = orderRepository;
            _dishRepository = dishRepository;
        }
        public async Task<IList<OrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Order> orders = await _orderRepository.GetAllWithIncludesAsync(new List<string> {"Table"});
            if (orders == null || orders.Count == 0) throw new Exception("Orders not found");
           

            return await GetAllWithInclude(orders);
        }

        private async Task<IList<OrderResponse>> GetAllWithInclude(List<Domain.Entities.Order> orders)
        {
            IList<OrderResponse> response = new List<OrderResponse>();

            foreach(var order in orders)
            {
                OrderResponse _order = new()
                {
                    Id = order.Id,
                    Status = GetOrderStatus(order.Status),
                    TableId = order.TableId,
                    SubTotal = order.SubTotal,
                    Dishes = await GetDishes(order.Dishes)
                };

                response.Add(_order);
            }


            return response;
        }

        private async Task<List<string>> GetDishes(string dishesString)
        {
            List<string> dishes = new();
            string[] dishArray = dishesString.Split("-");

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
