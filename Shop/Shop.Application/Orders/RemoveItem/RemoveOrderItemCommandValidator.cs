using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Orders.RemoveItem;

public class RemoveOrderItemCommandValidator :  AbstractValidator<RemoveOrderItemCommand>
{
    public RemoveOrderItemCommandValidator()
    {
        RuleFor(x => x.ItemId)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("محصول"));
    }
}