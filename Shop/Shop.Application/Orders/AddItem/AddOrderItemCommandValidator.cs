using FluentValidation;

namespace Shop.Application.Orders.AddItem;

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(1)
            .WithMessage("تعداد نمی تواند کمتر از یک باشد");
        
    }
}