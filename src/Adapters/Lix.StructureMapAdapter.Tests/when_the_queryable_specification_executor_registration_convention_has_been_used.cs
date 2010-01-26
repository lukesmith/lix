using Lix.Commons.Specifications.Executors;
using Machine.Specifications;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Lix.StructureMapAdapter.Tests
{
    [Subject(typeof(QueryableSpecificationExecutorRegistrationConvention), "Process")]
    public class when_the_queryable_specification_executor_registration_convention_has_been_used
    {
        private static QueryableSpecificationExecutorRegistrationConvention _convention;
        private static Registry _registry;
        private static PluginGraph _pluginGraph;

        private Establish context = () =>
                                        {
                                            _convention = new QueryableSpecificationExecutorRegistrationConvention();
                                            _registry = new Registry();
                                            _convention.Process(typeof(StubEntity), _registry);
                                        };

        private Because of = () =>
                                 {
                                     _pluginGraph = _registry.Build();
                                 };

        private It should_contain_an_instance_of_the_iqueryable_specification_executor = () => _pluginGraph.PluginFamilies[typeof(IQueryableSpecificationExecutor<StubEntity>)].ShouldNotBeNull();

        public class StubEntity
        {

        }
    }
}