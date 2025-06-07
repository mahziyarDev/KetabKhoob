using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetCurrent;
public record GetCurrentUserOrderQuery(long UserId):IBaseQuery<OrderDto?>;


public class GetCurrentUserOrderQueryHandler : IBaseQueryHandler<GetCurrentUserOrderQuery, OrderDto?>
{
     private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;

    public GetCurrentUserOrderQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }
    public async Task<OrderDto?> Handle(GetCurrentUserOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _shopContext.Orders
            .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.OrderStatus==Domain.OrderAgg.Enum.OrderStatus.Pendding, cancellationToken);
        if (order == null)
            return null;

        var orderDto = order.Map();
        orderDto.UserFullName = await _shopContext.Users.Where(f => f.Id == orderDto.UserId)
            .Select(s => $"{s.Name} {s.Family}").FirstAsync(cancellationToken);

        orderDto.Items = await orderDto.GetOrderItems(_dapperContext);
        return orderDto;
    }
}
