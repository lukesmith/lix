using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.NHibernate
{
    [TestFixture]
    public class when_requesting_a_paged_list_using_linq : nhibernate_test_setups
    {
        protected override void PerformSetUp()
        {
            this.UnitOfWork.Save(new Fish());
            this.UnitOfWork.Save(new Fish());
            this.UnitOfWork.Save(new Fish());
            this.UnitOfWork.Save(new Fish());
            this.UnitOfWork.Save(new Fish());
            this.UnitOfWork.Save(new Fish());
            this.UnitOfWork.Save(new Fish());
            this.UnitOfWork.Save(new Fish());
            this.UnitOfWork.Save(new Fish());
            this.UnitOfWork.Save(new Fish());
        }

        [Test]
        public void should_obtain_the_correct_total_item_count()
        {
            var result = this.UnitOfWork.Session.PagedList(Specification.Empty<Fish>(), 0, 5);

            result.TotalItemCount.ShouldBeEqualTo(10);
        }

        [Test]
        public void should_obtain_the_correct_number_of_items_in_page()
        {
            var result = this.UnitOfWork.Session.PagedList(Specification.Empty<Fish>(), 0, 5);

            result.Count().ShouldBeEqualTo(5);
        }
    }
}