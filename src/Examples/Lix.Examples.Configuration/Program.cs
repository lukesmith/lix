using Lix.Commons.Repositories;
using Lix.StructureMapAdapter;
using StructureMap;

namespace Lix.Examples.Configuration
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectFactory.Configure(OnConfigure);

            ObjectFactory.AssertConfigurationIsValid();
        }

        private static void OnConfigure(ConfigurationExpression x)
        {
            x.Scan(cfg =>
                       {
                           cfg.TheCallingAssembly();

                           x.For(typeof(IReportingRepository<>)).Use(typeof(InMemoryRepository<>));

                           cfg.WithDefaultConventions();
                       });

            // include the LixRegistry
            x.IncludeRegistry(new LixRegistry());
        }
    }
}
