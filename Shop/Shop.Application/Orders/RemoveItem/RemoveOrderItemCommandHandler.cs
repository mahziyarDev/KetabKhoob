using Common.Application;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.RemoveItem;

public class RemoveOrderItemCommandHandler : IBaseCommandHandler<RemoveOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderRepository"></param>
    public RemoveOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _orderRepository.GetCurrentUserOrderAsync(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();
        
        currentOrder.RemoveItem(request.ItemId);
        await _orderRepository.SaveChangeAsync();
        return OperationResult.Success();
    }
}