using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Application.Wrappers;


namespace Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById
{
    public class GetDishByIdQuery : IRequest<Response<DishResponse>>
    {
        public int Id { get; set; }
    }

    public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, Response<DishResponse>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IDishCategoryRepository _categoryRepository;
        public GetDishByIdQueryHandler(IDishRepository dishRepository, IMapper mapper, IIngredientRepository ingredientRepository, IDishCategoryRepository categoryRepository)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<DishResponse>> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Dish dish = await _dishRepository.GetByIdAsync(request.Id);
            if (dish == null ) throw new ApiExeption($"Dish with id {request.Id} not found", 404);
            DishResponse response = _mapper.Map<DishResponse>(dish);

            List<string> ingredients = new();
            string[] dishArray = dish.Ingredients.Split("-");

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

            return new Response<DishResponse>(response);
        }

        private async Task<string> DishCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category.Name;
        }

    }

}
