using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;
using Restaurant.Core.Domain.Entities;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient
{
    public class GetAllDishQuery : IRequest<Response<IList<DishResponse>>>
    {

    }

    public class GetAllDishQueryHandler : IRequestHandler<GetAllDishQuery, Response<IList<DishResponse>>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IDishCategoryRepository _dishCategoryRepository;
        public GetAllDishQueryHandler(IDishRepository dishRepository, IIngredientRepository ingredientRepository,IDishCategoryRepository dishCategory)
        {
            _dishRepository = dishRepository;
            _ingredientRepository = ingredientRepository;
            _dishCategoryRepository = dishCategory;
        }
        public async Task<Response<IList<DishResponse>>> Handle(GetAllDishQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Dish> dishes = await _dishRepository.GetAllAsync();
            if (dishes == null || dishes.Count == 0) throw new ApiExeption("Dishes not found",404);
            IList<DishResponse> response = new List<DishResponse>();

            foreach (var dish in dishes)
            {
                DishResponse _dish = new() {

                    Name = dish.Name,
                    Ingredients = await Ingredients(dish.Ingredients),
                    Price = dish.Price,
                    PeopleAmount = dish.PeopleAmount,
                    DishCategory = await DishCategory(dish.DishCategoryId)

                };

                response.Add(_dish);
            }

            return new Response<IList<DishResponse>>(response);
        }

        private async Task<List<string>> Ingredients(string dish)
        {
            List<string> ingredients = new();
            string[] dishArray = dish.Split("-");

            foreach (var ingredient in dishArray)
            {
                if (ingredient != "")
                {
                    var _ingredient = await _ingredientRepository.GetByIdAsync(Int32.Parse(ingredient));
                    ingredients.Add(_ingredient.Name);
                }
            }

            return ingredients;
        } 

        private async Task<string> DishCategory(int id)
        {
            var category = await _dishCategoryRepository.GetByIdAsync(id);
            return category.Name;
        }
    }

}
