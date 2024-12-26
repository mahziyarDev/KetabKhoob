using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using Common.Domain.ValueObjects;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Application.Products.Create;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.Products.Edit;

/// <summary></summary>
/// <param name="ProductId"></param>
/// <param name="Title"></param>
/// <param name="ImageFile"></param>
/// <param name="Description"></param>
/// <param name="CategoryId"></param>
/// <param name="SubCategoryId"></param>
/// <param name="SecondarySubCategoryId"></param>
/// <param name="Slug"></param>
/// <param name="SeoData"></param>
/// <param name="Specifications"></param>
public record EditProductCommand
(
    long ProductId,
    string Title ,
    IFormFile? ImageFile ,
    string Description ,
    long CategoryId ,
    long SubCategoryId ,
    long SecondarySubCategoryId ,
    string Slug ,
    SeoData SeoData ,
    List<ProductSpecification> Specifications 
)
    : IBaseCommand;

public class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IFileService _fileService;

    /// <summary></summary>
    /// <param name="productRepository"></param>
    /// <param name="productDomainService"></param>
    /// <param name="fileService"></param>
    public EditProductCommandHandler(IProductRepository productRepository, IProductDomainService productDomainService, IFileService fileService)
    {
        _productRepository = productRepository;
        _productDomainService = productDomainService;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var currentProduct = await _productRepository.GetAsync(request.ProductId);
        if(currentProduct == null) return OperationResult.NotFound();
        
        currentProduct.Edit(request.Title,request.Description,request.CategoryId,
            request.SubCategoryId,request.SecondarySubCategoryId,request.Slug,request.SeoData,_productDomainService);
        
        if (request.ImageFile != null)
        {
            _fileService.DeleteFile(Directories.ProductImage, currentProduct.ImageName);
            var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile,Directories.ProductImage);
            currentProduct.SetImageName(imageName);   
        }
        var productSpecifications = new List<ProductSpecification>();
        request.Specifications.ToList().ForEach(x =>
        {
            productSpecifications.Add(new ProductSpecification(x.Key,x.Value));
        });
        currentProduct.SetSpecifications(productSpecifications);
        
        await _productRepository.SaveChangeAsync();
        return OperationResult.Success();
    }
}

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