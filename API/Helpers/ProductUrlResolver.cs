using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    //This classess  are to help automapper to include the complete url for the pictures, by default he automapper doesn't include the 
    //https://localhost:5001 during mapping
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;

        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember,
        ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl; //conactenate the one that we defined in appsettings.Development
            }

            return null;
        }
    }
}