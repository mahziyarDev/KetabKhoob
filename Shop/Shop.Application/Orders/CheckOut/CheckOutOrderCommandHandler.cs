using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.CheckOut;

public class CheckOutOrderCommandHandler : IBaseCommandHandler<CheckOutOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    /// <summary></summary>
    /// <param name="orderRepository"></param>
    public CheckOutOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _orderRepository.GetCurrentUserOrderAsync(request.UserId);
        if(currentOrder == null)
            return OperationResult.NotFound();
        
        var orderAddress = new OrderAddress(request.Shire,request.City,request.PostalCode,
            request.PostalAddress,request.Name,request.Family,request.NationalCode);
        currentOrder.Checkout(orderAddress);
        await _orderRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}