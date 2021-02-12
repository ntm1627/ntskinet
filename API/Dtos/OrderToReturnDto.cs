using System;
using System.Collections.Generic;
using Core.Entities.OrderAggregate;

namespace API.Dtos
{
    //This Dto fattened out put of the Order class
    public class OrderToReturnDto
    {
        public int Id {get; set;}   //we have include the It to return along with other info
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipToAddress { get; set; }  
        public string DeliveryMethod { get; set; }    //now we just return the method not the details
        public decimal ShippingPrice { get; set; }   //we have add this and pick the price from the delivery method
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }  //This is changed from OrderItem to the Dto
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }  //automapper automatically fetch this from GetTotal of orders
        public string Status { get; set; }  //we return the id than the id
    }
}