using Common.Application;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.IncreaseItemCount;

public class IncreaseOrderItemCountCommandHandler : IBaseCommandHandler<IncreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _orderRepository;

    /// <summary></summary>
    /// <param name="orderRepository"></param>
    public IncreaseOrderItemCountCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(IncreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _orderRepository.GetCurrentUserOrderAsync(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();
        currentOrder.IncreaseItemCount(request.ItemId,request.Count);
        await _orderRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}