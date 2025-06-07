namespace Shop.Domain.OrderAgg.ValueObject;

public class OrderShippingMethod : Common.Domain.ValueObject
{
    public OrderShippingMethod(string shippingType, int shippingCost)
    {
        ShippingType = shippingType;
        ShippingCost = shippingCost;
    }

    public string ShippingType { get; private set; }
    public int ShippingCost { get; private set; }
    
}