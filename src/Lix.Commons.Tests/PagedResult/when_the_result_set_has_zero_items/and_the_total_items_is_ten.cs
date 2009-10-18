using System.Linq;
using MbUnit.Framework;

namespace Lix.Commons.Tests.PagedResult.when_the_result_set_has_zero_items
{
    [TestFixture]
    public class and_the_total_items_is_ten
    {
        private Commons.PagedResult pagedResult;

        [SetUp]
        public void SetUp()
        {
            this.pagedResult = new Commons.PagedResult(0, 0, 10, Enumerable.Empty<object>());
        }

        [Test]
        public void should_have_a_total_item_count_of_ten()
        {
            this.pagedResult.TotalItemCount.ShouldBeEqualTo(10);
        }

        [Test]
        public void should_have_a_total_number_of_pages_of_one()
        {
            this.pagedResult.TotalNumberPages.ShouldBeEqualTo(1);
        }
    }
}
