using FluentValidation;
using Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient;

namespace Restaurant.Core.Application.Features.Dish.Commands.CreateDish
{
    public class CreateDishCommandValidation : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidation() { 
        
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .MaximumLength(150).WithMessage("{PropertyName} shouldn't excee {MaximumLength}")
               .NotEqual("string").WithMessage("{PropertyName} shound't be equals to string 0");

            RuleFor(x => x.Price)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0.");

            RuleFor(x => x.PeopleAmount)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0.");

            RuleFor(x => x.DishCategoryId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0.");

            RuleFor(x => x.Ingredients)
              .NotNull().WithMessage("Ingredients {CollectionIndex} is required.");
              




        }
    }
}
