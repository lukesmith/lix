using Lix.Commons.Repositories;

namespace Lix.Commons.Specifications.Executors
{
    public interface ISpecificationExecutorFactory
    {
        ISpecificationExecutor<TEntity> CreateExecutor<TSpecification, TEntity, TRepository>(
            TSpecification specification, TRepository repository)
            where TSpecification : ISpecification
            where TRepository : IReportingRepository<TEntity>
            where TEntity : class;
    }
}