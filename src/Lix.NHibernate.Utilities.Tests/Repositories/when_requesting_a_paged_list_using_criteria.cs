using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Tests;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.NHibernate.Utilities.Tests.Repositories
{
    [TestFixture]
    public class when_requesting_a_paged_list_using_criteria : nhibernate_test_setups
    {
        protected override void PerformSetUp()
        {
            this.UnitOfWork.Session.Save(new Fish());
            this.UnitOfWork.Session.Save(new Fish());
            this.UnitOfWork.Session.Save(new Fish());
            this.UnitOfWork.Session.Save(new Fish());
            this.UnitOfWork.Session.Save(new Fish());
            this.UnitOfWork.Session.Save(new Fish());
            this.UnitOfWork.Session.Save(new Fish());
            this.UnitOfWork.Session.Save(new Fish());
            this.UnitOfWork.Session.Save(new Fish());
            this.UnitOfWork.Session.Save(new Fish());
        }

        [Test]
        public void should_obtain_the_correct_total_item_count()
        {
            var result = this.UnitOfWork.Session.CreateCriteria(typeof(Fish)).PagedList<Fish>(0, 5);

            result.TotalItemCount.ShouldBeEqualTo(10);
        }

        [Test]
        public void should_obtain_the_correct_number_of_items_in_page()
        {
            var result = this.UnitOfWork.Session.CreateCriteria(typeof(Fish)).PagedList<Fish>(0, 5);

            result.Count().ShouldBeEqualTo(5);
        }
    }
}