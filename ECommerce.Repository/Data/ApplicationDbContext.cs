using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public virtual DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region  Prodcut
            var product = builder.Entity<Product>();
            product.HasKey(p => p.ID).IsClustered();
            product.HasOne(p => p.Category).WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.ClientSetNull);
            product.Property(p => p.Title).IsRequired();
            product.Property(p => p.Status).IsRequired();
            product.Property(p => p.Price).IsRequired();
            product.Property(p => p.EditedDate).IsRequired();
            product.Property(p => p.CreatedDate).IsRequired();
            #endregion

            #region  Category
            var category = builder.Entity<Category>();
            category.HasKey(c => c.ID).IsClustered();
            category.HasAlternateKey(c => c.Guid);
            category.HasMany(c => c.Childs).WithOne(c => c.Parent)
                    .HasForeignKey(c => c.ParentId).HasPrincipalKey(c => c.Guid).OnDelete(DeleteBehavior.ClientSetNull);
            category.Property(c => c.Name).IsRequired().HasMaxLength(100);
            category.HasAlternateKey(c => c.Guid);
            category.Property(c => c.EditedDate).IsRequired();
            category.Property(c => c.CreatedDate).IsRequired();
            #endregion
        }

    }
}