using System.IO;
using API.Extensions;
using API.Extensions.SwaggerServiceExtensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
    
        public void ConfigureDevelopmentServices(IServiceCollection services)   //conventionally the name has to be like this
        {
            services.AddDbContext<StoreContext>(x =>
                x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppIdentityDbContext>(x =>
                 x.UseSqlite(_config.GetConnectionString("IdentityConnection"))); //separate DB for Identity

                 ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)   //conventionally the name has to be like this
        {
            services.AddDbContext<StoreContext>(x =>
                x.UseMySql(_config.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppIdentityDbContext>(x =>
                 x.UseMySql(_config.GetConnectionString("IdentityConnection"))); //separate DB for Identity

                 ConfigureServices(services);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();



            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            ;
            services.AddApplicationServices();   //a custom method
            services.AddIdentityServices(_config);     //a custom method
            services.AddSwaggerDocumentation(); //needs for the method in the extension
            services.AddCors(Option =>          //we are adding this to solve the CORS problem
            {
                Option.AddPolicy("CorsPolicy", policy =>
                 {
                     policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                 });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();  //this is our own middleware created by removing the develop Is.Dev

            //when there is a request to the end point that does not exit this  will hit our Errors class with errors route and with {0} status code
            //{0} is a place holder for the status code
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles(); //the order of this has to be just after UseRouting, this serve images and other static files
            app.UseStaticFiles(new StaticFileOptions   //this is due to the fact that we have taken images from wwwroot to API/Content
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Content")
                ),
                RequestPath = "/content"
            });

            app.UseCors("CorsPolicy");  //the origin of the header will appear in postman (Access-Control-Allow-Origin)

            app.UseAuthentication(); // this needs to come before useAuthorization 
            app.UseAuthorization();

            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //This addition end point if for angular
                endpoints.MapFallbackToController("Index", "Fallback");

            });
        }
    }
}
