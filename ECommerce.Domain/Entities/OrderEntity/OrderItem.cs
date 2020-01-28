using System.Collections.Generic;
using ECommerce.Domain.Entities.ProductEntity;

namespace ECommerce.Domain.Entities.OrderEntity
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public ProductSku ProductSku { get; set; }
        public int ProductSkuId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }

    }
}