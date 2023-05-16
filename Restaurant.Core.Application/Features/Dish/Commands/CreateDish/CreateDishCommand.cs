using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Interfaces.Repository;
using System.Text;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient
{
    public class CreateDishCommand : IRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int PeopleAmount { get; set; }
        public List<int> Ingredients { get; set; }
        public int DishCategoryId { get; set; }

    }

    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        private readonly IIngredientRepository _ingredientRepository;
        public CreateDishCommandHandler(IDishRepository dishRepository, IMapper mapper, IIngredientRepository ingredientRepository)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
        }

        public async Task Handle(CreateDishCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Dish dish = _mapper.Map<Domain.Entities.Dish>(command);
            if (command.DishCategoryId > 3) throw new Exception($"Dish Category with id {command.DishCategoryId} not found");


            StringBuilder stringBuilder = new StringBuilder();
            foreach (var ingredient in command.Ingredients)
            {
                if (await _ingredientRepository.GetByIdAsync(ingredient) != null)
                {
                    stringBuilder.Append($"-{ingredient}");
                }
                else
                {
                    throw new Exception($"Ingredient with id {ingredient} not found");
                }
            }

            dish.Ingredients = stringBuilder.ToString();
            await _dishRepository.AddAsync(dish);
        }
    }
}
