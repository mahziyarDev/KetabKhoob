using Common.Domain.Repository;

namespace Shop.Domain.OrderAgg.Repository;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order?> GetCurrentUserOrderAsync(long userId);
}