using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient
{
    public class GetAllDishQuery : IRequest<IList<DishResponse>>
    {

    }

    public class GetAllDishQueryHandler : IRequestHandler<GetAllDishQuery, IList<DishResponse>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        public GetAllDishQueryHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }
        public async Task<IList<DishResponse>> Handle(GetAllDishQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Dish> ingredients = await _dishRepository.GetAllAsync();
            if (ingredients == null || ingredients.Count == 0) throw new Exception("Dishes not found");
            IList<DishResponse> response = _mapper.Map<IList<DishResponse>>(ingredients);

            return response;
        }
    }

}
