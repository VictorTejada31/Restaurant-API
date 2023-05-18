using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Interfaces.Repository;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient
{
    //<summary>
    // Parameters to create a new Dish.
    //</summary>
    public class CreateDishCommand : IRequest
    {
        [SwaggerParameter(
            Description = "Dish Name"
            )]

        //<example>
        // Pizza
        //</example>
        public string Name { get; set; }

        [SwaggerParameter(
            Description = "Dish Price"
            )]
        //<example>
        // 5
        //</example>
        public double Price { get; set; }

        [SwaggerParameter(
            Description = "For how many people"
            )]
        //<example>
        // 3.00
        //</example>
        public int PeopleAmount { get; set; }

        [SwaggerParameter(
            Description = "Ingredients Ids"
            )]
        //<example>
        // {1,2,5}
        //</example>
        public List<int> Ingredients { get; set; }

        [SwaggerParameter(
            Description = "Dish Category Id"
            )]
        //<example>
        // 2
        //</example>
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
