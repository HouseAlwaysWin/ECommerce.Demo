using System;
using System.Collections.Generic;
using ECommerce.Domain.Entities.ProductEntity;

namespace ECommerce.Domain.Entities.OrderEntity
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public Guid OrderItemAkId { get; set; }
        public int Quantity { get; set; }
        public int TotalAmount { get; set; }
        public Product Product { get; set; }
        public ProductSku ProductSku { get; set; }
        public Order Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }

    }
}