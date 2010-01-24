using Lix.Commons.Specifications;
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