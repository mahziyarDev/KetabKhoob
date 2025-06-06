using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.SiteEntities.Banners.Create;

public class CreateBannerCommandHandler : IBaseCommandHandler<CreateBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;
    private  readonly IFileService _fileService;

    /// <summary></summary>
    /// <param name="bannerRepository"></param>
    /// <param name="fileService"></param>
    public CreateBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile,Directories.BannerImages);
        var banner = new Domain.SiteEntities.Banner(request.Link, imageName,request.PositionBanner);
        _bannerRepository.Add(banner);
        await _bannerRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}