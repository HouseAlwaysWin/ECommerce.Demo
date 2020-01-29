using System;

namespace ECommerce.Domain.Entities.ProductEntity
{
    public class Product_ProductCategory
    {
        public Guid ProductAkId { get; set; }
        public Product Product { get; set; }
        public Guid ProductCategoryAkId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}