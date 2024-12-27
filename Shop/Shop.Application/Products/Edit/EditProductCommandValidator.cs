using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using Shop.Application.Products.Create;

namespace Shop.Application.Products.Edit;

public class EditProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(ValidationMessages.required("عنوان"));
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(ValidationMessages.required("توضیحات"));

        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithMessage(ValidationMessages.required("اسلاگ"));
        
        RuleFor(x => x.ImageFile)
            .JustImageFile();
    }
}