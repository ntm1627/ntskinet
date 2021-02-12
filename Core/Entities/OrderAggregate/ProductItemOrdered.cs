namespace Core.Entities.OrderAggregate
{
    //This is just a snapshot of the product that are ordered
    //Owned entity by orderItem
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        //To pass values
        public ProductItemOrdered(int productItemId, string productName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}