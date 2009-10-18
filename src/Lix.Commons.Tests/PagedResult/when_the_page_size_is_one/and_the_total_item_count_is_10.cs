using System.Linq;
using MbUnit.Framework;

namespace Lix.Commons.Tests.PagedResult.when_the_page_size_is_one
{
    [TestFixture]
    public class and_the_total_item_count_is_10
    {
        private Commons.PagedResult pagedResult;

        [SetUp]
        public void SetUp()
        {
            this.pagedResult = new Commons.PagedResult(0, 1, 10, Enumerable.Range(0, 1));
        }

        [Test]
        public void should_have_a_total_number_of_pages_of_10()
        {
            this.pagedResult.TotalNumberPages.ShouldBeEqualTo(10);
        }
    }
}
