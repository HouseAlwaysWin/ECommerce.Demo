using System;
using System.Collections.Generic;

namespace ECommerce.Domain.Entities.ProductEntity
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public Guid ProductCategoryAkId { get; set; }
        public string Name { get; set; }
        public bool IsActived { get; set; }
        public DateTime EditedDate { get; set; }
        public DateTime CreatedDate { get; set; }


        public Guid? ParentId { get; set; }
        public ProductCategory Parent { get; set; }
        public virtual ICollection<ProductCategory> Childs { get; set; }

        public virtual ICollection<Product_ProductCategory> Product_ProductCategory { get; set; }

    }
}