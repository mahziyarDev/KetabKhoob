using Common.Application;

namespace Shop.Application.Orders.DecreaseItemCount;

/// <summary></summary>
/// <param name="UserId"></param>
/// <param name="ItemId"></param>
/// <param name="Count"></param>
public record DecreaseOrderItemCountCommand 
(
    long UserId,
    long ItemId,
    int Count
): IBaseCommand;