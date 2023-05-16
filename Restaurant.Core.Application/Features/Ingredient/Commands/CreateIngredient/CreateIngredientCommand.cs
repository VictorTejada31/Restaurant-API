using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient
{
    public class CreateIngredientCommand : IRequest
    {
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
