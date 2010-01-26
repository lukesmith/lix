using Lix.Commons.Specifications.Executors;
using StructureMap.Configuration.DSL;

namespace Lix.StructureMapAdapter
{
    public class LixRegistry : Registry
    {
        public LixRegistry()
        {
            this.For<ISpecificationExecutorFactory>().Use<StructureMapSpecificationExecutorFactory>();
        }
    }
}