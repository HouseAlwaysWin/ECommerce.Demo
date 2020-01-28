using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ECommerce.Domain.Entities.ImageEntity;
using ECommerce.Domain.Entities.OrderEntity;
using ECommerce.Domain.Entities.ProductEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Product> Product { get; set; }

        public virtual DbSet<ProductCategory> ProductCategory { get; set; }

        public virtual DbSet<ImageInfo> ImageInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region  Prodcut
            var product = builder.Entity<Product>();
            product.HasKey(p => p.ProductId).IsClustered();
            product.Property(p => p.Title).IsRequired();
            product.Property(p => p.Status).IsRequired();
            product.Property(p => p.EditedDate).IsRequired();
            product.Property(p => p.CreatedDate).IsRequired();
            product.HasMany(p => p.ProductSkus).WithOne(ps => ps.Product).OnDelete(DeleteBehavior.ClientSetNull);
            #endregion

            #region ProductSku
            var productSku = builder.Entity<ProductSku>();
            productSku.HasKey(ps => ps.ProductSkuId).IsClustered();
            #endregion

            #region  ProductCategory
            var productCategory = builder.Entity<ProductCategory>();
            productCategory.HasKey(c => c.ProductCategoryId).IsClustered();
            productCategory.HasAlternateKey(c => c.ChildId);
            productCategory
                    .HasMany(c => c.Childs)
                    .WithOne(c => c.Parent)
                    .HasForeignKey(c => c.ParentId)
                    .HasPrincipalKey(c => c.ChildId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            productCategory.Property(c => c.Name).IsRequired().HasMaxLength(100);
            productCategory.Property(pc => pc.IsActived).HasDefaultValue(true);
            productCategory.HasAlternateKey(c => c.ChildId);
            productCategory.Property(c => c.EditedDate).IsRequired();
            productCategory.Property(c => c.CreatedDate).IsRequired();
            #endregion

            #region productProductCategory
            var productProductCategory = builder.Entity<Product_ProductCategory>();
            productProductCategory.HasKey(pc => new { pc.ProductId, pc.ProductCategoryId }).IsClustered();
            productProductCategory
                .HasOne(pc => pc.Product)
                .WithMany(p => p.Product_ProductCategory)
                .HasForeignKey(pc => pc.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            productProductCategory
                .HasOne(pc => pc.ProductCategory)
                .WithMany(c => c.Product_ProductCategory)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            #endregion


            #region ProductImageInfoMap
            var productSkuImageInfo = builder.Entity<ProductSku_ImageInfo>();
            productSkuImageInfo.HasKey(pi => new { pi.ProductId, pi.ImageInfoId }).IsClustered();
            #endregion

            #region ImageInfo
            var imageInfo = builder.Entity<ImageInfo>();
            imageInfo.HasKey(i => i.ImageInfoId).IsClustered();
            imageInfo.Property(i => i.Url).IsRequired();
            imageInfo.Property(i => i.Type).IsRequired();
            imageInfo.Property(i => i.IsActived).HasDefaultValue(true);
            imageInfo.Property(i => i.CreatedDate).IsRequired();
            imageInfo.Property(i => i.EditedDate).IsRequired();
            #endregion

            #region Order
            var order = builder.Entity<Order>();
            order.HasKey(o => o.OrderId).IsClustered();
            order.Property(o => o.Username).IsRequired().HasMaxLength(100);
            order.Property(o => o.PhoneNumber).HasMaxLength(20);
            #endregion
        }

    }
}