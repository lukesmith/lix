using Lix.Commons;
using Lix.Commons.Repositories;
using MbUnit.Framework;
using NHibernate;

namespace Lix.NHibernate.Utilities.Tests.Repositories
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
            LixObjectFactory.Initialize(x => x.WithDefaultNHibernateExecutors());

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
            this.UnitOfWork.Commit();
            this.Session.Close();
            this.Session.Dispose();
        }
    }
}