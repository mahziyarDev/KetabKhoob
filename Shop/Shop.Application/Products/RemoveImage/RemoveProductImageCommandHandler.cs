using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application.Products.AddImage;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.Products.RemoveImage;

public class RemoveProductImageCommandHandler : IBaseCommandHandler<RemoveProductImageCommand>
{
    private readonly IFileService _fileService;
    private readonly IProductRepository _productRepository;

    /// <summary></summary>
    /// <param name="fileService"></param>
    /// <param name="productRepository"></param>
    public RemoveProductImageCommandHandler(IFileService fileService, IProductRepository productRepository)
    {
        _fileService = fileService;
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(RemoveProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetTrackingAsync(request.ProductId,cancellationToken);
        if (product == null) return OperationResult.NotFound();

        var imageName = product.RemoveImage(request.ImageId);
        await _productRepository.SaveChangeAsync(cancellationToken);
        _fileService.DeleteFile(Directories.ProductGalleryImage, imageName);
        return OperationResult.Success();
    }
}