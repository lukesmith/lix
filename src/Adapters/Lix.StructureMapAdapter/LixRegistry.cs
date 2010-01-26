using Lix.Commons.Specifications;
using Lix.Commons.Specifications.Executors;
using StructureMap.Configuration.DSL;

namespace Lix.StructureMapAdapter
{
    public class LixRegistry : Registry
    {
        public LixRegistry()
        {
            this.For<ISpecificationExecutorFactory>().Use<StructureMapSpecificationExecutorFactory>();

            // Register default IQueryableSpecification<> instance
            this.For(typeof(IQueryableSpecification<>)).Use(typeof(FindAll<>));
        }
    }
}