using FluentValidation;

namespace Shop.Application.Orders.IncreaseItemCount;

public class IncreaseOrderItemCountCommandValidator : AbstractValidator<IncreaseOrderItemCountCommand>
{
    public IncreaseOrderItemCountCommandValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(1)
            .WithMessage("تعداد نمی تواند کمتر از یک باشد");
    }
}