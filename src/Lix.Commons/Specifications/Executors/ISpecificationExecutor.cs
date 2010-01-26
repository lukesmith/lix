using System.Collections.Generic;

namespace Lix.Commons.Specifications.Executors
{
    public interface ISpecificationExecutor<TEntity>
        where TEntity : class
    {
        TEntity Get();

        IEnumerable<TEntity> List();

        PagedResult<TEntity> List(int startIndex, int pageSize);

        long Count();

        bool Exists();
    }
}