using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Interfaces.Repository;
using System.Text;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient
{
    public class UpdateDishCommand : IRequest<DishResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int PeopleAmount { get; set; }
        public List<int> Ingredients { get; set; }
        public int DishCategoryId { get; set; }
    }

    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, DishResponse>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        public UpdateDishCommandHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<DishResponse> Handle(UpdateDishCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Dish dish = await   _dishRepository.GetByIdAsync(command.Id);
            if (dish == null) throw new Exception($"Dish with id {command.Id} not found");

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var ingredient in command.Ingredients)
            {
                if (await _dishRepository.GetByIdAsync(ingredient) != null)
                {
                    stringBuilder.Append($"-{ingredient}");
                }
                else
                {
                    throw new Exception($"Ingredient with id {ingredient} not found");
                }
            }

            Domain.Entities.Dish dishUpdated = _mapper.Map<Domain.Entities.Dish>(command);
            dishUpdated.Ingredients = stringBuilder.ToString();

            var result = await _dishRepository.UpdateAsync(dishUpdated,command.Id);
            DishResponse response = _mapper.Map<DishResponse>(result);

            return response;
        }


    }
}
