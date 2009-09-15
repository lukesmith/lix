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

    public interface ISpecificationExecutor<TSpecification, TEntity> : ISpecificationExecutor<TEntity>
    {
        TSpecification Specification
        {
            get;
        }

        TEntity Get();

        IEnumerable<TEntity> List(TSpecification specification);

        IEnumerable<TEntity> List(TSpecification specification, int startIndex, int pageSize);

        long Count(TSpecification specification);

        bool Exists();
    }
}