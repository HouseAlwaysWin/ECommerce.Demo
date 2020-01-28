using System;

namespace ECommerce.Domain.Entities.ProductEntity
{
    public class Product_ProductCategory
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}