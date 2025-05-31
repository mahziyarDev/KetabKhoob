using Common.Application;

namespace Shop.Application.Sellers.AddInventory;

/// <summary></summary>
/// <param name="SellerId"></param>
/// <param name="ProductId"></param>
/// <param name="Count"></param>
/// <param name="Price"></param>
/// <param name="PercentageDiscount"></param>
public record AddInventoryCommand
(long SellerId,
long ProductId,
int Count ,
int Price,
int? DiscountPercentage
)
: IBaseCommand;