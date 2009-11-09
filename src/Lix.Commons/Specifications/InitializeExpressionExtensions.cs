using System;
using System.Linq;

namespace Lix.Commons.Specifications
{
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