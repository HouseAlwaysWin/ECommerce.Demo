
namespace ECommerce.Demo.Core.Entities {
    public class ProductPrice : BaseEntity {
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Unit { get; set; }
    }
}