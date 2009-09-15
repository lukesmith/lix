using System;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Specifications.Executors
{
    [TestFixture]
    public class when_a_specification_is_executed
    {
        [Test]
        [Pending]
        [ExpectedException(typeof(InvalidOperationException), "A constructor argument named 'context' could not be found.")]
        public void should_throw_if_the_executor_does_not_have_a_constructor_argument_named_context()
        {
            
        }
    }
}