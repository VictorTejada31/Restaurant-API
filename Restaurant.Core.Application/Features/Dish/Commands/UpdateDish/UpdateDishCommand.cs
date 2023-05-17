using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Features.Dish.Commands.UpdateDish;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;
using System.Text;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient
{
    public class UpdateDishCommand : IRequest<Response<UpdateDishResponse>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int PeopleAmount { get; set; }
        public List<int> Ingredients { get; set; }
        public int DishCategoryId { get; set; }
    }

    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, Response<UpdateDishResponse>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IDishCategoryRepository _categoryRepository;
        public UpdateDishCommandHandler(IDishRepository dishRepository, IMapper mapper, IIngredientRepository ingredientRepository, IDishCategoryRepository categoryRepository)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<UpdateDishResponse>> Handle(UpdateDishCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Dish dish = await   _dishRepository.GetByIdAsync(command.Id);
            if (dish == null) throw new ApiExeption($"Dish with id {command.Id} not found",404);
            if (command.DishCategoryId > 3) throw new ApiExeption($"Dish Category with id {command.DishCategoryId} not found",404);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var ingredient in command.Ingredients)
            {
                if (await _ingredientRepository.GetByIdAsync(ingredient) != null)
                {
                    stringBuilder.Append($"-{ingredient}");
                }
                else
                {
                    throw new ApiExeption($"Ingredient with id {ingredient} not found",404);
                }
            }

            Domain.Entities.Dish dishUpdated = _mapper.Map<Domain.Entities.Dish>(command);
            dishUpdated.Ingredients = stringBuilder.ToString();

            var result = await _dishRepository.UpdateAsync(dishUpdated,command.Id);
            UpdateDishResponse response = _mapper.Map<UpdateDishResponse>(result);

            List<string> ingredients = new();
            string[] dishArray = result.Ingredients.Split("-");

            foreach (var ingredient in dishArray)
            {
                if (ingredient != "")
                {
                    var _ingredient = await _ingredientRepository.GetByIdAsync(Int32.Parse(ingredient));
                    ingredients.Add(_ingredient.Name);
                }
            }

            response.Ingredients = ingredients;
            response.DishCategory = await DishCategory(dish.DishCategoryId);
            return new Response<UpdateDishResponse>(response);
        }

        private async Task<string> DishCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category.Name;
        }

    }
}
