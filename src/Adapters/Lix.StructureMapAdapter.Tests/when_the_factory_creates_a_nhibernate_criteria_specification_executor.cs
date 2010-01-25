using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Machine.Specifications;
using Moq;
using Container=StructureMap.Container;
using It=Machine.Specifications.It;

namespace Lix.StructureMapAdapter.Tests
{
    [Subject(typeof(StructureMapSpecificationExecutorFactory), "CreateExecutor")]
    public class when_the_factory_creates_a_nhibernate_criteria_specification_executor
    {
        private static ISpecificationExecutorFactory _specificationExecutorFactory;
        private static ISpecificationExecutor<object> _executorInstance;

        private Establish context = () =>
                                        {
                                            var container = new Container();
                                            container.Configure(x => x.For<ISpecificationExecutor<INHibernateCriteriaSpecification<object>, object>>().Use<NHibernateCriteriaSpecificationExecutor<object>>());
                                            _specificationExecutorFactory = new StructureMapSpecificationExecutorFactory(container);
                                        };

        private Because of = () => _executorInstance = _specificationExecutorFactory.CreateExecutor<INHibernateCriteriaSpecification<object>, object, INHibernateRepository<object>>(new Mock<INHibernateCriteriaSpecification<object>>().Object, new Mock<INHibernateRepository<object>>().Object);

        private It should_not_be_null = () => _executorInstance.ShouldNotBeNull();

        private It should_be_instance_of = () => _executorInstance.ShouldBeOfType(typeof(NHibernateCriteriaSpecificationExecutor<object>));
    }
}