using System.Collections.Generic;

namespace Lix.Commons.Specifications.Executors
{
    public interface IExecuteList<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> List();

        PagedResult<TEntity> List(int startIndex, int pageSize);
    }
}