using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_a_specification_interceptor
{
    [TestFixture]
    public class has_not_been_defined
    {
        [Test]
        public void should_find_the_original_specification()
        {
            var specification = new FindAll<Fish>();

            var interceptBy = Specification.Interceptors.GetReplacement(specification);
            interceptBy.ShouldBeTheSameObjectAs(specification);
        }
    }
}