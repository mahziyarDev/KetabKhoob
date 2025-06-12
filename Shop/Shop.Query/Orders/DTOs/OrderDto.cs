using Common.Query;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Enum;
using Shop.Domain.OrderAgg.ValueObject;

namespace Shop.Query.Orders.DTOs;

public class OrderDto : BaseDto
{
    public long UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public OrderDiscount? Discount { get; set; }
    public OrderAddress? Address { get; set; }
    public OrderShippingMethod? ShippingMethod { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public DateTime? LastUpdate { get; set; }


    public int TotalPrice
    {
        get
        {
            var total = Items.Sum(s => s.TotalPrice);
            if (Discount != null)            
                total -= Discount.DiscountAmount;
            
            total += ShippingMethod?.ShippingCost ?? 0;
            return total;
        }
    }
}
