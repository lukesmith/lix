using System.Collections.Generic;

namespace Lix.Commons.Specifications
{
    public interface ISpecificationExecutor<TEntity>
    {
        object Specification
        {
            get;
        }

        TEntity Get();

        IEnumerable<TEntity> List();

        IEnumerable<TEntity> List(int startIndex, int pageSize);

        long Count();

        bool Exists();
    }
}