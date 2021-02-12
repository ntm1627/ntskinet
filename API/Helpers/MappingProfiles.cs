using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    //This class is just to avoid the problem in mapping of ProductBrand and ProductType as there data type b/n the two classes is different
    //along with the image url
    public class MappingProfiles : Profile
    {
        // d and s refers to destination and source and they help to configure some properties which don't display the result in the required output format
        public MappingProfiles()
        {

            //Mapper.CreateMap<Source, Dest>()
            //  .ForMember(d => d.Foo, opt => opt.MapFrom(src => src.Foo)); o is for option

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d =>d.ProductBrand, o =>o.MapFrom(s =>s.ProductBrand.Name)) //d is destination and s source look for definition of the method ForMember 
                .ForMember(d =>d.ProductType, o =>o.MapFrom(s =>s.ProductType.Name))
                .ForMember (d =>d.PictureUrl, o =>o.MapFrom<ProductUrlResolver>());
            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();   //For identity and serve both ways
            CreateMap<CustomerBasketDto,CustomerBasket>();
            CreateMap<BasketItemDto,BasketItem>();
            CreateMap<AddressDto,Core.Entities.OrderAggregate.Address>(); //this one is for order
            CreateMap<Order, OrderToReturnDto>() //Return the results
                .ForMember(d =>d.DeliveryMethod, o =>o.MapFrom(s =>s.DeliveryMethod.ShortName))
                .ForMember(d =>d.ShippingPrice, o =>o.MapFrom(s =>s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>() //Return the results
                .ForMember(d =>d.ProductId, o =>o.MapFrom(s =>s.ItemOrdered.ProductItemId))
                .ForMember(d =>d.ProductName, o =>o.MapFrom(s =>s.ItemOrdered.ProductName))
                .ForMember(d =>d.PictureUrl, o =>o.MapFrom(s =>s.ItemOrdered.PictureUrl))
                .ForMember(d =>d.PictureUrl, o =>o.MapFrom<OrderItemUrlResolver>()); // this is to include http://localhost:5001 in the pictureUrl
        }
    }
}