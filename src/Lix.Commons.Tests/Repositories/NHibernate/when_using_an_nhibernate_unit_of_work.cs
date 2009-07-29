﻿using System.Collections.Generic;
using Lix.Commons.Repositories.NHibernate;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;
using NHibernate;

namespace Lix.Commons.Tests.Repositories.NHibernate
{
    [TestFixture]
    public class when_using_an_nhibernate_unit_of_work : when_using_a_unit_of_work<NHibernateUnitOfWork>
    {
        private ISessionFactory sessionFactory;
        protected ISession session;

        [FixtureSetUp]
        public void ClassSetup()
        {
            this.sessionFactory = SessionFactoryFactory.CreateSessionFactory();
        }

        [SetUp(Order = 0)]
        public virtual void SetUp()
        {
            this.session = this.sessionFactory.OpenSession();
            SessionFactoryFactory.BuildSchema(this.session);
        }

        [TearDown(Order = 0)]
        public void TearDown()
        {
            this.session.Close();
            this.session.Dispose();
        }

        protected override NHibernateUnitOfWork GetUnitOfWork()
        {
            return new NHibernateUnitOfWork(this.session);
        }

        protected override void Save(Fish entity)
        {
            this.session.Save(entity);
        }

        protected override IEnumerable<Fish> List()
        {
            return this.session.CreateCriteria(typeof(Fish)).List<Fish>();
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
    }
}