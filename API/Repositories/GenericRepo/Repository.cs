using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Repositories.GenericRepo {
    public class Repository<T> : IRepository<T> where T : class {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository (DbContext context) {
            _dbContext = context ??
                throw new ArgumentException (nameof (context));
            _dbSet = _dbContext.Set<T> ();
        }

        public void Delete (T entity) {
            throw new NotImplementedException ();
        }

        public void Delete (params T[] entities) {
            throw new NotImplementedException ();
        }

        public void Delete (IEnumerable<T> entities) {
            throw new NotImplementedException ();
        }

        public void Dispose () {
            throw new NotImplementedException ();
        }

        public IPaginate<T> GetList (
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0, int size = 20, bool enableTracking = true) {

            IQueryable<T> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking ();

            if (include != null) query = include (query);

            if (predicate != null) query = query.Where (predicate);

            return orderBy != null ? orderBy (query).ToPaginate (index, size) : query.ToPaginate (index, size);
        }

        public IPaginate<TResult> GetList<TResult> (
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0, int size = 20, bool enableTracking = true) where TResult : class {

            IQueryable<T> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking ();

            if (include != null) query = include (query);

            if (predicate != null) query = query.Where (predicate);

            return orderBy != null ?
                orderBy (query).Select (selector).ToPaginate (index, size) :
                query.Select (selector).ToPaginate (index, size);
        }

        public Task<IPaginate<T>> GetListAsync (Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int index = 0, int size = 20, bool enableTracking = true, CancellationToken cancellationToken = default) {
            throw new NotImplementedException ();
        }

        public Task<IPaginate<T>> GetListAsync (Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int index = 0, int size = 20) {
            throw new NotImplementedException ();
        }

        public Task<IPaginate<TResult>> GetListAsync<TResult> (Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int index = 0, int size = 20, CancellationToken cancellationToken = default, bool ignoreQueryFilters = false) where TResult : class {
            throw new NotImplementedException ();
        }

        public T Insert (T entity) {
            throw new NotImplementedException ();
        }

        public void Insert (params T[] entities) {
            throw new NotImplementedException ();
        }

        public void Insert (IEnumerable<T> entities) {
            throw new NotImplementedException ();
        }

        public ValueTask<EntityEntry<T>> InsertAsync (T entity, CancellationToken cancellationToken = default) {
            throw new NotImplementedException ();
        }

        public Task InsertAsync (params T[] entities) {
            throw new NotImplementedException ();
        }

        public Task InsertAsync (IEnumerable<T> entities, CancellationToken cancellationToken = default) {
            throw new NotImplementedException ();
        }

        public T SingleOrDefault (
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true, bool ignoreQueryFilters = false) {
            throw new NotImplementedException ();
        }

        public T SingleOrDefault (
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true) {

            IQueryable<T> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking ();

            if (include != null) query = include (query);

            if (predicate != null) query = query.Where (predicate);

            if (orderBy != null)
                return orderBy (query).FirstOrDefault ();
            return query.FirstOrDefault ();
        }

        public async Task<T> SingleOrDefaultAsync (
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true, bool ignoreQueryFilters = false) {

            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking ();

            if (include != null) query = include (query);

            if (predicate != null) query = query.Where (predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters ();

            if (orderBy != null) return await orderBy (query).FirstOrDefaultAsync ();

            return await query.FirstOrDefaultAsync ();
        }

        public async Task<T> SingleOrDefaultAsync (
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) {

            return await SingleOrDefaultAsync (predicate, orderBy, include, false);
        }

        public void Update (T entity) {
            throw new NotImplementedException ();
        }

        public void Update (params T[] entities) {
            throw new NotImplementedException ();
        }

        public void Update (IEnumerable<T> entities) {
            throw new NotImplementedException ();
        }
    }
}