using FluentValidation;
using Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient;

namespace Restaurant.Core.Application.Features.Order.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidation : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidation()
        {
            RuleFor(x => x.TableId)
                .NotNull().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");

            RuleFor(x => x.SubTotal)
                .NotNull().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");

            RuleFor(x => x.Dishes)
                .NotNull().WithMessage("Ingredients {CollectionIndex} is required.");

        }
    }
}
