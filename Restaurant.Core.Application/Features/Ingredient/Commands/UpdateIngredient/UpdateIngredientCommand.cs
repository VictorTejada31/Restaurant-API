using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient
{
    //<summary>
    // Parameters to create a new ingredient.
    //</summary>
    public class UpdateIngredientCommand : IRequest<Response<UpdateIngredientResponse>>
    {
        [SwaggerParameter(
            Description = "Ingredient Id"
            )]

        //<example>
        // 3
        //</example>
        public int Id { get; set; }

        [SwaggerParameter(
            Description = "Ingredient Name"
            )]

        //<example>
        // Salt
        //</example>
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
