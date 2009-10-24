using System.Linq;
using Lix.Commons.Specifications;
using NHibernate.Linq;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents extensions on the <see cref="Enumerable"/> object.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Creates a <see cref="PagedResult{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the objects.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="specification">The specification to find matching objects for.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A <see cref="PagedResult{T}"/> from the query.
        /// </returns>
        public static PagedResult<T> PagedList<T>(this INHibernateQueryable<T> query, IQueryableSpecification<T> specification, int startIndex, int pageSize)
        {
            var specificationCountQuery = specification.Build(query);
            var specificationQuery = specification.Build(query);

            var totalItems = specificationCountQuery.LongCount();
            var items = specificationQuery.Skip(startIndex).Take(pageSize).ToList();

            return new PagedResult<T>(startIndex, pageSize, totalItems, items);
        }
    }
}
