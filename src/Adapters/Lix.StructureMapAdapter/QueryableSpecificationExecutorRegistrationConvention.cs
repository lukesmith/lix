using System;
using Lix.Commons.Specifications;
using StructureMap.Configuration.DSL;

namespace Lix.StructureMapAdapter
{
    public class QueryableSpecificationExecutorRegistrationConvention : SpecificationExecutorRegistrationConventionBase
    {
        private static readonly Type OpenSpecificationInterfaceType = typeof(IQueryableSpecification<>);
        private static readonly Type OpenQueryableSpecificationExecutorType = typeof(QueryableSpecificationExecutor<>);

        public QueryableSpecificationExecutorRegistrationConvention(params Type[] namespacesContaining)
            : base(namespacesContaining)
        {
        }

        public override void Process(Type type, Registry registry)
        {
            this.Process(type, registry, OpenSpecificationInterfaceType, OpenQueryableSpecificationExecutorType);
        }
    }
}