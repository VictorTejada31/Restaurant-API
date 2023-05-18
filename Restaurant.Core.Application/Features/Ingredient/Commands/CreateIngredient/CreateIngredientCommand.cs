using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient
{
    //<summary>
    // Parameters to create a new ingredient.
    //</summary>
    public class CreateIngredientCommand : IRequest
    {
        [SwaggerParameter(
            Description = "Ingredient Name"
            )]

        //<example>
        // Salt
        //</example>
        public string Name { get; set; }
    }

    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        public CreateIngredientCommandHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateIngredientCommand command, CancellationToken cancellationToken)
        {
            await _ingredientRepository.AddAsync(_mapper.Map<Domain.Entities.Ingredient>(command));
        }
    }
}
