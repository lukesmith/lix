using Lix.Commons.Repositories;
using Lix.Commons.Tests;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;
using NHibernate.Criterion;

namespace Lix.NHibernate.Utilities.Tests.Repositories
{
    [TestFixture]
    public class when_doing_a_count_on_criteria : nhibernate_test_setups
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
        public void should_get_the_correct_count()
        {
            var result = this.UnitOfWork.Session.CreateCriteria(typeof(Fish)).Count();

            result.ShouldBeEqualTo(10);
        }

        [Test]
        public void should_get_the_correct_count_with_order_by_on_the_criteria()
        {
            var criteria = this.UnitOfWork.Session.CreateCriteria(typeof (Fish));
            criteria.AddOrder(Order.Desc("Id"));

            var result = criteria.Count();
            result.ShouldBeEqualTo(10);
        }
    }
}