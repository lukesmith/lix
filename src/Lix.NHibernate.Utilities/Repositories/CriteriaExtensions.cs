using System.Linq;
using NHibernate;
using NHibernate.Criterion;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents extensions on the <see cref="ICriteria"/> object.
    /// </summary>
    public static class CriteriaExtensions
    {
        /// <summary>
        /// Based on the ICriteria will return a paged result set, will create two copies
        /// of the query 1 will be used to select the total count of items, the other
        /// used to select the page of data.
        /// The results will be wraped in a PagedResult object which will contain
        /// the items and total item count.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A <see cref="PagedResult{TEntity}"/> collection.
        /// </returns>
        public static PagedResult<TEntity> PagedList<TEntity>(this ICriteria criteria, int startIndex, int pageSize)
        {
            ICriteria countCriteria = CriteriaTransformer.Clone(criteria)
                .SetProjection(Projections.RowCountInt64());

            countCriteria.ClearOrders();

            // Clone a copy fo the criteria to get the page of data,
            // setting max and first result, this will get the page of data.s
            ICriteria pageCriteria = CriteriaTransformer.Clone(criteria)
                    .SetMaxResults(pageSize)
                    .SetFirstResult(startIndex);

            var countFuture = countCriteria.FutureValue<long>();
            var listFuture = pageCriteria.Future<TEntity>();

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
        /// <param name="criteria">The criteria to add a count to.</param>
        /// <returns>
        /// A numerical value of the number of items matching the criteria.
        /// </returns>
        public static long Count(this ICriteria criteria)
        {
            ICriteria countCriteria = CriteriaTransformer.Clone(criteria)
                .SetProjection(Projections.RowCountInt64());

            countCriteria.ClearOrders();

            return countCriteria.UniqueResult<long>();
        }
    }
}