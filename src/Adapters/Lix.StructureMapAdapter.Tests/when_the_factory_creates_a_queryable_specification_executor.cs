using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Specifications.Executors;
using Machine.Specifications;
using Moq;
using Container=StructureMap.Container;
using It=Machine.Specifications.It;

namespace Lix.StructureMapAdapter.Tests
{
    [Subject(typeof(StructureMapSpecificationExecutorFactory), "CreateExecutor")]
    public class when_the_factory_creates_a_queryable_specification_executor
    {
        private static ISpecificationExecutorFactory _specificationExecutorFactory;
        private static ISpecificationExecutor<object> _executorInstance;

        private Establish context = () =>
        {
            var container = new Container();
            container.Configure(x => x.For<ISpecificationExecutor<IQueryableSpecification<object>, object>>().Use<QueryableSpecificationExecutor<object>>());
            _specificationExecutorFactory = new StructureMapSpecificationExecutorFactory(container);
        };

        private Because of = () => _executorInstance = _specificationExecutorFactory.CreateExecutor<IQueryableSpecification<object>, object, ILinqEnabledRepository<object>>(new Mock<IQueryableSpecification<object>>().Object, new Mock<ILinqEnabledRepository<object>>().Object);

        private It should_not_be_null = () => _executorInstance.ShouldNotBeNull();

        private It should_be_instance_of = () => _executorInstance.ShouldBeOfType(typeof(QueryableSpecificationExecutor<object>));
    }
}
