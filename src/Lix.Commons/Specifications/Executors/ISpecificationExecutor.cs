using Lix.Commons.Repositories;

namespace Lix.Commons.Specifications.Executors
{
    public interface ISpecificationExecutor<TEntity> : IExecuteGet<TEntity>, IExecuteList<TEntity>, IExecuteCount, IExecuteExists
        where TEntity : class
    {
        void SetRepository(IReportingRepository<TEntity> repository);
    }
}