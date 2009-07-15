using System.Collections;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;

namespace Lix.Commons.Repositories.NHibernate
{
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
        /// <param name="session">The session.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static PagedList<TEntity> PagedList<TEntity>(this ICriteria criteria, ISession session, int startIndex, int pageSize)
        {
            IMultiCriteria multiCriteria = session.CreateMultiCriteria();

            // Clone a copy of the criteria, setting a projection
            // to get the row count, this will get the total number of
            // items in the query using a select count(*)
            ICriteria countCriteria = CriteriaTransformer.Clone(criteria)
                .SetProjection(Projections.RowCountInt64());

            // Clear the ordering of the results
            countCriteria.Orders.Clear();

            // Clone a copy fo the criteria to get the page of data,
            // setting max and first result, this will get the page of data.s
            ICriteria pageCriteria = CriteriaTransformer.Clone(criteria)
                .SetMaxResults(pageSize)
                .SetFirstResult(startIndex);

            multiCriteria.Add(countCriteria);
            multiCriteria.Add(pageCriteria);

            var result = multiCriteria.List();
            var count = (long)((IList)result[0])[0];
            var list = ((IList)result[1]).Cast<TEntity>();

            // Create a new pagedresult object and populate it, use the paged query
            // to get the items, and the count query to get the total item count.
            var pagedResult = new PagedList<TEntity>(
                startIndex,
                pageSize,
                count,
                list);

            // Return the result.
            return pagedResult;
        }
    }
}