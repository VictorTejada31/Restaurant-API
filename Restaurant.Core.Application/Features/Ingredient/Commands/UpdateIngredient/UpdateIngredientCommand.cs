using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient
{
    public class UpdateIngredientCommand : IRequest<Response<UpdateIngredientResponse>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, Response<UpdateIngredientResponse>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        public UpdateIngredientCommandHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateIngredientResponse>> Handle(UpdateIngredientCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Ingredient ingredient = await _ingredientRepository.GetByIdAsync(command.Id);
            if (ingredient == null) throw new ApiExeption($"Ingredient with id {command.Id} not found",404);

            var result = await _ingredientRepository.UpdateAsync(_mapper.Map<Domain.Entities.Ingredient>(command),command.Id);
            UpdateIngredientResponse response = _mapper.Map<UpdateIngredientResponse>(result);

            return new Response<UpdateIngredientResponse>(response);
        }
    }
}
