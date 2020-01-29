using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ECommerce.Domain.Entities.FaqEntity;
using ECommerce.Domain.Entities.ImageEntity;
using ECommerce.Domain.Entities.NotificationEntity;
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
            product.HasAlternateKey(p => p.ProductAkId);
            product.Property(p => p.Title).IsRequired();
            product.Property(p => p.Status)
                .IsRequired()
                .HasDefaultValue(ProductStatus.ForSale);
            product.Property(p => p.IsActived)
                .IsRequired()
                .HasDefaultValue(true);
            product.Property(p => p.EditedDate)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow);
            product.Property(p => p.CreatedDate)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow);
            product.HasMany(p => p.ProductSkus)
                   .WithOne(ps => ps.Product)
                   .HasPrincipalKey(p => p.ProductAkId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            product.HasMany(p => p.OrderItems)
                    .WithOne(oi => oi.Product)
                    .HasForeignKey(oi => oi.OrderItemAkId)
                    .HasPrincipalKey(p => p.ProductAkId);
            #endregion

            #region ProductSku
            var productSku = builder.Entity<ProductSku>();
            productSku.HasKey(ps => ps.ProductSkuId).IsClustered();
            productSku.HasAlternateKey(ps => ps.ProductSkuAkId);
            productSku.Property(ps => ps.StockQuantity).IsRequired();
            productSku.Property(ps => ps.IsActived).HasDefaultValue(true);
            productSku.Property(ps => ps.Price).IsRequired();
            productSku.Property(ps => ps.CreatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            productSku.Property(ps => ps.EditedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            productSku
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductSkus)
                .HasForeignKey(ps => ps.ProductSkuAkId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            productSku
                .HasMany(ps => ps.ImageInfos)
                .WithOne(i => i.ProductSku)
                .HasPrincipalKey(ps => ps.ProductSkuAkId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            #endregion

            #region  ProductCategory
            var productCategory = builder.Entity<ProductCategory>();
            productCategory.HasKey(c => c.ProductCategoryId).IsClustered();
            productCategory.HasAlternateKey(c => c.ProductCategoryAkId);
            productCategory
                    .HasMany(pc => pc.Childs)
                    .WithOne(pc => pc.Parent)
                    .HasForeignKey(pc => pc.ParentId)
                    .HasPrincipalKey(pc => pc.ProductCategoryAkId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            productCategory.Property(c => c.Name).IsRequired().HasMaxLength(100);
            productCategory.Property(pc => pc.IsActived).HasDefaultValue(true);
            productCategory.Property(c => c.EditedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            productCategory.Property(c => c.CreatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            #endregion

            #region Product_ProductCategory
            var productProductCategory = builder.Entity<Product_ProductCategory>();
            productProductCategory.HasKey(ppc => new { ppc.ProductAkId, ppc.ProductCategoryAkId }).IsClustered();
            productProductCategory
                .HasOne(ppc => ppc.Product)
                .WithMany(p => p.Product_ProductCategory)
                .HasForeignKey(ppc => ppc.ProductAkId);
            productProductCategory
                .HasOne(ppc => ppc.ProductCategory)
                .WithMany(pc => pc.Product_ProductCategory)
                .HasForeignKey(ppc => ppc.ProductCategoryAkId);
            #endregion

            #region ImageInfo
            var imageInfo = builder.Entity<ImageInfo>();
            imageInfo.HasKey(i => i.ImageInfoId).IsClustered();
            imageInfo.HasAlternateKey(i => i.ImageInfoAKId);
            imageInfo.Property(i => i.Url).IsRequired();
            imageInfo.Property(i => i.Type).IsRequired();
            imageInfo.Property(i => i.IsActived).HasDefaultValue(true);
            imageInfo.Property(i => i.CreatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            imageInfo.Property(i => i.EditedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            imageInfo
                .HasOne(i => i.ProductSku)
                .WithMany(p => p.ImageInfos)
                .HasForeignKey(i => i.ImageInfoAKId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            #endregion

            #region Order
            var order = builder.Entity<Order>();
            order.HasKey(o => o.OrderId).IsClustered();
            order.HasAlternateKey(o => o.OrderAkId);
            order.Property(o => o.Username).IsRequired().HasMaxLength(100);
            order.Property(o => o.PhoneNumber).HasMaxLength(20);
            order.Property(o => o.MobileNumber).HasMaxLength(20);
            order.Property(o => o.Email).IsRequired();
            order.Property(o => o.TotalAmount).IsRequired();
            order.Property(o => o.Tax).IsRequired();
            order.Property(o => o.PaymentType).IsRequired();
            order.Property(o => o.Status).IsRequired();
            order.Property(o => o.ShippingType).IsRequired();
            order.Property(o => o.InvoiceType).IsRequired();
            order.Property(o => o.CreatedDate).IsRequired();
            order.Property(o => o.EditedDate).IsRequired();
            order.HasMany(o => o.OrderItems)
                 .WithOne(oi => oi.Order)
                 .HasForeignKey(oi => oi.OrderItemAkId)
                 .HasPrincipalKey(o => o.OrderAkId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region OrderItem
            var orderItem = builder.Entity<OrderItem>();
            orderItem.HasKey(oi => oi.OrderItemId);
            orderItem.HasAlternateKey(oi => oi.OrderItemAkId);
            orderItem.Property(oi => oi.Quantity).IsRequired();
            orderItem.Property(oi => oi.TotalAmount).IsRequired();
            orderItem
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.OrderItemAkId)
                .HasPrincipalKey(p => p.ProductAkId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            orderItem
                .HasOne(oi => oi.ProductSku)
                .WithMany(ps => ps.OrderItems)
                .HasForeignKey(oi => oi.OrderItemAkId)
                .HasPrincipalKey(ps => ps.ProductSkuAkId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            orderItem
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderItemAkId)
                .HasPrincipalKey(o => o.OrderAkId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            orderItem
                .Property(oi => oi.CreatedDate)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow);
            orderItem
                .Property(oi => oi.EditedDate)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow);
            #endregion

            #region Faq
            var faq = builder.Entity<Faq>();
            faq.HasKey(f => f.FaqId);
            faq.HasAlternateKey(f => f.FaqAkId);
            faq.Property(f => f.Question)
                .IsRequired()
                .HasMaxLength(200);
            faq.Property(f => f.Answer)
                .IsRequired();
            faq.Property(f => f.IsActived)
                .IsRequired()
                .HasDefaultValue(true);
            faq.Property(f => f.CreatedDate)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow);
            faq.Property(f => f.EditedDate)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow);
            #endregion

            #region FaqCategory
            var faqCategory = builder.Entity<FaqCategory>();
            faqCategory.HasKey(fc => fc.FaqCategoryId);
            faqCategory.HasAlternateKey(fc => fc.FaqCategoryAkId);
            faqCategory.Property(fc => fc.Name)
                       .IsRequired()
                       .HasMaxLength(100);
            faqCategory.Property(fc => fc.IsActived)
                       .IsRequired()
                       .HasDefaultValue(true);
            faqCategory.Property(fc => fc.CreatedDate)
                       .IsRequired()
                       .HasDefaultValue(DateTime.UtcNow);
            faqCategory.Property(fc => fc.EditedDate)
                       .IsRequired()
                       .HasDefaultValue(DateTime.UtcNow);

            faqCategory.HasMany(fc => fc.Childs)
                       .WithOne(fc => fc.Parent)
                       .HasForeignKey(fc => fc.ParentId)
                       .HasPrincipalKey(fc => fc.FaqCategoryAkId)
                       .OnDelete(DeleteBehavior.ClientSetNull);
            #endregion

            #region Faq_FaqCategory
            var faqFaqcategory = builder.Entity<Faq_FaqCategory>();
            faqFaqcategory.HasKey(ffc => new { ffc.FaqAkId, ffc.FaqCategoryAkId });
            faqFaqcategory
                .HasOne(ffc => ffc.Faq)
                .WithMany(f => f.Faq_FaqCategory)
                .HasForeignKey(ffc => ffc.FaqAkId)
                .HasPrincipalKey(f => f.FaqAkId);
            faqFaqcategory
                .HasOne(ffc => ffc.FaqCategory)
                .WithMany(f => f.Faq_FaqCategory)
                .HasForeignKey(ffc => ffc.FaqCategoryAkId)
                .HasPrincipalKey(fc => fc.FaqCategoryAkId);
            #endregion

            #region Notice
            var notice = builder.Entity<Notice>();
            #endregion


        }

    }
}