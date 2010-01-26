using Lix.StructureMapAdapter;
using StructureMap;

namespace Lix.Examples.Repository
{
    public class Bootstrapper
    {
        public static void Configure()
        {
            ObjectFactory.Initialize(OnConfigure);
        }

        private static void OnConfigure(IInitializationExpression x)
        {
            x.Scan(cfg =>
                       {
                           cfg.TheCallingAssembly();

                           // Register the queryable specification executor convention
                           cfg.With(new QueryableSpecificationExecutorRegistrationConvention(typeof(Person)));

                           cfg.WithDefaultConventions();
                       });

            // include the LixRegistry
            x.IncludeRegistry(new LixRegistry());
        }
    }
}