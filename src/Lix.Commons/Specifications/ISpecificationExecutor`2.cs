using System.Collections.Generic;

namespace Lix.Commons.Specifications
{
    public interface ISpecificationExecutor<TSpecification, TEntity> : ISpecificationExecutor<TEntity>
    {
        TSpecification Specification
        {
            get;
        }

        TEntity Get();

        IEnumerable<TEntity> List();

        PagedResult<TEntity> List(int startIndex, int pageSize);

        long Count();

        bool Exists();
    }
}