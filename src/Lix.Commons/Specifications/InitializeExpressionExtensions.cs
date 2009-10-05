using System;
using System.Linq;
using Lix.Commons.Specifications.NHibernate;

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

    public static class InitializeExpressionExtensions
    {
        public static IInitializeExpression RegisterSpecificationExecutor(this IInitializeExpression initializer, Type specificationType, Type specificationExecutorType)
        {
            if (!specificationType.GetInterfaces().Contains(typeof(ISpecification)))
            {
                throw new ArgumentException("specificationType");
            }

            if (!specificationExecutorType.GetInterfaces().Any(x => LixObjectFactory.DoTypesMatch(x, typeof(ISpecificationExecutor<>))))
            {
                throw new ArgumentException("specificationExecutorType");
            }

            LixObjectFactory.Container.RegisterForType(specificationType, specificationExecutorType);

            return initializer;
        }
    }
}