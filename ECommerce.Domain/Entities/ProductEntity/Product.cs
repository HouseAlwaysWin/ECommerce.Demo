using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECommerce.Domain.Entities.ImageEntity;

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
        public int ID { get; set; }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public int Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime EditedDate { get; set; }
        public DateTime CreatedDate { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ImageId { get; set; }
        public ICollection<ImageInfo> ImagesInfo { get; set; }

    }
}