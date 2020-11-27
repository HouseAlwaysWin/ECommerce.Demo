using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Demo.Core.Entities;
using ECommerce.Demo.Core.Interfaces;
using ECommerce.Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Demo.Infrastructure.Repositories.EFRepo
{
    public class ProductRepository : IProductRepository {
        private readonly StoreContext _context;
        public ProductRepository (StoreContext context) {
            this._context = context;

        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync () {
            return await _context.ProductBrands.ToListAsync ();
        }

        public async Task<Product> GetProductByIdAsync (int id) {
            return await _context.Products
                .Include (p => p.ProductType)
                .Include (p => p.ProductBrand)
                .FirstOrDefaultAsync (p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts(int num = 1000)
        {
            return _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync () {
            return await _context.Products
                .Include (p => p.ProductType)
                .Include (p => p.ProductBrand)
                .ToListAsync ();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync () {
            return await _context.ProductTypes.ToListAsync ();
        }
    }
}