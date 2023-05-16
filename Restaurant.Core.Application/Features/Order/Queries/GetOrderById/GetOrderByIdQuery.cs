using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Interfaces.Repository;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById
{
    public class GetOrderByIdQuery : IRequest<OrderResponse>
    {
        public int Id { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Order ingredients = await _orderRepository.GetByIdAsync(request.Id);
            if (ingredients == null ) throw new Exception($"Dish with id {request.Id} not found");
            OrderResponse response = _mapper.Map<OrderResponse>(await _orderRepository.GetAllWithIncludesAsync(new List<string> {"Table"}));

            return response;
        }
    }

}
