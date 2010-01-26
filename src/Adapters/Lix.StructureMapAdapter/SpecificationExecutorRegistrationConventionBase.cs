using System;
using System.Collections.Generic;
using Lix.Commons.Specifications.Executors;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Lix.StructureMapAdapter
{
    public abstract class SpecificationExecutorRegistrationConventionBase : IRegistrationConvention
    {
        protected static readonly Type OpenSpecificationExecutorInterfaceType = typeof(ISpecificationExecutor<,>);
        private readonly IList<string> namespaces = new List<string>();

        protected SpecificationExecutorRegistrationConventionBase(params Type[] namespacesContaining)
        {
            foreach (var type in namespacesContaining)
            {
                this.namespaces.Add(type.Namespace);
            }
        }

        public abstract void Process(Type type, Registry registry);

        protected void Process(Type type, Registry registry, Type specificationType, Type implementationType)
        {
            if (!type.IsAbstract && type.IsClass && this.namespaces.Contains(type.Namespace))
            {
                var closedSpecificationInterfaceType = specificationType.MakeGenericType(type);
                var closedSpecificationExecutorInterfaceType = OpenSpecificationExecutorInterfaceType.MakeGenericType(closedSpecificationInterfaceType, type);
                var closedQueryableSpecificationExecutorType = implementationType.MakeGenericType(type);

                registry.For(closedSpecificationExecutorInterfaceType).Use(closedQueryableSpecificationExecutorType);
            }
        }
    }
}