using System;
using System.Collections.Generic;

namespace Core.Entities.OrderAggregate
{
    public class Order: BaseEntity
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderItem> orderItems,string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public Address ShipToAddress { get; set; }  //address is owned by Order and doesn't need include
        public DeliveryMethod DeliveryMethod { get; set; }     //DeliveryMethod is a related property and needs to be included for eger loading
        public IReadOnlyList<OrderItem> OrderItems { get; set; }  //A single order may have many order items,1-m, a related property, needs to be included
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }
        public decimal GetTotal ()    //automapper automaticall provide this to any get calls that request for total
        {
            return Subtotal + DeliveryMethod.Price;

        }
        

        }
    }

