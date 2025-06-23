using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.SendOrder;

public class SendOrderCommand : IBaseCommand
{
    public SendOrderCommand(long orderId)
    {
        OrderId = orderId;
    }

    public long OrderId { get; private set; }
}

public class SendOrderCommandHandler : IBaseCommandHandler<SendOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public SendOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(SendOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetTrackingAsync(request.OrderId, cancellationToken);
        if(order==null)
            return OperationResult.NotFound();

        order.ChangeStatus(Domain.OrderAgg.Enum.OrderStatus.Shipping);
        await _orderRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}