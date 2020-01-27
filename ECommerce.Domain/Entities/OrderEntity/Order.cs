using System.Collections.Generic;
using ECommerce.Domain.Entities.ProductEntity;

namespace ECommerce.Domain.Entities.OrderEntity
{
    public enum PaymentType
    {

    }
    public enum OrderStatus
    {

    }
    public class Order
    {
        public long OrderID { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderStatus Status { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public int ProductId { get; set; }
    }
}