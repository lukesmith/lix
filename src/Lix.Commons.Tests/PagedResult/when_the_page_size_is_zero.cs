using System.Linq;
using MbUnit.Framework;

namespace Lix.Commons.Tests.PagedResult
{
    [TestFixture]
    public class when_the_page_size_is_zero
    {
        private Commons.PagedResult pagedResult;

        [SetUp]
        public void SetUp()
        {
            this.pagedResult = new Commons.PagedResult(0, 0, 20, Enumerable.Range(0, 0));
        }

        [Test]
        public void should_have_a_total_number_of_pages_of_one()
        {
            this.pagedResult.TotalNumberPages.ShouldBeEqualTo(1);
        }

        [Test]
        public void should_not_have_more_items()
        {
            this.pagedResult.HasMoreItems.ShouldBeEqualTo(false);
        }
    }
}
