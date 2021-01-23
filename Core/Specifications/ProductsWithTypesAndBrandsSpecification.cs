using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        //This will bring all the includes
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
        :base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains   //|| is like or else
            (productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId==productParams.BrandId) &&   //if it doesn't have a value we bring brands that match with Id.
            (!productParams.TypeId.HasValue || x.ProductTypeId ==productParams.TypeId)        //we use the base constructor as it has a where condition to filer based on type and brand
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x =>x.Name); //By default it is ascending/alphabetical order
            ApplyPaging(productParams.PageSize*(productParams.PageIndex-1),productParams.PageSize); //This for take and skip

            if(!string.IsNullOrEmpty(productParams.sort))
            {
                switch (productParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p =>p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p =>p.Price);
                        break;
                    default:
                        AddOrderBy(n =>n.Name);
                        break;
                }
            }
        }

        //Expression<Func<Product, bool>> is replaced by id this time, 
        //the constructor of the base/BaseSpecification is also replaced with x=>x.ID
        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}