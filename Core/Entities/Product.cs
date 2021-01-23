
namespace Core.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } //the spelling mistake releases an error
        public ProductType ProductType { get; set; }   
        public int ProductTypeId { get; set; } //Fully definition the r/p, the name ProductTypeId is just to let know EF  know this has a relationshiop with ProductType serving as Foreign Key
        public ProductBrand  ProductBrand{ get; set; }
        public int ProductBrandId { get; set; }
    }
}