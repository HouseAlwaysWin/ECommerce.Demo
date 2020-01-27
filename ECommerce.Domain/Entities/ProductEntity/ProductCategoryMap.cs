namespace ECommerce.Domain.Entities.ProductEntity
{
    public class ProductCategoryMap
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}