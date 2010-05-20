using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using ExpressionVisitor = NHibernate.Linq.Visitors.ExpressionVisitor;

namespace Lix.Futures
{
    public class LixWhereArgumentsVisitor : ExpressionVisitor
    {
        private readonly Func<string, string> escapeSearchValue = x => Regex.Replace(x, @"[_%[]{1}", m => "\\" + m.Value);

        protected override Expression VisitMethodCall(MethodCallExpression expr)
        {
            if (expr.Method.Name == "Like")
            {
                var comparisonTypeExpression = expr.Arguments[2] as ConstantExpression;
                
                var searchValueExpression = expr.Arguments[1];
                Expression searchValue;

                switch (searchValueExpression.NodeType)
                {
                    case ExpressionType.Constant:
                        {
                            searchValue = HandleConstantExpression(searchValueExpression as ConstantExpression);
                            break;
                        }
                    case ExpressionType.MemberAccess:
                        {
                            searchValue = HandleMemberExpression(searchValueExpression as MemberExpression);
                            break;
                        }
                    case ExpressionType.Invoke:
                        {
                            searchValue = HandleInvokeExpression(searchValueExpression as InvocationExpression);
                            break;
                        }
                    case ExpressionType.Call:
                        {
                            searchValue = HandleCallExpression(searchValueExpression as MethodCallExpression);
                            break;
                        }
                    default:
                        throw new NotSupportedException(string.Format("Expression '{0}' used for the searchValue is not supported.", searchValueExpression.NodeType));
                }

                return Expression.Call(expr.Arguments[0], comparisonTypeExpression.Value.ToString(), null, searchValue);
            }

            return base.VisitMethodCall(expr);
        }

        private Expression HandleCallExpression(MethodCallExpression expression)
        {
            return Expression.Call(this.escapeSearchValue.Method, expression);
        }

        private Expression HandleInvokeExpression(InvocationExpression expression)
        {
            return Expression.Call(this.escapeSearchValue.Method, expression);
        }

        private Expression HandleMemberExpression(MemberExpression expr)
        {
            return Expression.Call(this.escapeSearchValue.Method, expr);
        }

        private ConstantExpression HandleConstantExpression(ConstantExpression expr)
        {
            return Expression.Constant(this.escapeSearchValue(expr.Value as string));
        }
    }
}