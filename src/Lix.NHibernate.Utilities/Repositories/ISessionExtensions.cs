using Lix.Commons.Specifications;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Linq;
using NHibernate.Linq.Visitors;

namespace Lix.Commons.Repositories
{
    public static class ISessionExtensions
    {
        /// <summary>
        /// Based on the specification will return a paged result set, will create two copies
        /// of the query 1 will be used to select the total count of items, the other
        /// used to select the page of data.
        /// The results will be wraped in a PagedResult object which will contain
        /// the items and total item count.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="session">The session.</param>
        /// <param name="specification">The specification.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A <see cref="PagedResult{TEntity}"/> collection.
        /// </returns>
        public static PagedResult<TEntity> PagedList<TEntity>(this ISession session, IQueryableSpecification<TEntity> specification, int startIndex, int pageSize)
        {
            var query = session.Linq<TEntity>();
            var specificationQuery = specification.Build(query);

            // This block of code was found in the NHibernate.Linq source   
            // using NHibernate.Linq.Visitors;   
            // using NHibernate.Engine;   
            System.Linq.Expressions.Expression expression = specificationQuery.Expression;
            expression = Evaluator.PartialEval(expression);
            expression = new BinaryBooleanReducer().Visit(expression);
            expression = new AssociationVisitor((ISessionFactoryImplementor)session.SessionFactory).Visit(expression);
            expression = new InheritanceVisitor().Visit(expression);
            expression = CollectionAliasVisitor.AssignCollectionAccessAliases(expression);
            expression = new PropertyToMethodVisitor().Visit(expression);
            expression = new BinaryExpressionOrderer().Visit(expression);
            var translator = new NHibernateQueryTranslator(session);
            object results = translator.Translate(expression, query.QueryOptions);

            // My LINQ query converted to ICriteria   
            var criteria = results as ICriteria;

            return criteria.PagedList<TEntity>(startIndex, pageSize);
        }
    }
}
