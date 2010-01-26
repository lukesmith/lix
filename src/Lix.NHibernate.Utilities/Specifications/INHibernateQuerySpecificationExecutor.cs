using Lix.Commons.Specifications.Executors;

namespace Lix.Commons.Specifications
{
    public interface INHibernateQuerySpecificationExecutor<TEntity> : ISpecificationExecutor<INHibernateQuerySpecification, TEntity>
        where TEntity : class
    {
    }
}