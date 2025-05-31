using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.SiteEntities.Sliders.Edit;

/// <summary></summary>
/// <param name="Id"></param>
/// <param name="Link"></param>
/// <param name="ImageFile"></param>
/// <param name="Title"></param>
public record EditSliderCommand
    (
    long Id ,
    string Link,
    IFormFile? ImageFile,
    string Title 
    ) : IBaseCommand;