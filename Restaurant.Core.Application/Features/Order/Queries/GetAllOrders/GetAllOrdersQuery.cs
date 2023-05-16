using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Interfaces.Repository;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient
{
    public class GetAllOrdersQuery : IRequest<IList<OrderResponse>>
    {

    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IList<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<IList<OrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Order> ingredients = await _orderRepository.GetAllWithIncludesAsync(new List<string> {"Table"});
            if (ingredients == null || ingredients.Count == 0) throw new Exception("Orders not found");
            IList<OrderResponse> response = _mapper.Map<IList<OrderResponse>>(ingredients);

            return response;
        }
    }

}
