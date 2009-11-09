using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace Lix.Futures
{
    public class LixNHibernateQueryProvider : NHibernateQueryProvider
    {
        public LixNHibernateQueryProvider(ISession session, QueryOptions queryOptions) : base(session, queryOptions)
        {
        }

        public LixNHibernateQueryProvider(ISession session, QueryOptions queryOptions, string entityName) : base(session, queryOptions, entityName)
        {
        }

        public override object Execute(Expression expression)
        {
            var lixExpression = new LixExpressionVisitor().Visit(expression);

            return base.Execute(lixExpression);
        }
    }
}