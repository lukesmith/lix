using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lix.Commons.Specifications
{
    internal class InterceptionQueryableSpecification<TEntity> : DefaultQueryableSpecification<TEntity>
        where TEntity : class
    {
        private readonly IQueryable<TEntity> data;

        public InterceptionQueryableSpecification(IQueryable<TEntity> data)
        {
            this.data = data;
        }

        protected override Expression<Func<TEntity, bool>> Predicate
        {
            get
            {
                return x => true;
            }
        }

        public override IQueryable<TEntity> Build(IQueryable<TEntity> context)
        {
            return this.data;
        }
    }
}