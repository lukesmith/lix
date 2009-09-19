using Lix.Commons.Specifications;
using Lix.Commons.Tests.when_registering_a_specification_executor.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_registering_a_specification_executor
{
    [TestFixture]
    public class that_has_already_had_the_same_specification_type_registered
    {
        [SetUp]
        public void SetUp()
        {
            SpecificationExecutorFactory.RegisterSpecificationExecutor(typeof(IImplementISpecification), typeof(IImplementISpecificationExecutor));
        }

        [TearDown]
        public void TearDown()
        {
            SpecificationExecutorFactory.ClearExecutors();
        }

        [Test]
        public void should_replace_the_previous_specification_type()
        {
            SpecificationExecutorFactory.RegisterSpecificationExecutor(typeof(IImplementISpecification), typeof(IImplementISpecificationExecutor));
        }
    }
}
