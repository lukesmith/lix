using NHibernate;
using NHibernate.Linq;

namespace Lix.Futures
{
    public static class LixNHibernateExtensions
    {
        /// <summary>
        /// Creates a new <see cref="T:Lix.Futures.LixNHibernateQueryProvider"/> object used to evaluate an expression tree.
        /// </summary>
        /// <typeparam name="T">An NHibernate entity type.</typeparam>
        /// <param name="session">An initialized <see cref="T:NHibernate.ISession"/> object.</param>
        /// <returns>An <see cref="T:Lix.Futures.LixNHibernateQueryProvider"/> used to evaluate an expression tree.</returns>
        public static INHibernateQueryable<T> Linq<T>(this ISession session)
        {
            var options = new QueryOptions();
            return new Query<T>(new LixNHibernateQueryProvider(session, options), options);
        }

        public static INHibernateQueryable<T> Linq<T>(this ISession session, string entityName)
        {
            var options = new QueryOptions();
            return new Query<T>(new LixNHibernateQueryProvider(session, options, entityName), options);
        }
    }
}