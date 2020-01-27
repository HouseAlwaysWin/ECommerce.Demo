using System.Collections.Generic;
using ECommerce.Repository.Repositories.Interface;
using ECommerce.Service.Services.Interfaces;

namespace ECommerce.Service.Services
{
    public class ProductService<T> : IProductService<T>
    {
        IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public T GetProductById(string Id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetProducts()
        {
            throw new System.NotImplementedException();
        }
    }
}