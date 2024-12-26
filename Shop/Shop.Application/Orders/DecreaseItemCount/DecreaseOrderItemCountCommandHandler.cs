using Common.Application;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.DecreaseItemCount;

public class DecreaseOrderItemCountCommandHandler : IBaseCommandHandler<IncreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _orderRepository;

    /// <summary></summary>
    /// <param name="orderRepository"></param>
    public DecreaseOrderItemCountCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(IncreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _orderRepository.GetCurrentUserOrderAsync(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();
        currentOrder.DecreaseItemCount(request.ItemId,request.Count);
        await _orderRepository.SaveChangeAsync();
        return OperationResult.Success();
    }
}