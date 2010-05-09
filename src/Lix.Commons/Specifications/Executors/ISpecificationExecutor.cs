using Lix.Commons.Repositories;

namespace Lix.Commons.Specifications.Executors
{
    public interface ISpecificationExecutor<TEntity> : IExecuteGet<TEntity>, IExecuteList<TEntity>, IExecuteCount, IExecuteExists
        where TEntity : class
    {
        void SetRepository(IRepository<TEntity> repository);
    }
}