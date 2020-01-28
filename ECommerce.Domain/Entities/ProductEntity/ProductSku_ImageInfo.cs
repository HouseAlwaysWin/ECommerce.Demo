using System;
using ECommerce.Domain.Entities.ImageEntity;

namespace ECommerce.Domain.Entities.ProductEntity
{
    public class ProductSku_ImageInfo
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ImageInfoId { get; set; }
        public ImageInfo ImageInfo { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime EditDate { get; set; }

    }
}