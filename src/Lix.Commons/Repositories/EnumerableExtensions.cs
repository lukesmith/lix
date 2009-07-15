using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Specifications;
using NHibernate.Linq;

namespace Lix.Commons.Repositories
{
    public static class EnumerableExtensions
    {
        public static PagedList<T> PagedList<T>(this IEnumerable<T> enumerable, int startIndex, int pageSize)
        {
            var totalItems = enumerable.LongCount();
            var items = enumerable.Skip(startIndex).Take(pageSize).ToList();

            return new PagedList<T>(startIndex, pageSize, totalItems, items);
        }

        public static PagedList<T> PagedList<T>(this INHibernateQueryable<T> query, IQueryableSpecification<T> specification, int startIndex, int pageSize)
        {
            var specificationCountQuery = specification.Build(query);
            var specificationQuery = specification.Build(query);

            var totalItems = specificationCountQuery.LongCount();
            var items = specificationQuery.Skip(startIndex).Take(pageSize).ToList();

            return new PagedList<T>(startIndex, pageSize, totalItems, items);
        }
    }
}
