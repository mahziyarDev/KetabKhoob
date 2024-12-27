using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAgg;

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