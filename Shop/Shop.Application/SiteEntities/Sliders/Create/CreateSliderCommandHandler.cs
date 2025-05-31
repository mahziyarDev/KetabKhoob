using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.SiteEntities.Sliders.Create;

internal class CreateSliderCommandHandler : IBaseCommandHandler<CreateSliderCommand>
{
    private readonly IFileService _fileService;
    private readonly ISliderRepository _sliderRepository;
    /// <summary></summary>
    /// <param name="fileService"></param>
    /// <param name="repository"></param>
    public CreateSliderCommandHandler(IFileService fileService, ISliderRepository repository)
    {
        _fileService = fileService;
        _sliderRepository = repository;
    }

    public async Task<OperationResult> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService
            .SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);
        var slider = new Slider(request.Title,request.Link,imageName);

        _sliderRepository.Add(slider,cancellationToken);
        await _sliderRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}