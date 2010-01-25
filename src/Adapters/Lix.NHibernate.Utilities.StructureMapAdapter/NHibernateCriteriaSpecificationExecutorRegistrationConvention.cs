using System;
using Lix.Commons.Specifications;
using StructureMap.Configuration.DSL;

namespace Lix.StructureMapAdapter
{
    public class NHibernateCriteriaSpecificationExecutorRegistrationConvention : SpecificationExecutorRegistrationConventionBase
    {
        private static readonly Type OpenSpecificationInterfaceType = typeof(INHibernateCriteriaSpecification<>);
        private static readonly Type OpenCriteriaSpecificationExecutorType = typeof(NHibernateCriteriaSpecificationExecutor<>);
        
        public NHibernateCriteriaSpecificationExecutorRegistrationConvention(params Type[] namespacesContaining)
            : base(namespacesContaining)
        {
        }

        public override void Process(Type type, Registry registry)
        {
            this.Process(type, registry, OpenSpecificationInterfaceType, OpenCriteriaSpecificationExecutorType);
        }
    }
}