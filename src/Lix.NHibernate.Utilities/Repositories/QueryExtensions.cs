using System.Linq;
using NHibernate;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents extensions on the <see cref="IQuery"/> object.
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// Based on the IQuery will return a paged result set, will create two copies
        /// of the query 1 will be used to select the total count of items, the other
        /// used to select the page of data.
        /// The results will be wraped in a PagedResult object which will contain
        /// the items and total item count.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A <see cref="PagedResult{TEntity}"/> collection.
        /// </returns>
        public static PagedResult<TEntity> PagedList<TEntity>(this IQuery query, IQuery countQuery, int startIndex, int pageSize)
        {
            var pageQuery = query.SetMaxResults(pageSize)
                .SetFirstResult(startIndex);

            var countFuture = countQuery.FutureValue<long>();
            var listFuture = pageQuery.Future<TEntity>();

            var count = countFuture.Value;
            var list = listFuture.ToList();

            // Create a new pagedresult object and populate it, use the paged query
            // to get the items, and the count query to get the total item count.
            var pagedResult = new PagedResult<TEntity>(
                startIndex,
                pageSize,
                count,
                list);

            // Return the result.
            return pagedResult;
        }

        /// <summary>
        /// Performs a COUNT(*) on the criteria.
        /// </summary>
        /// <param name="query">The query to add a count to.</param>
        /// <returns>
        /// A numerical value of the number of items matching the criteria.
        /// </returns>
        public static long Count(this IQuery query)
        {
            return query.UniqueResult<long>();
        }
    }
}