using System;
using System.Collections.Generic;
using Lix.Commons.Specifications;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Lix.StructureMapAdapter
{
    public class QueryableSpecificationExecutorRegistrationConvention : IRegistrationConvention
    {
        private static readonly Type OpenSpecificationInterfaceType = typeof(IQueryableSpecification<>);
        private static readonly Type OpenSpecificationExecutorInterfaceType = typeof(ISpecificationExecutor<,>);
        private static readonly Type OpenQueryableSpecificationExecutorType = typeof(QueryableSpecificationExecutor<>);
        private readonly IList<string> namespaces = new List<string>();

        public QueryableSpecificationExecutorRegistrationConvention(params Type[] namespacesContaining)
        {
            foreach (var type in namespacesContaining)
            {
                namespaces.Add(type.Namespace);
            }
        }

        public void Process(Type type, Registry registry)
        {
            if (!type.IsAbstract && type.IsClass && namespaces.Contains(type.Namespace))
            {
                var closedSpecificationInterfaceType = OpenSpecificationInterfaceType.MakeGenericType(type);
                var closedSpecificationExecutorInterfaceType = OpenSpecificationExecutorInterfaceType.MakeGenericType(closedSpecificationInterfaceType, type);
                var closedQueryableSpecificationExecutorType = OpenQueryableSpecificationExecutorType.MakeGenericType(type);

                registry.For(closedSpecificationExecutorInterfaceType).Use(closedQueryableSpecificationExecutorType);
            }
        }
    }
}