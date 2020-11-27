using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ECommerce.Demo.Core.Interfaces;

namespace ECommerce.Demo.Infrastructure.Repositories.EFRepo.Specs
{
        public class BaseSpec<T> : ISpecification<T>
    {
        public BaseSpec()
        {
        }
        public BaseSpec(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria  {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy  {get; private set;}

        public Expression<Func<T, object>> OrderByDescending  {get;private set;}

        public int Take  {get; private set;}

        public int Skip  {get; private set;}

        public bool IsPagingEnabled {get; private set;}

        protected void AddInclude(Expression<Func<T,object>> includeExpression){
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T,object>> orderByExpression){
            OrderBy = orderByExpression;
        }

        protected void ApplyPaging(int skip, int take){
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected void AddOrderByDescending(Expression<Func<T,object>> orderByDescExpression){
            OrderByDescending = orderByDescExpression;
        }
    }
}