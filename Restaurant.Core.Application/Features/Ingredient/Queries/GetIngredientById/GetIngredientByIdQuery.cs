using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById
{
    public class GetIngredientByIdQuery : IRequest<Response<IngredientResponse>>
    {
        public int Id { get; set; }
    }

    public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, Response<IngredientResponse>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        public GetIngredientByIdQueryHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<Response<IngredientResponse>> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Ingredient ingredients = await _ingredientRepository.GetByIdAsync(request.Id);
            if (ingredients == null ) throw new ApiExeption($"Ingredient with id {request.Id} not found", 404);
            IngredientResponse response = _mapper.Map<IngredientResponse>(ingredients);

            return new Response<IngredientResponse>(response);
        }
    }

}
