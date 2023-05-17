using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient
{
    public class GetAllIngredientQuery : IRequest<Response<IList<IngredientResponse>>>
    {

    }

    public class GetAllIngredientQueryHandler : IRequestHandler<GetAllIngredientQuery, Response<IList<IngredientResponse>>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        public GetAllIngredientQueryHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<Response<IList<IngredientResponse>>> Handle(GetAllIngredientQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Ingredient> ingredients = await _ingredientRepository.GetAllAsync();
            if (ingredients == null || ingredients.Count == 0) throw new ApiExeption("Ingredients not found", 404);
            IList<IngredientResponse> response = _mapper.Map<IList<IngredientResponse>>(ingredients);

            return new Response<IList<IngredientResponse>>(response);
        }
    }

}
