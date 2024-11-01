using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.ProductAgg;

public class ProductImage : BaseEntity
{
    
    #region Properties
    public long ProductId { get; internal set; }
    public string ImageName { get; private set; }
    public int Sequence { get; private set; }
    #endregion

    #region Function
    
    public ProductImage(string imageName, int sequence)
    {
        NullOrEmptyDomainDataException.CheckString(imageName,nameof(imageName));
        ImageName = imageName;
        Sequence = sequence;
    }

    #endregion
}