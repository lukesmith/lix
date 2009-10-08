using MbUnit.Framework;

namespace Lix.Commons.Tests.when_configuring_lix
{
    [TestFixture]
    public class and_resetting_the_factory
    {
        [SetUp]
        public void SetUp()
        {
            LixObjectFactory.Initialize();
        }

        [Test]
        public void should_create_a_new_container()
        {
            var containerBeforeReset = LixObjectFactory.Container;

            LixObjectFactory.Reset();

            LixObjectFactory.Container.ShouldSatisfy(x => x != containerBeforeReset);
        }
    }
}
