using Lix.Commons.Specifications;
using MbUnit.Framework;
using Lix.Commons.Specifications;

namespace Lix.Commons.Tests.when_registering_a_specification_executor
{
    [TestFixture]
    public class with_nothing_special
    {
        [Test]
        public void should_register_the_executor_to_the_container()
        {
            LixObjectFactory.Initialize(
                x =>
                x.RegisterSpecificationExecutor(typeof (INHibernateCriteriaSpecification),
                                                typeof (NHibernateCriteriaSpecificationExecutor<>)));

            LixObjectFactory.Container.FindTypeFor(typeof (INHibernateCriteriaSpecification)).ShouldBeTheSameObjectAs(
                typeof (NHibernateCriteriaSpecificationExecutor<>));
        }
    }
}
