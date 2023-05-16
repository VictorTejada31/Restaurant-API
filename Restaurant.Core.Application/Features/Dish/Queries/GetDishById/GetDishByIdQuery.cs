using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Interfaces.Repository;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById
{
    public class GetDishByIdQuery : IRequest<DishResponse>
    {
        public int Id { get; set; }
    }

    public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, DishResponse>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        public GetDishByIdQueryHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }
        public async Task<DishResponse> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Dish ingredients = await _dishRepository.GetByIdAsync(request.Id);
            if (ingredients == null ) throw new Exception($"Dish with id {request.Id} not found");
            DishResponse response = _mapper.Map<DishResponse>(ingredients);

            return response;
        }
    }

}
