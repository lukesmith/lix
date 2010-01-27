using System;
using Lix.Commons.Specifications;
using Lix.Commons.Specifications.Executors;
using StructureMap.Configuration.DSL;

namespace Lix.StructureMapAdapter
{
    public class QueryableSpecificationExecutorRegistrationConvention : SpecificationExecutorRegistrationConventionBase
    {
        private static readonly Type OpenSpecificationInterfaceType = typeof(IQueryableSpecification<>);
        private static readonly Type OpenQueryableSpecificationExecutorType = typeof(QueryableSpecificationExecutor<>);

        public override void Process(Type type, Registry registry)
        {
            this.Process(type, registry, OpenSpecificationInterfaceType, OpenQueryableSpecificationExecutorType);
        }
    }
}