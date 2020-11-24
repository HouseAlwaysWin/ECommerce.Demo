using System;
using System.Collections.Generic;

namespace ECommerce.Demo.Core.Entities {
    public class Product:BaseEntity {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
        public ICollection<ProductPrice> Prices { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}