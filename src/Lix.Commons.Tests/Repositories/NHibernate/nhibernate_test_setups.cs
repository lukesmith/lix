﻿using Lix.Commons.Repositories.NHibernate;
using Lix.Commons.Specifications;
using MbUnit.Framework;
using NHibernate;

namespace Lix.Commons.Tests.Repositories.NHibernate
{
    public abstract class nhibernate_test_setups
    {
        public ISessionFactory SessionFactory
        {
            get;
            private set;
        }
        
        protected ISession Session
        {
            get;
            set;
        }

        protected NHibernateUnitOfWork UnitOfWork
        {
            get;
            private set;
        }

        [FixtureSetUp]
        public void ClassSetup()
        {
            this.SessionFactory = SessionFactoryFactory.CreateSessionFactory();
        }

        [SetUp(Order = 0)]
        public void SetUp()
        {
            SpecificationExecutorFactory.Initialize().WithDefaultNHibernateExecutors();

            this.Session = this.SessionFactory.OpenSession();

            using (var tx = this.Session.BeginTransaction())
            {
                SessionFactoryFactory.BuildSchema(this.Session);

                tx.Commit();
            }

            this.UnitOfWork = new NHibernateUnitOfWork(this.Session);
            this.UnitOfWork.Begin();

            this.PerformSetUp();

            this.UnitOfWork.Commit(true);
        }

        protected virtual void PerformSetUp()
        {
        }

        [TearDown(Order = 0)]
        public virtual void TearDown()
        {
            SpecificationExecutorFactory.ClearExecutors();

            this.UnitOfWork.Commit();
            this.Session.Close();
            this.Session.Dispose();
        }
    }
}