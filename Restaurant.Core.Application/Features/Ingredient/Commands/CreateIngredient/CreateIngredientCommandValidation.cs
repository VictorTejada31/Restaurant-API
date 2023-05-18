using FluentValidation;

namespace Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient
{
    public class CreateIngredientCommandValidation : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .MaximumLength(80);
        }
    }
}
