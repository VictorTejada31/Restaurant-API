using FluentValidation;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient
{
    public class UpdateIngredientCommandValidation : AbstractValidator<UpdateIngredientCommand>
    {
        public UpdateIngredientCommandValidation() {

            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .MaximumLength(80);
        }
    }
}
