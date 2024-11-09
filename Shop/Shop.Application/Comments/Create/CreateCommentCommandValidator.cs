using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Comments.Create;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x=>x.Text)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("متن"))
            .MinimumLength(5)
            .WithMessage(ValidationMessages.maxLength("متن",5));
    }
}