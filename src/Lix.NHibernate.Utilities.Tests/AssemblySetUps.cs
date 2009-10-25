using Lix.Commons;
using MbUnit.Framework;

namespace Lix.NHibernate.Utilities.Tests
{
    [AssemblyFixture]
    public class AssemblySetUps
    {
        [SetUp]
        public void TestSetUp()
        {
            LixObjectFactory.Initialize(x => x.WithDefaultNHibernateExecutors());
        }

        [TearDown]
        public void TestTearDown()
        {
            LixObjectFactory.Reset();
        }
    }
}