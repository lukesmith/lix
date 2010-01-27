using System;
using Lix.Commons.Specifications.Executors;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Lix.StructureMapAdapter
{
    public abstract class SpecificationExecutorRegistrationConventionBase : IRegistrationConvention
    {
        protected static readonly Type OpenSpecificationExecutorInterfaceType = typeof(ISpecificationExecutor<,>);
        
        public abstract void Process(Type type, Registry registry);

        protected void Process(Type type, Registry registry, Type specificationType, Type implementationType)
        {
            if (!type.IsAbstract && type.IsClass && !type.IsGenericTypeDefinition)
            {
                var closedSpecificationInterfaceType = specificationType.MakeGenericType(type);
                var closedSpecificationExecutorInterfaceType = OpenSpecificationExecutorInterfaceType.MakeGenericType(closedSpecificationInterfaceType, type);
                var closedSpecificationExecutorType = implementationType.MakeGenericType(type);

                registry.For(closedSpecificationExecutorInterfaceType).Use(closedSpecificationExecutorType);
            }
        }
    }
}