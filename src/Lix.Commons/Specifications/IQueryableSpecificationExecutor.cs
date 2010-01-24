namespace Lix.Commons.Specifications
{
    public interface IQueryableSpecificationExecutor<TEntity> : ISpecificationExecutor<IQueryableSpecification<TEntity>, TEntity>
        where TEntity : class
    {
    }
}