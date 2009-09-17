using System;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_getting_an_executor
{
    [TestFixture]
    public class when_getting_an_executor
    {
        [Test]
        [Pending]
        [ExpectedException(typeof(InvalidOperationException), "A constructor argument named 'context' could not be found.")]
        public void should_throw_if_the_executor_does_not_have_a_constructor_argument_named_context()
        {
            
        }
    }
}