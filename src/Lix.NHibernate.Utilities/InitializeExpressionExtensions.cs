using Lix.Commons.Specifications;

namespace Lix.Commons
{
    public static class NHibernateInitializeExpressionExtensions
    {
        public static IInitializeExpression WithDefaultNHibernateExecutors(this IInitializeExpression initializer)
        {
            initializer.RegisterSpecificationExecutor(typeof(INHibernateCriteriaSpecification), typeof(DefaultNHibernateCriteriaSpecificationExecutor<>));
            initializer.RegisterSpecificationExecutor(typeof(INHibernateQuerySpecification), typeof(DefaultNHibernateQuerySpecificationExecutor<>));

            return initializer;
        }
    }
}