using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECommerce.Domain.Entities.ImageEntity;
using ECommerce.Domain.Entities.OrderEntity;

namespace ECommerce.Domain.Entities.ProductEntity
{
    public enum ProductStatus
    {
        ForSale,
        Discontinued,
        OnSale,
    }
    public class Product
    {
        public int ProductId { get; set; }
        public Guid ProductSkuId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime EditedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Product_ProductCategory> Product_ProductCategory { get; set; }
        public virtual ICollection<ProductSku> ProductSkus { get; set; }

    }
}