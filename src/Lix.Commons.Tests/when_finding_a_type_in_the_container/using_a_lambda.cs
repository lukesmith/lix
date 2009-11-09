using System.Linq;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_finding_a_type_in_the_container
{
    [TestFixture]
    public class using_a_lambda
    {
        [Test]
        public void should_find_the_correct_type()
        {
            var container = new Container();
            container.Register(typeof(FakeObject));

            container.FindTypeFor(x => x.GetInterfaces().Contains(typeof (IFakeInterface))).ShouldBeTheSameObjectAs(typeof(FakeObject));
        }
    }

    public interface IFakeInterface
    {
    }

    public class FakeObject : IFakeInterface
    {
    }
}
