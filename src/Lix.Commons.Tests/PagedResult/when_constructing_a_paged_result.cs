using System.Linq;
using MbUnit.Framework;

namespace Lix.Commons.Tests.PagedResult
{
    [TestFixture]
    public class when_constructing_a_paged_result
    {
        [Test]
        [ExpectedArgumentException(Message = "Items cannot contain more items than the pageSize.")]
        public void should_throw_when_items_has_a_count_greater_than_the_pagesize()
        {
            new Commons.PagedResult(0, 10, 100, Enumerable.Range(0, 12));
        }

        [Test]
        public void should_create_when_items_has_a_count_equal_to_the_pagesize()
        {
            new Commons.PagedResult(0, 10, 100, Enumerable.Range(0, 10));
        }

        [Test]
        public void should_create_when_items_has_a_count_less_to_the_pagesize()
        {
            new Commons.PagedResult(0, 10, 100, Enumerable.Range(0, 9));
        }
    }
}
