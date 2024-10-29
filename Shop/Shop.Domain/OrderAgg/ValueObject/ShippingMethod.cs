namespace Shop.Domain.OrderAgg.ValueObject;

public class ShippingMethod : Common.Domain.ValueObject
{
    public ShippingMethod(string shippingType, int shippingCost)
    {
        ShippingType = shippingType;
        ShippingCost = shippingCost;
    }

    public string ShippingType { get; private set; }
    public int ShippingCost { get; private set; }
    
}