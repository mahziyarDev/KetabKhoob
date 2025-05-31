using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.SiteEntities.Banners.Edit;

public class EditBannerCommandHandler : IBaseCommandHandler<EditBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;
    private  readonly IFileService _fileService;

    /// <summary></summary>
    /// <param name="bannerRepository"></param>
    /// <param name="fileService"></param>
    public EditBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(EditBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _bannerRepository.GetTracking(request.Id, cancellationToken);
        if(banner == null) return OperationResult.NotFound();
        
        var imageName = banner.ImageName;
        
        var oldImage = banner.ImageName;
        
        if (request.ImageFile != null)
        {
            imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile,Directories.BannerImages);
            _fileService.DeleteFile(Directories.BannerImages, oldImage);   
        }
        banner.Edit(request.Link,imageName,request.PositionBanner);
        await _bannerRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
   
}