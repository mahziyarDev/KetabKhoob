using Common.Domain;
using Shop.Domain.OrderAgg.Enum;
using Shop.Domain.OrderAgg.ValueObject;
using System.Reflection.Metadata.Ecma335;
using Common.Domain.Exceptions;

namespace Shop.Domain.OrderAgg
{
    public class Order : AggregateRoot
    {
        private Order()
        {
            
        }
        public Order(long userId)
        {
            UserId = userId;
            OrderStatus = OrderStatus.Pennding;
            Items = new List<OrderItem>();
        }

        public long UserId { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public OrderDiscount? Discount { get; private set; }
        public OrderAddress? Address { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public ShippingMethod? ShippingMethod { get; set; }
        public DateTime? LastUpdate { get; private set; }
        public int TotalPrice {
            get
            {
                var totalPrice = Items.Sum(x => x.TotalPrice);
                if (ShippingMethod != null)
                    totalPrice += this.ShippingMethod.ShippingCost;
                if (Discount != null)
                    totalPrice -= Discount.DiscountAmount;
                return totalPrice;
            }
            
        }
    
        public int ItemCount => Items.Count();

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(long orderItemId)
        {
            var currentItem = Items.FirstOrDefault(x => x.Id == orderItemId);
            if(currentItem == null) throw new NullOrEmptyDomainDataException("چیزی برای حذف یافت نشد");
            Items.Remove(currentItem);
        }
        public void ChangeCountItem(long orderItemId , int newCount)
        {
            var currentItem = Items.FirstOrDefault(x => x.Id == orderItemId);
            if(currentItem == null) throw new NullOrEmptyDomainDataException("چیزی یافت نشد");
            currentItem.ChangeCount(newCount);
        }

        public void ChangeStatus(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
            LastUpdate = DateTime.Now;
        }

        public void Checkout(OrderAddress orderAddress)
        {
            Address = orderAddress;
            OrderStatus = OrderStatus.Finally;
        }
    }
}
