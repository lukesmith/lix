using Lix.Commons;
using MbUnit.Framework;

namespace Lix.Futures.Tests
{
    [AssemblyFixture]
    public class AssemblySetUps
    {
        [SetUp]
        public void TestSetUp()
        {
            LixObjectFactory.Initialize();
        }

        [TearDown]
        public void TestTearDown()
        {
            LixObjectFactory.Reset();
        }
    }
}