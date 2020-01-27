using System.Collections.Generic;
using ECommerce.Domain.Entities.ProductEntity;
using ECommerce.Repository.Repositories.Interface;

namespace ECommerce.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetAllProducts()
        {
            throw new System.NotImplementedException();
        }

        public Product GetProductById(long Id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            throw new System.NotImplementedException();
        }


        public bool CreateProduct()
        {
            return false;
        }

        public bool UpdateProductById(Product product)
        {
            return false;
        }

        public bool DeleteProduct(long Id)
        {
            return false;
        }
    }
}