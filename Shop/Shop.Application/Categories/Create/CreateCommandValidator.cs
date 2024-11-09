using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.Create;

public class CreateCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(x => x.Slug)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("اسلاگ"));
    }
}