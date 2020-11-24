using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Demo.Core.Entities;

namespace ECommerce.Demo.Core.Interfaces {
    public interface IProductRepository {
        IEnumerable<Product> GetProducts (int num = 1000);
        Task<Product> GetProductByIdAsync (int id);
        Task<IReadOnlyList<Product>> GetProductsAsync ();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync ();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync ();

    }
}