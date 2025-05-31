using Common.Application;

namespace Shop.Application.Sellers.EditInventory;

/// <summary></summary>
/// <param name="SellerId"></param>
/// <param name="InventoryId"></param>
/// <param name="Count"></param>
/// <param name="Price"></param>
/// <param name="DiscountPercentage"></param>
public record EditInventoryCommand
(
    long SellerId,
    long InventoryId,
    int Count ,
    int Price,
    int? DiscountPercentage  
)
: IBaseCommand;