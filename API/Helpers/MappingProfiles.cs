using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    //This class is just to avoid the problem in mapping of ProductBrand and ProductType as there data type b/n the two classes is different
    //along with the image url
    public class MappingProfiles : Profile
    {
        
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d =>d.ProductBrand, o =>o.MapFrom(s =>s.ProductBrand.Name)) //d is destination and s source look for definition of the method ForMember 
                .ForMember(d =>d.ProductType, o =>o.MapFrom(s =>s.ProductType.Name))
                .ForMember (d =>d.PictureUrl, o =>o.MapFrom<ProductUrlResolver>());

        }
    }
}