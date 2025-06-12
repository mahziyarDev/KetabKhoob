using Common.Query;

namespace Shop.Query.Orders.DTOs;

public class OrderItemDto : BaseDto
{
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductSlug { get; set; } = string.Empty;
    public string ProductImageName { get; set; } = string.Empty;
    public string ShopName { get; set; } = string.Empty;
    public long OrderId { get; set; }
    public long InventoryId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int TotalPrice => Price * Count;
}
