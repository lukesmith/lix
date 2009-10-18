using System.Linq;
using MbUnit.Framework;

namespace Lix.Commons.Tests.PagedResult.when_the_result_set_has_zero_items
{
    [TestFixture]
    public class and_the_total_items_is_zero
    {
        private Commons.PagedResult pagedResult;

        [SetUp]
        public void SetUp()
        {
            this.pagedResult = new Commons.PagedResult(0, 0, 0, Enumerable.Empty<object>());
        }

        [Test]
        public void should_have_no_more_items()
        {
            this.pagedResult.HasMoreItems.ShouldBeEqualTo(false);
        }

        [Test]
        public void should_have_a_total_of_one_page()
        {
            this.pagedResult.TotalNumberPages.ShouldBeEqualTo(1);
        }

        [Test]
        public void should_have_a_page_number_of_one()
        {
            this.pagedResult.PageNumber.ShouldBeEqualTo(1);
        }

        [Test]
        public void should_have_a_zero_page_index()
        {
            this.pagedResult.PageIndex.ShouldBeEqualTo(0);
        }

        [Test]
        public void should_have_a_page_size_of_zero()
        {
            this.pagedResult.PageSize.ShouldBeEqualTo(0);
        }

        [Test]
        public void should_have_a_zero_total_item_count()
        {
            this.pagedResult.TotalItemCount.ShouldBeEqualTo(0);
        }
    }
}
