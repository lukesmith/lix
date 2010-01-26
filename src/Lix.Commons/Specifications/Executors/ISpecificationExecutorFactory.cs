using Lix.Commons.Repositories;

namespace Lix.Commons.Specifications.Executors
{
    public interface ISpecificationExecutorFactory
    {
        ISpecificationExecutor<TEntity> CreateExecutor<TSpecification, TEntity, TRepository>(
            TSpecification specification, TRepository repository)
            where TSpecification : ISpecification
            where TRepository : IQueryRepository<TEntity>
            where TEntity : class;
    }
}