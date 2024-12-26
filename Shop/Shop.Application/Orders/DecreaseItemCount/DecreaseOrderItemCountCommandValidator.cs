using FluentValidation;
using Shop.Application.Orders.IncreaseItemCount;

namespace Shop.Application.Orders.DecreaseItemCount;

public class DecreaseOrderItemCountCommandValidator : AbstractValidator<IncreaseOrderItemCountCommand>
{
    public DecreaseOrderItemCountCommandValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(1)
            .WithMessage("تعداد نمی تواند کمتر از یک باشد");
    }
}