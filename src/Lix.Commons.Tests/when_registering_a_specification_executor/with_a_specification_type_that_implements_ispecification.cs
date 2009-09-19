using Lix.Commons.Specifications;
using Lix.Commons.Tests.when_registering_a_specification_executor.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_registering_a_specification_executor
{
    [TestFixture]
    public class with_a_specification_type_that_implements_ispecification
    {
        [TearDown]
        public void TearDown()
        {
            SpecificationExecutorFactory.ClearExecutors();
        }

        [Test]
        public void should_succeed()
        {
            SpecificationExecutorFactory.RegisterSpecificationExecutor(typeof(IImplementISpecification), typeof(IImplementISpecificationExecutor));
        }
    }
}
