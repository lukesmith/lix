using System.Collections.Generic;
using Lix.Commons.Specifications;

namespace Lix.Commons.Services
{
    public interface IService<T>
    {
        T Save(T entity);
        void Delete(T entity);

        IQueryableSpecification<T> GetListSpecification();
        IEnumerable<T> List();
        PagedList<T> List(int startIndex, int pageSize);
    }
}
