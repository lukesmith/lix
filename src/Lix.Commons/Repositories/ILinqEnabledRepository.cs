using System.Linq;

namespace Lix.Commons.Repositories
{
    public interface ILinqEnabledRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> RepositoryQuery { get; }
    }
}