using System.Linq;

namespace Lix.Commons.Repositories
{
    public interface ILinqRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> RepositoryQuery { get; }
    }
}