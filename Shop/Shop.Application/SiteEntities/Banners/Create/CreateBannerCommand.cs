using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;

namespace Shop.Application.SiteEntities.Banners.Create;

/// <summary></summary>
/// <param name="Link"></param>
/// <param name="ImageFile"></param>
/// <param name="PositionBanner"></param>
public record CreateBannerCommand
(
string Link ,
IFormFile ImageFile ,
PositionBanner PositionBanner
):IBaseCommand;