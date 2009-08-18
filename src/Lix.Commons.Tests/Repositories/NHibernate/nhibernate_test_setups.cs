using System;
using Lix.Commons.Repositories.NHibernate;
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
        
        private ISession Session
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

            HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();
        }

        [SetUp(Order = 0)]
        public virtual void SetUp()
        {
            this.Session = this.SessionFactory.OpenSession();
            SessionFactoryFactory.BuildSchema(this.Session);

            this.UnitOfWork = new NHibernateUnitOfWork(this.Session);
        }

        [TearDown(Order = 0)]
        public void TearDown()
        {
            this.Session.Close();
            this.Session.Dispose();
        }
    }
}