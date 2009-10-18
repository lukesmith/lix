using System.Linq;
using MbUnit.Framework;

namespace Lix.Commons.Tests.PagedResult.when_less_items_are_returned_than_in_the_datasource
{
    [TestFixture]
    public class and_the_start_index_is_zero
    {
        private Commons.PagedResult pagedResult;

        [SetUp]
        public void SetUp()
        {
            this.pagedResult = new Commons.PagedResult(0, 10, 20, Enumerable.Range(0, 10));
        }

        [Test]
        public void should_not_have_more_items()
        {
            this.pagedResult.HasMoreItems.ShouldBeEqualTo(true);
        }

        [Test]
        public void should_have_a_total_of_2_pages()
        {
            this.pagedResult.TotalNumberPages.ShouldBeEqualTo(2);
        }

        [Test]
        public void should_have_a_next_page_number_of_2()
        {
            this.pagedResult.NextPageNumber.ShouldBeEqualTo(2);
        }

        [Test]
        public void should_have_a_next_page_index_of_1()
        {
            this.pagedResult.NextPageIndex.ShouldBeEqualTo(1);
        }
    }
}