using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Comments.Edit;

public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
{
    public EditCommentCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x=>x.Text)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("متن"))
            .MinimumLength(5)
            .WithMessage(ValidationMessages.maxLength("متن",5));
    }
}