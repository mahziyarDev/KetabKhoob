using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.Products.AddImage;

public class AddProductImageCommandHandler : IBaseCommandHandler<AddProductImageCommand>
{
    private readonly IFileService _fileService;
    private readonly IProductRepository _productRepository;

    /// <summary></summary>
    /// <param name="fileService"></param>
    /// <param name="productRepository"></param>
    public AddProductImageCommandHandler(IFileService fileService, IProductRepository productRepository)
    {
        _fileService = fileService;
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetTrackingAsync(request.ProductId,cancellationToken);
        if (product == null) return OperationResult.NotFound();

        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleryImage);
        var productImage = new ProductImage(imageName, request.Sequence);
        product.AddImage(productImage);
        await _productRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}