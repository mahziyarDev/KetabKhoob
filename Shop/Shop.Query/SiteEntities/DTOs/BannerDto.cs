using Common.Query;
using Shop.Domain.SiteEntities;

namespace Shop.Query.SiteEntities.DTOs;

public class BannerDto : BaseDto
{
    public string Link { get; set; }
    public string ImageName { get; set; }
    public PositionBanner Position { get; set; }
}
