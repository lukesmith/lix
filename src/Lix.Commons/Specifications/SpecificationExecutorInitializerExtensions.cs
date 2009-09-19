using Lix.Commons.Specifications.NHibernate;

namespace Lix.Commons.Specifications
{
    public static class SpecificationExecutorInitializerExtensions
    {
        public static ISpecificationExecutorInitializer WithDefaultNHibernateExecutors(this ISpecificationExecutorInitializer initializer)
        {
            SpecificationExecutorFactory.RegisterSpecificationExecutor(typeof(INHibernateCriteriaSpecification), typeof(NHibernateCriteriaSpecificationExecutor<>));

            return initializer;
        }
    }
}