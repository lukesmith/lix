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

        IEnumerable<TEntity> List(object specification);

        IEnumerable<TEntity> List(object specification, int startIndex, int pageSize);

        long Count(object specification);

        bool Exists();
    }
}