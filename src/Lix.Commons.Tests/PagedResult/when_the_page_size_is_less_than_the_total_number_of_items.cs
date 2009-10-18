using System.Linq;
using MbUnit.Framework;

namespace Lix.Commons.Tests.PagedResult
{
    [TestFixture]
    public class when_the_page_size_is_less_than_the_total_number_of_items
    {
        private Commons.PagedResult pagedResult;

        [SetUp]
        public void SetUp()
        {
            this.pagedResult = new Commons.PagedResult(0, 9, 10, Enumerable.Range(0, 9));
        }

        [Test]
        public void should_not_have_any_more_items()
        {
            this.pagedResult.HasMoreItems.ShouldBeEqualTo(true);
        }

        [Test]
        public void should_be_the_first_page()
        {
            this.pagedResult.IsFirstPage.ShouldBeEqualTo(true);
        }

        [Test]
        public void should_have_a_page_index_of_zero()
        {
            this.pagedResult.PageIndex.ShouldBeEqualTo(0);
        }

        [Test]
        public void should_have_the_correct_total_number_of_pages()
        {
            this.pagedResult.TotalNumberPages.ShouldBeEqualTo(2);
        }
    }
}
