using System;
using System.Collections.Generic;
using ECommerce.Domain.Entities.ProductEntity;

namespace ECommerce.Domain.Entities.ImageEntity
{
    public enum ImageType
    {
        Product,
        Category,
    }

    public class ImageInfo
    {
        public int ImageInfoId { get; set; }
        public string Url { get; set; }
        public ImageType Type { get; set; }
        public bool IsActived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}