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
        public int TotalPrice => Items.Sum(x => x.TotalPrice);
        public int ItemCount => Items.Count();


    }
    public class OrderItem : BaseEntity
    {
        public OrderItem(long inventoryId, int count, int price)
        {
            InventoryId = inventoryId;
            Count = count;
            Price = price;
        }

        public long OrderId { get; internal set; }
        public long InventoryId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public int TotalPrice => TotalPrice * Price;
        
        public void ChangeCount(int newCount)
        {
            if (newCount < 1)
                return;
            Count = newCount;
        }

    }
}
