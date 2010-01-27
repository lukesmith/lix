namespace Lix.Commons.Specifications.Executors
{
    public interface INHibernateQuerySpecificationExecutor<TEntity> : ISpecificationExecutor<INHibernateQuerySpecification, TEntity>
        where TEntity : class
    {
    }
}