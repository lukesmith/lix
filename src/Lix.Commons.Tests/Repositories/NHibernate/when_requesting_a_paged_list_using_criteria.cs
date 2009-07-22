using System.Linq;
using Lix.Commons.Repositories.NHibernate;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;
using NHibernate.Linq;

namespace Lix.Commons.Tests.Repositories.NHibernate
{
    [TestFixture]
    public class when_requesting_a_paged_list_using_linq : nhibernate_test_setups
    {
        public override void SetUp()
        {
            base.SetUp();

            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
        }

        [Test]
        public void should_obtain_the_correct_total_item_count()
        {
            var result = this.session.PagedList(Specification.Empty<Fish>(), 0, 5);

            result.TotalItemCount.ShouldBeEqualTo(10);
        }

        [Test]
        public void should_obtain_the_correct_number_of_items_in_page()
        {
            var result = this.session.PagedList(Specification.Empty<Fish>(), 0, 5);

            result.Count().ShouldBeEqualTo(5);
        }
    }

    [TestFixture]
    public class when_requesting_a_paged_list_using_criteria : nhibernate_test_setups
    {
        public override void SetUp()
        {
            base.SetUp();

            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
            this.session.Save(new Fish());
        }

        [Test]
        public void should_obtain_the_correct_total_item_count()
        {
            var result = this.session.CreateCriteria(typeof(Fish)).PagedList<Fish>(0, 5);

            result.TotalItemCount.ShouldBeEqualTo(10);
        }

        [Test]
        public void should_obtain_the_correct_number_of_items_in_page()
        {
            var result = this.session.CreateCriteria(typeof(Fish)).PagedList<Fish>(0, 5);

            result.Count().ShouldBeEqualTo(5);
        }
    }
}
