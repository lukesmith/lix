using System;
using Lix.Commons.Specifications;
using Lix.Commons.Specifications.Executors;
using StructureMap.Configuration.DSL;

namespace Lix.StructureMapAdapter
{
    public class NHibernateCriteriaSpecificationExecutorRegistrationConvention : SpecificationExecutorRegistrationConventionBase
    {
        private static readonly Type OpenSpecificationInterfaceType = typeof(INHibernateCriteriaSpecification<>);
        private static readonly Type OpenCriteriaSpecificationExecutorType = typeof(NHibernateCriteriaSpecificationExecutor<>);
        
        public override void Process(Type type, Registry registry)
        {
            this.Process(type, registry, OpenSpecificationInterfaceType, OpenCriteriaSpecificationExecutorType);
        }
    }
}