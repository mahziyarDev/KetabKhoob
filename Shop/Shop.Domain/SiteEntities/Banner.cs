using System.ComponentModel.DataAnnotations;
using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.SiteEntities;

public class Banner : BaseEntity
{
    #region Properties

    public string Link { get; private set; }
    public string ImageName { get; private set; }
    public PositionBanner PositionBanner { get; set; }

    #endregion

    #region Function

    public Banner(string link ,string imageName ,PositionBanner positionBanner)
    {
        Guard(link,imageName);
        Link = link;
        ImageName = imageName;
        PositionBanner = positionBanner;
    }
    public void Edit(string link ,string imageName ,PositionBanner positionBanner)
    {
        Guard(link,imageName);
        Link = link;
        ImageName = imageName;
        PositionBanner = positionBanner;
    }

    private void Guard(string link,string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(link, nameof(link));
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
    }
    #endregion
}

public enum PositionBanner
{
    [Display(Name = "سمت راست اسلایدر")]
    RightSideOfTheSlider,
    [Display(Name = "زیر اسلایدر")]
    UnderTheSlider,
}