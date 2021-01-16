
namespace Core.Entities
{
    public class Product
    {
        public int Id { get; set; } //by EF convention this this the primary key and it is also  identity key but can be changed
        public string Name { get; set; }
    }
}