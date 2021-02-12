using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWithItemsAndOrderingMethodSpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsAndOrderingMethodSpecification(string email): base (o =>o.BuyerEmail==email)
        {
            AddInclude(o =>o.OrderItems);       //both OrderItems and DeliveeryMethod are related properties
            AddInclude(o =>o.DeliveryMethod);
            AddOrderByDescending(o =>o.OrderDate);
        }

        public OrdersWithItemsAndOrderingMethodSpecification(int id, string email) : base(o =>o.Id==id && o.BuyerEmail ==email)
        {
            AddInclude(o =>o.OrderItems);
            AddInclude(o =>o.DeliveryMethod);

        }
    }
}