using Common.Query;
using Shop.Domain.OrderAgg.Enum;

namespace Shop.Query.Orders.DTOs;

public class OrderFilterData : BaseDto
{
    public long UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public string? Shire { get; set; }
    public string? City { get; set; }
    public string? ShippingType { get; set; }
    public int TotalPrice { get; set; }
    public int TotalItemCount { get; set; }
}
