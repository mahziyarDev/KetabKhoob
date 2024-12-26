using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Orders.AddItem;

public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISellerRepository _sellerRepository;
    /// <summary></summary>
    /// <param name="orderRepository"></param>
    /// <param name="sellerRepository"></param>
    public AddOrderItemCommandHandler(IOrderRepository orderRepository, ISellerRepository sellerRepository)
    {
        _orderRepository = orderRepository;
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _sellerRepository.GetInventoryByIdAsync(request.InventoryId);
        if (inventory == null)
            return OperationResult.NotFound();

        if (inventory.Count < request.Count)
            return OperationResult.Error("تعداد محثولات موجود در انبار کمتر از تعداد درخواست شماست");
        
        //اگر مقدار order برابر با null بود مقدار جدیدی را new میکند وبه order میدهد
        var order = await _orderRepository.GetCurrentUserOrderAsync(request.UserId) ?? new Order(request.UserId);
        
        order.AddItem(new OrderItem(inventory.ProductId,request.Count,inventory.Price));
        
        if (ItemCountBeggreThanInventoryCount(inventory,order))
            return OperationResult.Error("تعداد محثولات موجود در انبار کمتر از تعداد درخواست شماست");
        
        await _orderRepository.SaveChangeAsync();
        return OperationResult.Success();
    }

    private bool ItemCountBeggreThanInventoryCount(InventoryResult inventory, Order order)
    {
        var orderItem = order.Items.FirstOrDefault(x=>x.Id == inventory.Id);
        if (orderItem.Count > inventory.Count)
            return true;
        return false;
    }
}