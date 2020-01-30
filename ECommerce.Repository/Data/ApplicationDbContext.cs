using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ECommerce.Domain.Entities.CommentEntity;
using ECommerce.Domain.Entities.FaqEntity;
using ECommerce.Domain.Entities.ImageEntity;
using ECommerce.Domain.Entities.NoticeEntity;
using ECommerce.Domain.Entities.NotificationEntity;
using ECommerce.Domain.Entities.OrderEntity;
using ECommerce.Domain.Entities.ProductEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Repository.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public virtual DbSet<Product> Product { get; set; }

        public virtual DbSet<ProductCategory> ProductCategory { get; set; }

        public virtual DbSet<ImageInfo> ImageInfo { get; set; }

        protected override void OnModelCreating (ModelBuilder builder) {
            #region  Prodcut
            var product = builder.Entity<Product> ();
            product.HasKey (p => p.ProductId).IsClustered ();
            product.HasAlternateKey (p => p.ProductAkId);
            product.Property (p => p.Title).IsRequired ();
            product.Property (p => p.Status)
                .IsRequired ()
                .HasDefaultValue (ProductStatus.ForSale);
            product.Property (p => p.IsActived)
                .IsRequired ()
                .HasDefaultValue (true);
            product.Property (p => p.EditedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            product.Property (p => p.CreatedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            product.HasMany (p => p.ProductSkus)
                .WithOne (ps => ps.Product)
                .HasPrincipalKey (p => p.ProductAkId)
                .OnDelete (DeleteBehavior.ClientSetNull);
            #endregion

            #region ProductSku
            var productSku = builder.Entity<ProductSku> ();
            productSku.HasKey (ps => ps.ProductSkuId).IsClustered ();
            productSku.HasAlternateKey (ps => ps.ProductSkuAkId);
            productSku.Property (ps => ps.StockQuantity).IsRequired ();
            productSku.Property (ps => ps.IsActived).HasDefaultValue (true);
            productSku.Property (ps => ps.Price).IsRequired ();
            productSku.Property (ps => ps.ReviewRateSum).IsRequired ().HasDefaultValue (0);
            productSku.Property (ps => ps.CreatedDate).IsRequired ().HasDefaultValue (DateTime.UtcNow);
            productSku.Property (ps => ps.EditedDate).IsRequired ().HasDefaultValue (DateTime.UtcNow);
            productSku
                .HasOne (ps => ps.Product)
                .WithMany (p => p.ProductSkus)
                .HasForeignKey (ps => ps.ProductSkuAkId)
                .OnDelete (DeleteBehavior.ClientSetNull);
            productSku
                .HasMany (ps => ps.ImageInfos)
                .WithOne (i => i.ProductSku)
                .HasPrincipalKey (ps => ps.ProductSkuAkId)
                .OnDelete (DeleteBehavior.ClientSetNull);
            productSku.HasMany (ps => ps.Comments)
                .WithOne (c => c.ProductSku)
                .HasForeignKey (c => c.CommentAkId)
                .HasPrincipalKey (ps => ps.ProductSkuAkId);
            #endregion

            #region  ProductCategory
            var productCategory = builder.Entity<ProductCategory> ();
            productCategory.HasKey (c => c.ProductCategoryId).IsClustered ();
            productCategory.HasAlternateKey (c => c.ProductCategoryAkId);
            productCategory
                .HasMany (pc => pc.Childs)
                .WithOne (pc => pc.Parent)
                .HasForeignKey (pc => pc.ParentId)
                .HasPrincipalKey (pc => pc.ProductCategoryAkId)
                .OnDelete (DeleteBehavior.ClientSetNull);
            productCategory.Property (c => c.Name).IsRequired ().HasMaxLength (100);
            productCategory.Property (pc => pc.IsActived).HasDefaultValue (true);
            productCategory.Property (c => c.EditedDate).IsRequired ().HasDefaultValue (DateTime.UtcNow);
            productCategory.Property (c => c.CreatedDate).IsRequired ().HasDefaultValue (DateTime.UtcNow);
            #endregion

            #region Product_ProductCategory
            var productProductCategory = builder.Entity<Product_ProductCategory> ();
            productProductCategory.HasKey (ppc => new { ppc.ProductAkId, ppc.ProductCategoryAkId }).IsClustered ();
            productProductCategory
                .HasOne (ppc => ppc.Product)
                .WithMany (p => p.Product_ProductCategory)
                .HasForeignKey (ppc => ppc.ProductAkId);
            productProductCategory
                .HasOne (ppc => ppc.ProductCategory)
                .WithMany (pc => pc.Product_ProductCategory)
                .HasForeignKey (ppc => ppc.ProductCategoryAkId);
            #endregion

            #region ImageInfo
            var imageInfo = builder.Entity<ImageInfo> ();
            imageInfo.HasKey (i => i.ImageInfoId).IsClustered ();
            imageInfo.HasAlternateKey (i => i.ImageInfoAKId);
            imageInfo.Property (i => i.Url).IsRequired ();
            imageInfo.Property (i => i.Type).IsRequired ();
            imageInfo.Property (i => i.IsActived).HasDefaultValue (true);
            imageInfo.Property (i => i.CreatedDate).IsRequired ().HasDefaultValue (DateTime.UtcNow);
            imageInfo.Property (i => i.EditedDate).IsRequired ().HasDefaultValue (DateTime.UtcNow);
            imageInfo
                .HasOne (i => i.ProductSku)
                .WithMany (p => p.ImageInfos)
                .HasForeignKey (i => i.ImageInfoAKId)
                .OnDelete (DeleteBehavior.ClientSetNull);
            #endregion

            #region Order
            var order = builder.Entity<Order> ();
            order.HasKey (o => o.OrderId).IsClustered ();
            order.HasAlternateKey (o => o.OrderAkId);
            order.Property (o => o.Username).IsRequired ().HasMaxLength (100);
            order.Property (o => o.PhoneNumber).HasMaxLength (20);
            order.Property (o => o.MobileNumber).HasMaxLength (20);
            order.Property (o => o.Email).IsRequired ();
            order.Property (o => o.TotalAmount).IsRequired ();
            order.Property (o => o.Tax).IsRequired ();
            order.Property (o => o.PaymentType).IsRequired ();
            order.Property (o => o.Status).IsRequired ();
            order.Property (o => o.ShippingType).IsRequired ();
            order.Property (o => o.InvoiceType).IsRequired ();
            order.Property (o => o.CreatedDate).IsRequired ();
            order.Property (o => o.EditedDate).IsRequired ();
            order.HasMany (o => o.OrderItems)
                .WithOne (oi => oi.Order)
                .HasForeignKey (oi => oi.OrderItemAkId)
                .HasPrincipalKey (o => o.OrderAkId)
                .OnDelete (DeleteBehavior.Cascade);
            #endregion

            #region OrderItem
            var orderItem = builder.Entity<OrderItem> ();
            orderItem.HasKey (oi => oi.OrderItemId);
            orderItem.HasAlternateKey (oi => oi.OrderItemAkId);
            orderItem.Property (oi => oi.Quantity).IsRequired ();
            orderItem.Property (oi => oi.TotalAmount).IsRequired ();
            orderItem
                .HasOne (oi => oi.Order)
                .WithMany (o => o.OrderItems)
                .HasForeignKey (oi => oi.OrderItemAkId)
                .HasPrincipalKey (o => o.OrderAkId)
                .OnDelete (DeleteBehavior.ClientSetNull);
            orderItem
                .Property (oi => oi.CreatedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            orderItem
                .Property (oi => oi.EditedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            #endregion

            #region Faq
            var faq = builder.Entity<Faq> ();
            faq.HasKey (f => f.FaqId);
            faq.HasAlternateKey (f => f.FaqAkId);
            faq.Property (f => f.Question)
                .IsRequired ()
                .HasMaxLength (200);
            faq.Property (f => f.Answer)
                .IsRequired ();
            faq.Property (f => f.IsActived)
                .IsRequired ()
                .HasDefaultValue (true);
            faq.Property (f => f.CreatedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            faq.Property (f => f.EditedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            #endregion

            #region FaqCategory
            var faqCategory = builder.Entity<FaqCategory> ();
            faqCategory.HasKey (fc => fc.FaqCategoryId);
            faqCategory.HasAlternateKey (fc => fc.FaqCategoryAkId);
            faqCategory.Property (fc => fc.Name)
                .IsRequired ()
                .HasMaxLength (100);
            faqCategory.Property (fc => fc.IsActived)
                .IsRequired ()
                .HasDefaultValue (true);
            faqCategory.Property (fc => fc.CreatedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            faqCategory.Property (fc => fc.EditedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);

            faqCategory.HasMany (fc => fc.Childs)
                .WithOne (fc => fc.Parent)
                .HasForeignKey (fc => fc.ParentId)
                .HasPrincipalKey (fc => fc.FaqCategoryAkId)
                .OnDelete (DeleteBehavior.ClientSetNull);
            #endregion

            #region Faq_FaqCategory
            var faqFaqcategory = builder.Entity<Faq_FaqCategory> ();
            faqFaqcategory.HasKey (ffc => new { ffc.FaqAkId, ffc.FaqCategoryAkId });
            faqFaqcategory
                .HasOne (ffc => ffc.Faq)
                .WithMany (f => f.Faq_FaqCategory)
                .HasForeignKey (ffc => ffc.FaqAkId)
                .HasPrincipalKey (f => f.FaqAkId);
            faqFaqcategory
                .HasOne (ffc => ffc.FaqCategory)
                .WithMany (f => f.Faq_FaqCategory)
                .HasForeignKey (ffc => ffc.FaqCategoryAkId)
                .HasPrincipalKey (fc => fc.FaqCategoryAkId);
            #endregion

            #region Notice
            var notice = builder.Entity<Notice> ();
            notice.HasKey (n => n.NoticeId);
            notice.HasAlternateKey (n => n.NoticeAkId);
            notice.Property (n => n.Title).IsRequired ().HasMaxLength (100);
            notice.Property (n => n.Content).IsRequired ().HasMaxLength (1000);
            notice.Property (n => n.CreatedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            notice.Property (n => n.EditedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            #endregion

            #region NoticeCategory
            var noticeCategory = builder.Entity<NoticeCategory> ();
            noticeCategory.HasKey (nc => nc.NoticeCategoryId);
            noticeCategory.HasAlternateKey (nc => nc.NoticeCategoryAkId);
            noticeCategory.Property (nc => nc.Name)
                .IsRequired ()
                .HasMaxLength (100);
            noticeCategory.Property (nc => nc.IsActived)
                .IsRequired ()
                .HasDefaultValue (true);
            noticeCategory.Property (n => n.CreatedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            noticeCategory.Property (n => n.EditedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            noticeCategory.HasMany (nc => nc.Child)
                .WithOne (nc => nc.Parent)
                .HasForeignKey (nc => nc.ParentId)
                .HasPrincipalKey (nc => nc.NoticeCategoryAkId)
                .OnDelete (DeleteBehavior.SetNull);
            #endregion

            #region Notice_NoticeCategory
            var noticeNoticeCategory = builder.Entity<Notice_NoticeCategory> ();
            noticeNoticeCategory.HasKey (nnc => new { nnc.NoticeAkId, nnc.NoticeCategoryAkId });
            noticeNoticeCategory.HasOne (nnc => nnc.Notice)
                .WithMany (nnc => nnc.Notice_NoticeCategory)
                .HasForeignKey (nnc => nnc.NoticeAkId)
                .HasPrincipalKey (n => n.NoticeAkId);
            noticeNoticeCategory.HasOne (nnc => nnc.NoticeCategory)
                .WithMany (nnc => nnc.Notice_NoticeCategory)
                .HasForeignKey (nnc => nnc.NoticeCategoryAkId)
                .HasPrincipalKey (nc => nc.NoticeCategoryAkId);
            #endregion

            #region Comment
            var comment = builder.Entity<Comment> ();
            comment.HasKey (c => c.CommentId);
            comment.HasAlternateKey (c => c.CommentAkId);
            comment.Property (c => c.Username).IsRequired ();
            comment.Property (c => c.Content).IsRequired ();
            comment.Property (c => c.ReviewRate).HasDefaultValue (0);
            comment.Property (n => n.CreatedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            comment.Property (n => n.EditedDate)
                .IsRequired ()
                .HasDefaultValue (DateTime.UtcNow);
            comment.HasOne (c => c.ProductSku)
                .WithMany (ps => ps.Comments)
                .HasForeignKey (c => c.CommentAkId)
                .HasPrincipalKey (ps => ps.ProductSkuAkId);
            #endregion

        }

    }
}