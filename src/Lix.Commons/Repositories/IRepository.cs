using System.Collections.Generic;
using Lix.Commons.Specifications;

namespace Lix.Commons.Repositories
{
    public interface IRepository<T>
    {
        T Save(T entity);
        void Remove(T entity);

        T Get(ISpecification specification);
        IEnumerable<T> List(ISpecification specification);
        PagedResult<T> List(ISpecification specification, int startIndex, int pageSize);

        bool Exists(IQueryableSpecification<T> specification);
    }
}
