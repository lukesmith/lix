namespace Lix.Commons.Specifications
{
    public interface INHibernateCriteriaSpecificationExecutor<TEntity> : ISpecificationExecutor<INHibernateCriteriaSpecification<TEntity>, TEntity>
        where TEntity : class
    {
    }
}