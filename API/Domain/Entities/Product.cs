using System;

namespace ECommerce.Demo.API.Domain.Entities {
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}