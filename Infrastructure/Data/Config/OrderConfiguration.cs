using System;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    //IEntityTypeConfiguration<Order> Allows configuration for an entity type to be factored into 
    //a separate class, rather than in-line in OnModelCreating(ModelBuilder)
    //Good also the public changed with internal as it will be accessed only in DbContext 
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Only some of the propertties are configured

            builder.OwnsOne(o =>o.ShipToAddress, a => {a.WithOwner ();});   
         //   builder.OwnsOne(typeof(Address),"ShipToAddress");  //from https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities
            builder.Property(s =>s.Status)
                .HasConversion(
                    o =>o.ToString(),
                    o =>(OrderStatus) Enum.Parse(typeof(OrderStatus),o)
                 );
            builder.HasMany(o =>o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade); //when order deleted items will be deleted
        }
    }
}