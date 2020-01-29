using System;
using System.Collections.Generic;
using ECommerce.Domain.Entities.ImageEntity;
using ECommerce.Domain.Entities.OrderEntity;

namespace ECommerce.Domain.Entities.ProductEntity
{
    public class ProductSku
    {
        public int ProductSkuId { get; set; }
        public Guid ProductSkuAkId { get; set; }
        public int StockQuantity { get; set; }
        public string Manufacturer { get; set; }
        public bool IsActived { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public Product Product { get; set; }
        public virtual ICollection<ImageInfo> ImageInfos { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}