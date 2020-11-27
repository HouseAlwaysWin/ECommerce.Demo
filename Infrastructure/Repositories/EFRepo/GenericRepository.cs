using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Demo.Core.Entities;
using ECommerce.Demo.Core.Interfaces;
using ECommerce.Demo.Infrastructure.Data;
using ECommerce.Demo.Infrastructure.Repositories.EFRepo.Specs;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Demo.Infrastructure.Repositories.EFRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {  private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            this._context = context;
            
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec){
            return SpecEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(),spec);
        }

    }
}