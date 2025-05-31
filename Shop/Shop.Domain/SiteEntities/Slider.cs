using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.SiteEntities;

public class Slider : BaseEntity
{
    #region Properties

    public string Title { get; private set; } = string.Empty;
    public string Link { get; private set; } = string.Empty;
    public string ImageName { get; private set; } = string.Empty;
    

    #endregion

    #region Function

    public Slider(string title,string link ,string imageName)
    {
        Guard(title ,link,imageName);
        Title = title;
        Link = link;
        ImageName = imageName;
    }
    public void Edit(string title ,string link ,string imageName)
    {
        Guard(title,link,imageName);
        Title = title;
        Link = link;
        ImageName = imageName;
    }

    private void Guard(string title, string link,string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(link, nameof(link));
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
    }
    #endregion
}