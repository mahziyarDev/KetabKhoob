using Common.Application;

namespace Shop.Application.Orders.IncreaseItemCount;

/// <summary></summary>
/// <param name="UserId"></param>
/// <param name="ItemId"></param>
/// <param name="Count"></param>
public record IncreaseOrderItemCountCommand 
(
    long UserId,
    long ItemId,
    int Count
): IBaseCommand;