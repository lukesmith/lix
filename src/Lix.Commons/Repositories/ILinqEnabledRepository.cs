using System.Linq;

namespace Lix.Commons.Repositories
{
    public interface ILinqEnabledRepository<TEntity> : IReportingRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> RepositoryQuery { get; }
    }
}