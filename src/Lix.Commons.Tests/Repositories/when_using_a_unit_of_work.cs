using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories
{
    [TestFixture]
    public abstract class when_using_a_unit_of_work<TUnitOfWork, TEntity>
        where TUnitOfWork : IUnitOfWork
        where TEntity : new()
    {
        protected abstract TUnitOfWork CreateUnitOfWork();

        protected abstract IEnumerable<TEntity> List();

        protected abstract void SaveToUnitOfWork(TUnitOfWork unitOfWork, TEntity entity);

        [Test]
        [ExpectedException(typeof(InvalidOperationException), Message = "Unable to commit when not active.")]
        public void should_throw_if_commit_called_and_unit_of_work_is_not_active()
        {
            var unitOfWork = this.CreateUnitOfWork();

            unitOfWork.IsActive.ShouldBeEqualTo(false);
            unitOfWork.Commit();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), Message = "Unable to rollback when not active.")]
        public void should_throw_if_rollback_called_and_unit_of_work_is_not_active()
        {
            var unitOfWork = this.CreateUnitOfWork();

            unitOfWork.IsActive.ShouldBeEqualTo(false);
            unitOfWork.Rollback();
        }

        [Test]
        public void should_not_throw_if_unit_of_work_has_not_begun_when_disposing()
        {
            var unitOfWork = this.CreateUnitOfWork();
            unitOfWork.Dispose();
        }

        [Test]
        public void should_persist_the_data_on_commit()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();

                this.SaveToUnitOfWork(unitOfWork, new TEntity());

                unitOfWork.Commit();
            }

            this.List().Count().ShouldBeEqualTo(1);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), Message = "A unit of work has already begun for this session.")]
        public void should_not_be_able_to_have_multiple_units_of_work_active_for_the_same_session()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();
                this.SaveToUnitOfWork(unitOfWork, new TEntity());

                using (var unitOfWork2 = this.CreateUnitOfWork())
                {
                    unitOfWork2.Begin();
                    this.SaveToUnitOfWork(unitOfWork, new TEntity());
                    unitOfWork2.Commit();
                }

                unitOfWork.Commit();
            }
        }

        [Test]
        public void should_not_persist_the_data_if_commit_is_not_called()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();
                this.SaveToUnitOfWork(unitOfWork, new TEntity());
            }

            this.List().Count().ShouldBeEqualTo(0);
        }

        [Test]
        public void should_not_persist_the_data_if_rollback_is_called()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();
                this.SaveToUnitOfWork(unitOfWork, new TEntity());
                unitOfWork.Rollback();
            }

            this.List().Count().ShouldBeEqualTo(0);
        }

        [Test]
        public void should_persist_the_data_if_commit_is_called()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();
                this.SaveToUnitOfWork(unitOfWork, new TEntity());
                unitOfWork.Commit();
            }

            this.List().Count().ShouldBeEqualTo(1);
        }

        [Test]
        public void should_become_inactive_when_the_unit_of_work_is_commited()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();
                this.SaveToUnitOfWork(unitOfWork, new TEntity());
                unitOfWork.Commit();

                unitOfWork.IsActive.ShouldBeEqualTo(false);
            }
        }

        [Test]
        public void should_remain_active_when_the_unit_of_work_is_commited()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();
                this.SaveToUnitOfWork(unitOfWork, new TEntity());
                unitOfWork.Commit(true);

                unitOfWork.IsActive.ShouldBeEqualTo(true);
            }
        }

        [Test]
        public void should_rollback_commit_not_called_when_using_multiple_commits_within_unit_of_work()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();
                this.SaveToUnitOfWork(unitOfWork, new TEntity());
                unitOfWork.Commit(true);

                this.SaveToUnitOfWork(unitOfWork, new TEntity());
            }

            this.List().Count().ShouldBeEqualTo(1);
        }

        [Test]
        public void should_become_active_once_the_unit_of_work_has_begun()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();
                unitOfWork.IsActive.ShouldBeEqualTo(true);
                unitOfWork.Commit();
            }
        }

        [Test]
        public void should_be_able_to_run_consecutive_units_of_work_with_the_same_session()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();
                this.SaveToUnitOfWork(unitOfWork, new TEntity());
                unitOfWork.Commit();
            }

            this.List().Count().ShouldBeEqualTo(1);

            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();
                this.SaveToUnitOfWork(unitOfWork, new TEntity());
                unitOfWork.Commit();
            }

            this.List().Count().ShouldBeEqualTo(2);
        }
    }
}