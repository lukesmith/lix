using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_a_specification_interceptor
{
    [TestFixture]
    public class of_type_queryable_has_been_defined
    {
        [TearDown]
        public void TearDown()
        {
            Specification.Interceptors.Clear();
        }

        [Test]
        public void should_get_the_replacement_specification()
        {
            var interceptWith = new EmptyFishQueryableSpecification2();
            Specification.Intercept<FindAll<Fish>>().With(interceptWith);

            var interceptBy = Specification.Interceptors.GetReplacement(new FindAll<Fish>());
            interceptBy.ShouldBeTheSameObjectAs(interceptWith);
        }
    }
}
