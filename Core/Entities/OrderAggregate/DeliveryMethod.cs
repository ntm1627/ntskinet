namespace Core.Entities.OrderAggregate
{
    //This is just for the users to select the delivery methods and will have a table
    public class DeliveryMethod: BaseEntity
    {
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
    }
}