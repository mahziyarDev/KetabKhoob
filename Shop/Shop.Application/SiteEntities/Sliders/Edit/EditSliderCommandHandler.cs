using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.SiteEntities.Sliders.Edit;

internal class EditSliderCommandHandler : IBaseCommandHandler<EditSliderCommand>
{
    private readonly IFileService _fileService;
    private readonly ISliderRepository _sliderRepository;
    /// <summary></summary>
    /// <param name="fileService"></param>
    /// <param name="repository"></param>
    public EditSliderCommandHandler(IFileService fileService, ISliderRepository sliderRepository)
    {
        _fileService = fileService;
        _sliderRepository = sliderRepository;
    }
    public async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetTracking(request.Id,cancellationToken);
        if (slider == null)
            return OperationResult.NotFound();
        var imageName = slider.ImageName;
        var oldImage = slider.ImageName;
        if (request.ImageFile != null)
        {
            imageName = await _fileService
                .SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);
            _fileService.DeleteFile(Directories.SliderImages, oldImage);
        }

        slider.Edit(request.Title, request.Link, imageName);

        await _sliderRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }

   
}