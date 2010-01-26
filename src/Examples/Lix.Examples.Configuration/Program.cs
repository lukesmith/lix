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

                           // Register the queryable specification executor convention
                           cfg.With(new QueryableSpecificationExecutorRegistrationConvention());
                           // Register the nhibernate specification executor convention for types in the same namespace as the type Person
                           cfg.With(new NHibernateCriteriaSpecificationExecutorRegistrationConvention(typeof (Person)));

                           cfg.WithDefaultConventions();
                       });

            // include the LixRegistry
            x.IncludeRegistry(new LixRegistry());
        }
    }
}
