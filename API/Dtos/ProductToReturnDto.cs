namespace API.Dtos
{
    //This is what we need to return to the client, a flat object that has the following properties
    // add Id and remove the foreign keys and change the data type of ProductType and ProductBrand make them  simple string
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }

        public string ProductBrand { get; set; }
 
    }
}