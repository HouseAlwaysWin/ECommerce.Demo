namespace ECommerce.Demo.API.Domain.Entities {
    public class ProductPrice {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Unit { get; set; }
    }
}