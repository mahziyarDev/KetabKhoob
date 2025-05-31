using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.Products.Create;

public class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IFileService _fileService;

    /// <summary></summary>
    /// <param name="productRepository"></param>
    /// <param name="productDomainService"></param>
    /// <param name="fileService"></param>
    public CreateProductCommandHandler(IProductRepository productRepository, IProductDomainService productDomainService,
        IFileService fileService)
    {
        _productRepository = productRepository;
        _productDomainService = productDomainService;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile,Directories.ProductImage);
        
        var product = new Product(request.Title,imageName,request.Description,
            request.CategoryId,request.SubCategoryId,request.SecondarySubCategoryId,
            request.Slug,request.SeoData,_productDomainService);
        
        await _productRepository.AddAsync(product,cancellationToken);
        
        var productSpecifications = new List<ProductSpecification>();
        request.Specifications.ToList().ForEach(x =>
        {
            productSpecifications.Add(new ProductSpecification(x.Key,x.Value));
        });
        product.SetSpecifications(productSpecifications);
        
        await _productRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}