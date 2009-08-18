using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lix.Commons.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Like<T>(this IQueryable<T> source, Expression<Func<T, string>> field, string value, ComparisonType comparisonType)
        {
            var p = field.Parameters[0];

            return
                source.Where(Expression.Lambda<Func<T, bool>>(
                        Expression.Call(field.Body, comparisonType.ToString(), null, Expression.Constant(value)), p));
        }
    }
}