using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.SiteEntities.Sliders.Delete;

internal class DeleteSliderCommandHandler : IBaseCommandHandler<DeleteSliderCommand>
{
    private readonly ISliderRepository _repository;
    private readonly IFileService _localFileService;
    public DeleteSliderCommandHandler(ISliderRepository repository, IFileService localFileService)
    {
        _repository = repository;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(DeleteSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _repository.GetTrackingAsync(request.Id, cancellationToken);
        if (slider == null) return OperationResult.NotFound();

        _repository.Delete(slider);
        await _repository.SaveChangeAsync(cancellationToken);
        _localFileService.DeleteFile(Directories.SliderImages, slider.ImageName);
        return OperationResult.Success();
    }
}