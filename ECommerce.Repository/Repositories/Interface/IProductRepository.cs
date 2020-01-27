using System.Collections.Generic;
using ECommerce.Domain.Entities.ProductEntity;

namespace ECommerce.Repository.Repositories.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(long Id);
        IEnumerable<Product> GetProductsByName(string name);
        IEnumerable<Product> GetProductsByCategory(string categoryId);
        bool CreateProduct();
        bool UpdateProductById(Product product);
        bool DeleteProduct(long Id);
    }
}