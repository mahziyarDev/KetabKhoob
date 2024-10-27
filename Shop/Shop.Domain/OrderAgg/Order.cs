using Common.Domain;
using Shop.Domain.OrderAgg.Enum;
using Shop.Domain.OrderAgg.ValueObject;
using System.Reflection.Metadata.Ecma335;

namespace Shop.Domain.OrderAgg
{
    public class Order : AggregateRoot
    {
        public long UserId { get; set; }
        public OrderStatus OrderStatus { get; private set; }
        public OrderDiscount? Discount { get; private set; }
        public List<OrderItem> Items { get; private set; }

    }
    public class OrderItem : BaseEntity 
    {
        public long OrderId { get; internal set; }
    }
}
