using System;
using Lix.Commons.Repositories.NHibernate;
using Lix.Commons.Tests.Repositories.NHibernate.Examples;
using MbUnit.Framework;
using Moq;

namespace Lix.Commons.Tests.Repositories.NHibernate
{
    [TestFixture]
    public class when_using_an_nhibernate_unit_of_work : nhibernate_test_setups
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException), Message = "Unable to commit before begin.")]
        public void should_throw_if_commit_called_and_unit_of_work_is_not_active()
        {
            var unitOfWork = new Mock<NHibernateUnitOfWork>(this.session);

            unitOfWork.Object.IsActive.ShouldBeEqualTo(false);
            unitOfWork.Object.Commit();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), Message = "Unable to rollback when not active.")]
        public void should_throw_if_rollback_called_and_unit_of_work_is_not_active()
        {
            var unitOfWork = new Mock<NHibernateUnitOfWork>(this.session);

            unitOfWork.Object.IsActive.ShouldBeEqualTo(false);
            unitOfWork.Object.Rollback();
        }

        [Test]
        public void should_commit_transaction_on_commit()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                unitOfWork.Commit();

                unitOfWork.Transaction.WasCommitted.ShouldBeEqualTo(true);
            }
        }

        [Test]
        public void should_rollback_transaction_on_dispose_if_not_committed_and_unit_of_work_has_begun()
        {
            var unitOfWork = new NHibernateUnitOfWork(this.session);
            unitOfWork.Begin();
            unitOfWork.Dispose();

            unitOfWork.Transaction.WasRolledBack.ShouldBeEqualTo(true);
        }

        [Test]
        public void should_not_throw_if_unit_of_work_has_not_begun_when_disposing()
        {
            var unitOfWork = new NHibernateUnitOfWork(this.session);
            unitOfWork.Dispose();
        }

        [Test]
        public void should_persist_the_data_on_commit()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();

                this.session.Save(new Fish());

                unitOfWork.Commit();
            }

            this.session.CreateCriteria(typeof(Fish)).List().Count.ShouldBeEqualTo(1);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), Message = "A unit of work has already begun for this session.")]
        public void should_not_be_able_to_have_multiple_units_of_work_active_for_the_same_session()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                this.session.Save(new Fish());

                using (var unitOfWork2 = new NHibernateUnitOfWork(this.session))
                {
                    unitOfWork2.Begin();
                    this.session.Save(new Fish());
                    unitOfWork2.Commit();
                }

                unitOfWork.Commit();
            }
        }

        [Test]
        public void should_not_persist_the_data_if_commit_is_not_called()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                this.session.Save(new Fish());
            }

            this.session.CreateCriteria(typeof(Fish)).List().Count.ShouldBeEqualTo(0);
        }

        [Test]
        public void should_not_persist_the_data_if_rollback_is_called()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                this.session.Save(new Fish());
                unitOfWork.Rollback();
            }

            this.session.CreateCriteria(typeof(Fish)).List().Count.ShouldBeEqualTo(0);
        }

        [Test]
        public void should_persist_the_data_if_commit_is_called()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                this.session.Save(new Fish());
                unitOfWork.Commit();
            }

            this.session.CreateCriteria(typeof(Fish)).List().Count.ShouldBeEqualTo(1);
        }

        [Test]
        public void should_become_inactive_when_the_unit_of_work_is_commited()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                this.session.Save(new Fish());
                unitOfWork.Commit();

                unitOfWork.IsActive.ShouldBeEqualTo(false);
            }
        }

        [Test]
        public void should_remain_active_when_the_unit_of_work_is_commited()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                this.session.Save(new Fish());
                unitOfWork.Commit(true);

                unitOfWork.IsActive.ShouldBeEqualTo(true);
            }
        }

        [Test]
        public void should_rollback_commit_not_called_when_using_multiple_commits_within_unit_of_work()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                this.session.Save(new Fish());
                unitOfWork.Commit(true);

                this.session.Save(new Fish());
            }

            this.session.CreateCriteria(typeof(Fish)).List().Count.ShouldBeEqualTo(1);
        }

        [Test]
        public void should_become_active_once_the_unit_of_work_has_begun()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                unitOfWork.IsActive.ShouldBeEqualTo(true);
                unitOfWork.Commit();
            }
        }

        [Test]
        public void should_be_able_to_run_consecutive_units_of_work_with_the_same_session()
        {
            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                this.session.Save(new Fish());
                unitOfWork.Commit();
            }

            this.session.CreateCriteria(typeof(Fish)).List().Count.ShouldBeEqualTo(1);

            using (var unitOfWork = new NHibernateUnitOfWork(this.session))
            {
                unitOfWork.Begin();
                this.session.Save(new Fish());
                unitOfWork.Commit();
            }

            this.session.CreateCriteria(typeof(Fish)).List().Count.ShouldBeEqualTo(2);
        }
    }
}