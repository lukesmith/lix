namespace Lix.Commons.Specifications
{
    public static class NHibernateInitializeExpressionExtensions
    {
        public static IInitializeExpression WithDefaultNHibernateExecutors(this IInitializeExpression initializer)
        {
            initializer.RegisterSpecificationExecutor(typeof(INHibernateCriteriaSpecification), typeof(NHibernateCriteriaSpecificationExecutor<>));

            return initializer;
        }
    }
}