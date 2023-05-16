using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient;
using Restaurant.Core.Application.Interfaces.Repository;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById
{
    public class GetIngredientByIdQuery : IRequest<TableResponse>
    {
        public int Id { get; set; }
    }

    public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, TableResponse>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        public GetIngredientByIdQueryHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<TableResponse> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Ingredient ingredients = await _ingredientRepository.GetByIdAsync(request.Id);
            if (ingredients == null ) throw new Exception($"Ingredient with id {request.Id} not found");
            TableResponse response = _mapper.Map<TableResponse>(ingredients);

            return response;
        }
    }

}
