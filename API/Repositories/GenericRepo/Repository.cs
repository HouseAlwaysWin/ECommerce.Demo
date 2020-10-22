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
    public abstract class Repository<T> : IRepository<T> where T : class {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository (DbContext context) {
            _dbContext = context ??
                throw new ArgumentException (nameof (context));
            _dbSet = _dbContext.Set<T> ();
        }

        public void Delete (T entity) {
            _dbSet.Remove (entity);
        }

        public void Delete (params T[] entities) {
            _dbSet.RemoveRange (entities);
        }

        public void Delete (IEnumerable<T> entities) {
            _dbSet.RemoveRange (entities);
        }

        public void Dispose () {
            _dbContext?.Dispose ();
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

        public IPaginate<T> GetList (Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0, int size = 20) {
            return GetList (predicate, orderBy, include, index, size, false);
        }

        public IPaginate<TResult> GetList<TResult> (Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0, int size = 20) where TResult : class {
            return GetList (selector, predicate, orderBy, include, index, size, false);
        }

        public Task<IPaginate<T>> GetListAsync (Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool enableTracking = true,
            CancellationToken cancellationToken = default) {
            IQueryable<T> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking ();

            if (include != null) query = include (query);

            if (predicate != null) query = query.Where (predicate);

            if (orderBy != null)
                return orderBy (query).ToPaginateAsync (index, size, 0, cancellationToken);
            return query.ToPaginateAsync (index, size, 0, cancellationToken);
        }

        public Task<IPaginate<TResult>> GetListAsync<TResult> (Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool enableTracking = true,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false)
        where TResult : class {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking ();

            if (include != null) query = include (query);

            if (predicate != null) query = query.Where (predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters ();

            if (orderBy != null)
                return orderBy (query).Select (selector).ToPaginateAsync (index, size, 0, cancellationToken);

            return query.Select (selector).ToPaginateAsync (index, size, 0, cancellationToken);
        }
        public async Task<IPaginate<T>> GetListAsync (Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20) {
            return await GetListAsync (predicate, orderBy, include, index, size, false);
        }

        public async Task<IPaginate<TResult>> GetListAsync<TResult> (Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false) where TResult : class {
            return await GetListAsync (selector, predicate, orderBy, include, index, size, false, cancellationToken,
                ignoreQueryFilters);
        }

        public virtual T Insert (T entity) {
            return _dbSet.Add (entity).Entity;
        }

        public void Insert (params T[] entities) {
            _dbSet.AddRange (entities);
        }

        public void Insert (IEnumerable<T> entities) {
            _dbSet.AddRange (entities);
        }

        public virtual ValueTask<EntityEntry<T>> InsertAsync (
            T entity, CancellationToken cancellationToken = default) {
            return _dbSet.AddAsync (entity, cancellationToken);
        }

        public virtual Task InsertAsync (params T[] entities) {
            return _dbSet.AddRangeAsync (entities);
        }

        public virtual Task InsertAsync (
            IEnumerable<T> entities, CancellationToken cancellationToken = default) {
            return _dbSet.AddRangeAsync (entities);
        }

        public T SingleOrDefault (Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
        ) {
            return SingleOrDefault (predicate, orderBy, include, false);
        }

        public T SingleOrDefault (
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true, bool ignoreQueryFilters = false) {

            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking ();

            if (include != null) query = include (query);

            if (predicate != null) query = query.Where (predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters ();

            return orderBy != null ? orderBy (query).FirstOrDefault () : query.FirstOrDefault ();
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

        public virtual async Task<T> SingleOrDefaultAsync (
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

        public virtual async Task<T> SingleOrDefaultAsync (
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) {

            return await SingleOrDefaultAsync (predicate, orderBy, include, false);
        }

        public void Update (T entity) {
            _dbSet.Update (entity);
        }

        public void Update (params T[] entities) {
            _dbSet.UpdateRange (entities);
        }

        public void Update (IEnumerable<T> entities) {
            _dbSet.UpdateRange (entities);
        }
    }
}