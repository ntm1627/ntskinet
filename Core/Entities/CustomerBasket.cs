using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
        }
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>(); //initialize the basket with an empty list of items
        public int? DeliveryMethodId { get; set; }  //can have null
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
         public decimal ShippingPrice { get; set; }
    }
}