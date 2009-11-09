using System.Linq.Expressions;
using NHibernate.Linq.Visitors;

namespace Lix.Futures
{
    public class LixExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression expr)
        {
            this.Visit(expr.Arguments[0]);

            switch (expr.Method.Name)
            {
                case "Where":
                    return HandleWhereCall(expr);
            }

            return expr;
        }

        private static Expression HandleWhereCall(MethodCallExpression call)
        {
            var e = call.Arguments[1];

            return Expression.Call(call.Arguments[0],
                                    call.Method,
                                    new[] {call.Arguments[0], new LixWhereArgumentsVisitor().Visit(e)});
        }
    }
}