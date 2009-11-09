using System.Linq;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests
{
    [TestFixture]
    public class when_using_generic_paged_results
    {
        private PagedResult<Fish> fishResults;

        [SetUp]
        public void SetUp()
        {
            var fishes = Enumerable.Repeat(new Fish(), 10);
            this.fishResults = new PagedResult<Fish>(0, 10, 20, fishes);
        }

        [Test]
        public void should_iterate_over_the_results()
        {
            int count = 0;

            foreach (var fish in this.fishResults)
            {
                count++;
            }

            count.ShouldBeEqualTo(10);
        }
    }
}
