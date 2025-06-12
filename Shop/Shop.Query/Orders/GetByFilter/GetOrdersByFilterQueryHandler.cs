using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetByFilter;

internal class GetOrdersByFilterQueryHandler : IBaseQueryHandler<GetOrdersByFilterQuery, OrderFilterResult>
{
    private readonly ShopContext _context;

    public GetOrdersByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<OrderFilterResult> Handle(GetOrdersByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _context.Orders
            
            .OrderByDescending(d => d.Id).AsQueryable();

        if (@params.Status != null)
            result = result.Where(r => r.OrderStatus == @params.Status);

        if (@params.UserId != null)
            result = result.Where(r => r.UserId == @params.UserId);

        if (@params.StartDate != null)
            result = result.Where(r => r.CreationDate.Date >= @params.StartDate.Value.Date);

        if (@params.EndDate != null)
            result = result.Where(r => r.CreationDate.Date <= @params.EndDate.Value.Date);

        
        var skip = (@params.PageId - 1) * @params.Take;
        //دریافت کاربر های مربوط به هرسفارش
        var userId = result.Skip(skip).Take(@params.Take).Select(x => x.UserId).ToList();
        var userInfo = _context.Users
            .Where(x=>userId.Contains(x.Id))
            .Select(x=>new UserInfoDto
            {
                FullName = x.Name + " " + x.Family,
                UserId = x.Id
            }).ToList();
        var model = new OrderFilterResult()
        {
            Data = await result.Skip(skip).Take(@params.Take)
                .Select(order =>order
                .MapFilterData(userInfo.First(x=>x.UserId == order.UserId).FullName??""))
                .ToListAsync(cancellationToken),
            FilterParams = @params
        };
        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}