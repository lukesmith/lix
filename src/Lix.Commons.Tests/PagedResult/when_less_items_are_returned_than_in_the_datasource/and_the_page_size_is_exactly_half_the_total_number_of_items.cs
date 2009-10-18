using System;
using System.Linq;
using MbUnit.Framework;

namespace Lix.Commons.Tests.PagedResult.when_less_items_are_returned_than_in_the_datasource
{
    [TestFixture]
    public class and_the_page_size_is_exactly_half_the_total_number_of_items
    {
        private Commons.PagedResult pagedResult;

        [SetUp]
        public void SetUp()
        {
            this.pagedResult = new Commons.PagedResult(0, 10, 20, Enumerable.Range(0, 10));
        }

        [Test]
        public void should_have_a_next_page_number_of_2()
        {
            this.pagedResult.NextPageNumber.ShouldBeEqualTo(2);
        }

        [Test]
        public void should_have_a_total_of_2_pages()
        {
            this.pagedResult.TotalNumberPages.ShouldBeEqualTo(2);
        }

        [Test]
        public void should_have_a_result_size_exactly_half_the_total_item_count()
        {
            long remainder;
            Math.DivRem(this.pagedResult.TotalItemCount, this.pagedResult.PageSize, out remainder);

            remainder.ShouldBeEqualTo(0);
        }
    }
}