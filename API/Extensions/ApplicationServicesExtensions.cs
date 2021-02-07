using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository,BasketRepository>(); 
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>))); //since we are using the generic class interface it is different from the norma
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                  {
                      var errors = actionContext.ModelState
                             .Where(e => e.Value.Errors.Count > 0)
                             .SelectMany(x => x.Value.Errors)
                             .Select(x => x.ErrorMessage).ToArray();

                      var errorResponse = new ApiValidationErrorResponse
                      {
                          Errors = errors
                      };

                      return new BadRequestObjectResult(errorResponse);

                  };

            });

            return services;
        }
    }
}