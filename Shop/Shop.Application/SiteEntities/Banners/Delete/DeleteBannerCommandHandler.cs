using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.SiteEntities.Banners.Delete;

internal class DeleteBannerCommandHandler : IBaseCommandHandler<DeleteBannerCommand>
{
    private readonly IBannerRepository _repository;
    private readonly IFileService _localFileService;
    public DeleteBannerCommandHandler(IBannerRepository repository, IFileService localFileService)
    {
        _repository = repository;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _repository.GetTrackingAsync(request.Id,cancellationToken);
        if (banner == null)
            return OperationResult.NotFound();

        _repository.Delete(banner);
        await _repository.SaveChangeAsync(cancellationToken);
        _localFileService.DeleteFile(Directories.BannerImages, banner.ImageName);
        return OperationResult.Success();
    }
}