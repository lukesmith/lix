using Lix.Commons;
using MbUnit.Framework;

namespace Lix.NHibernate.Utilities.Tests
{
    [AssemblyFixture]
    public class AssemblySetUps
    {
        [TearDown]
        public void TestTearDown()
        {
            LixObjectFactory.Reset();
        }
    }
}