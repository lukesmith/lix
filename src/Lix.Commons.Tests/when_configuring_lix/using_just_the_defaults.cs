using Lix.Commons.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_configuring_lix
{
    [TestFixture]
    public class using_just_the_defaults
    {
        [Test]
        public void should_use_the_default_specification_interceptor()
        {
            LixObjectFactory.CreateSpecificationInterceptor().ShouldBeTheSameTypeAs(typeof(DefaultSpecificationInterceptor));
        }
    }
}
