using ECommerce.Demo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Demo.Infrastructure.Configs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property (p => p.Id).IsRequired ();
            builder.Property (p => p.Name).IsRequired ().HasMaxLength (100);
            builder.Property (p => p.Description).IsRequired ();
            builder.HasMany (p => p.Prices)
                .WithOne().HasForeignKey(p => p.Id);
            builder.Property (p => p.PictureUrl).IsRequired ();
            builder.HasOne (b => b.ProductBrand).WithMany ()
                .HasForeignKey (p => p.ProductBrandId);
            builder.HasOne (b => b.ProductType).WithMany ()
                .HasForeignKey (p => p.ProductTypeId);
        }
    }
}