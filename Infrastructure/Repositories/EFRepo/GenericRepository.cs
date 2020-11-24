using System.Threading.Tasks;
using ECommerce.Demo.Core.Entities;
using ECommerce.Demo.Core.Interfaces;

namespace Infrastructure.Repositories.EFRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public Task<int> CountAsync(ISpecification<T> spec)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            throw new System.NotImplementedException();
        }

        public Task<System.Collections.Generic.IReadOnlyList<T>> ListAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<System.Collections.Generic.IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            throw new System.NotImplementedException();
        }
    }
}