using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p =>p.Id).IsRequired();  //already a required actually
            builder.Property (p =>p.Name).IsRequired().HasMaxLength(400);
            builder.Property (p =>p.Description).IsRequired().HasMaxLength(380);
            builder.Property (p =>p.Price).HasColumnType("decimal (18,2)"); //a bit different this in sqlite
            builder.Property (p =>p.PictureUrl).IsRequired();
            builder.HasOne (b =>b.ProductBrand).WithMany()
                .HasForeignKey(p =>p.ProductBrandId);
            builder.HasOne (t =>t.ProductType).WithMany()
                .HasForeignKey (p =>p.ProductTypeId);

        }
    }
}