using System;

namespace ECommerce.Domain.Entities.ImageEntity
{
    public enum ImageStatus
    {
        Product,
        Category,
    }

    public class ImageInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public ImageStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}