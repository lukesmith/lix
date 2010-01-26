namespace Lix.Commons.Specifications.Executors
{
    public interface IQueryableSpecificationExecutor<TEntity> : ISpecificationExecutor<IQueryableSpecification<TEntity>, TEntity>
        where TEntity : class
    {
    }
}