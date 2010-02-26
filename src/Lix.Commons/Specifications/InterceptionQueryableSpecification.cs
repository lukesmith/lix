using System.Linq;

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

        protected override IQueryable<TEntity> Build(IQueryable<TEntity> context)
        {
            return this.data;
        }
    }
}