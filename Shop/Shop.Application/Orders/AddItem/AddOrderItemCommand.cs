using Common.Application;

namespace Shop.Application.Orders.AddItem;

/// <summary></summary>
/// <param name="InventoryId"></param>
/// <param name="Count"></param>
/// <param name="UserId"></param>
public record AddOrderItemCommand
(
    long InventoryId,
    int Count,
    long UserId
) 
: IBaseCommand;