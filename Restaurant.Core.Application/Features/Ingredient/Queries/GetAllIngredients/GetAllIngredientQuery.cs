using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient
{
    public class GetAllIngredientQuery : IRequest<IList<TableResponse>>
    {

    }

    public class GetAllIngredientQueryHandler : IRequestHandler<GetAllIngredientQuery, IList<TableResponse>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        public GetAllIngredientQueryHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<IList<TableResponse>> Handle(GetAllIngredientQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Ingredient> ingredients = await _ingredientRepository.GetAllAsync();
            if (ingredients == null || ingredients.Count == 0) throw new Exception("Ingredients not found");
            IList<TableResponse> response = _mapper.Map<IList<TableResponse>>(ingredients);

            return response;
        }
    }

}
