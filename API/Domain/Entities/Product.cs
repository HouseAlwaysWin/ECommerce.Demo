using System;
using System.Collections.Generic;

namespace ECommerce.Demo.API.Domain.Entities {
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public ICollection<ProductPrice> Prices { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}