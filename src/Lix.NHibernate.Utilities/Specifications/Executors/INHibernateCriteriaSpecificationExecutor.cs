namespace Lix.Commons.Specifications.Executors
{
    public interface INHibernateCriteriaSpecificationExecutor<TEntity> : ISpecificationExecutor<INHibernateCriteriaSpecification<TEntity>, TEntity>
        where TEntity : class
    {
    }
}