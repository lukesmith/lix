using Lix.Commons.Specifications;
using Lix.Commons.Tests.when_registering_a_specification_executor.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_registering_a_specification_executor
{
    [TestFixture]
    public class with_a_specification_executor_type_that_does_not_implement_ispecificationexecutor
    {
        [TearDown]
        public void TearDown()
        {
            SpecificationExecutorFactory.ClearExecutors();
        }

        [Test]
        [ExpectedArgumentException]
        public void should_throw()
        {
            SpecificationExecutorFactory.RegisterSpecificationExecutor(typeof(IImplementISpecification), typeof(IDontImplementISpecificationExecutor));
        }
    }
}
