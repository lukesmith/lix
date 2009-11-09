using Lix.Commons.Specifications;
using Lix.Commons.Tests.SpecificationExecutors.when_registering_a_specification_executor.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.SpecificationExecutors.when_registering_a_specification_executor
{
    [TestFixture]
    public class with_a_specification_type_that_implements_ispecification
    {
        [Test]
        public void should_succeed()
        {
            LixObjectFactory.Initialize(
                x =>
                x.RegisterSpecificationExecutor(typeof (IImplementISpecification),
                                                typeof (IImplementISpecificationExecutor)));
        }
    }
}