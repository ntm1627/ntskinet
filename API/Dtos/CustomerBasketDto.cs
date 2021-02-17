using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }  //can have null
        public string ClientSecret { get; set; } //this will be availabe as back in the server as well
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}