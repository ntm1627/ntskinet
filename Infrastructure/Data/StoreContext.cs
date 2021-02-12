
using System;
using System.Linq;
using System.Reflection;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        //the base is passting the options to the partent class DbContext class
         public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    //we are overriding this method that is responsible for configuration using the configuration file
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //This is just to convert decimal to double as sqlite doesn't support decimal
            //The same is for the date
            if(Database.ProviderName=="Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach(var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties=entityType.ClrType.GetProperties().Where(p =>p.PropertyType
                    ==typeof(decimal));

                    var dateTimeProperties=  entityType.ClrType.GetProperties()
                          .Where(p =>p.PropertyType == typeof(DateTimeOffset));

                    foreach(var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion<double>();
                    }

                    foreach(var property in dateTimeProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                           .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}