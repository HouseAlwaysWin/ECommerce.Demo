using System;
using System.Collections.Generic;
using ECommerce.Domain.Entities.ProductEntity;

namespace ECommerce.Domain.Entities.OrderEntity
{
    public enum PaymentType
    {
        ConvenientStore,
        LinePay,
        CreditCard,
        ATM,
        AfterShippingPay
    }
    public enum OrderStatus
    {
        Cancelled,
        CheckingOrder,
        HasPaid,
        Processing,
        Shipping,
        Arrivaled,
        Refund
    }

    public enum ShippingType
    {

    }

    public enum InvoiceType
    {
        Donate,
        DuplicateInvoice,
        TriplicateInvoice
    }
    public class Order
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int TotalAmount { get; set; }
        public int Tax { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderStatus Status { get; set; }
        public ShippingType ShippingType { get; set; }
        public string ShippingAddress { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public string InvoiceVat { get; set; }
        public string DonateCode { get; set; }
        public DateTime? CancelledDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}