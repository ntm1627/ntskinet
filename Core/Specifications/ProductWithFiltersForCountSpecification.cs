using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
              : base(x =>
                    (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
                    (productParams.Search)) &&
                    (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&   //if it doesn't have a value we bring brands that match with Id.
                    (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)        //we use the base constructor as it has a where condition to filer based on type and brand
            )
        {

        }
    }
}