﻿using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.Enum;
using Shop.Domain.OrderAgg.Events;
using Shop.Domain.OrderAgg.ValueObject;

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
            OrderStatus = OrderStatus.Pendding;
            Items = new List<OrderItem>();
        }

        public long UserId { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public OrderDiscount? Discount { get; private set; }
        public OrderAddress? Address { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public OrderShippingMethod? ShippingMethod { get; set; }
        public DateTime? LastUpdate { get; private set; }
        public int TotalPrice
        {
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
            ChangeOrderGuard();
            if (OrderStatus == OrderStatus.Pendding)
                throw new InvalidDomainDataException("امکان ثبت محصول در این سفارش وجود ندارد");

            var oldItem = Items.FirstOrDefault(x => x.InventoryId == item.InventoryId);
            if (oldItem != null)
            {
                oldItem.ChangeCount(oldItem.Count + item.Count);
                return;
            }
            Items.Add(item);
        }

        public void RemoveItem(long orderItemId)
        {
            ChangeOrderGuard();
            var currentItem = Items.FirstOrDefault(x => x.Id == orderItemId);
            if (currentItem == null) throw new NullOrEmptyDomainDataException("چیزی برای حذف یافت نشد");
            Items.Remove(currentItem);
        }
        public void ChangeCountItem(long orderItemId, int newCount)
        {
            ChangeOrderGuard();
            var currentItem = Items.FirstOrDefault(x => x.Id == orderItemId);
            if (currentItem == null) throw new NullOrEmptyDomainDataException("چیزی یافت نشد");
            currentItem.ChangeCount(newCount);
        }

        public void Finally()
        {
            OrderStatus = OrderStatus.Finally;
            LastUpdate = DateTime.Now;
            AddDomainEvent(new OrderFinalized(Id));
        }
        public void IncreaseItemCount(long itemId, int count)
        {
            ChangeOrderGuard();
            var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
            if (currentItem == null) throw new NullOrEmptyDomainDataException("چیزی یافت نشد");
            currentItem.IncreaseCount(count);
        }
        public void DecreaseItemCount(long itemId, int count)
        {
            ChangeOrderGuard();
            var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
            if (currentItem == null) throw new NullOrEmptyDomainDataException("چیزی یافت نشد");
            currentItem.DecreaseCount(count);
        }
        public void ChangeStatus(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
            LastUpdate = DateTime.Now;
        }

        public void Checkout(OrderAddress orderAddress)
        {
            ChangeOrderGuard();
            Address = orderAddress;
            OrderStatus = OrderStatus.Finally;
        }

        private void ChangeOrderGuard()
        {
            if (OrderStatus != OrderStatus.Pendding)
                throw new InvalidDomainDataException("امکان ویرایش این سفارش وجود ندارد.");
        }
    }
}
