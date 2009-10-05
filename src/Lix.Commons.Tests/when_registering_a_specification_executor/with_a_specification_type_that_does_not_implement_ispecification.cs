using Lix.Commons.Specifications;
using Lix.Commons.Tests.when_registering_a_specification_executor.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_registering_a_specification_executor
{
    [TestFixture]
    public class with_a_specification_type_that_does_not_implement_ispecification
    {
        [Test]
        [ExpectedArgumentException]
        public void should_throw()
        {
            LixObjectFactory.Initialize(
                x =>
                x.RegisterSpecificationExecutor(typeof (IDontImplementISpecification),
                                                typeof (IImplementISpecificationExecutor)));
        }
    }
}
