namespace API.Dtos
{
    //This is a Dto serve to return the result of the Ordered items using OrderItem
    public class OrderItemDto
    {
        public int ProductId { get; set; }  //The ProductItemOrdered properties are flattened
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}