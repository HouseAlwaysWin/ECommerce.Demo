using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ECommerce.Domain.Entities.ImageEntity;
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
            product.Property(p => p.Price).IsRequired();
            product.Property(p => p.EditedDate).IsRequired();
            product.Property(p => p.CreatedDate).IsRequired();
            #endregion

            #region  ProductCategory
            var productCategory = builder.Entity<ProductCategory>();
            productCategory.HasKey(c => c.ProductCategoryId).IsClustered();
            productCategory.HasAlternateKey(c => c.Guid);
            productCategory.HasMany(c => c.Childs).WithOne(c => c.Parent)
                    .HasForeignKey(c => c.ParentId).HasPrincipalKey(c => c.Guid).OnDelete(DeleteBehavior.ClientSetNull);
            productCategory.Property(c => c.Name).IsRequired().HasMaxLength(100);
            productCategory.HasAlternateKey(c => c.Guid);
            productCategory.Property(c => c.EditedDate).IsRequired();
            productCategory.Property(c => c.CreatedDate).IsRequired();
            #endregion

            #region ProductCategoryMap
            var productCategoryMap = builder.Entity<ProductCategoryMap>();
            productCategoryMap.HasKey(pc => new { pc.ProductId, pc.ProductCategoryId });
            productCategoryMap
                .HasOne(pc => pc.Product)
                .WithMany(c => c.ProductCategoryMaps)
                .HasForeignKey(p => p.ProductCategoryId).OnDelete(DeleteBehavior.ClientSetNull);
            productCategoryMap
                .HasOne(pc => pc.ProductCategory)
                .WithMany(p => p.ProductCategoryMaps)
                .HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.ClientSetNull);
            #endregion

            #region ImageInfo
            var imageInfo = builder.Entity<ImageInfo>();
            imageInfo.HasKey(i => i.ID);

            #endregion
        }

    }
}