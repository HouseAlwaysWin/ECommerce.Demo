using System.Collections.Generic;
using ECommerce.Domain.Entities.ProductEntity;

namespace ECommerce.Domain.Entities.OrderEntity
{
    public class Order
    {
        public long ID { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}