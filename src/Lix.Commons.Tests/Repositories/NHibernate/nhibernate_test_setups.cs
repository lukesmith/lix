using MbUnit.Framework;
using NHibernate;

namespace Lix.Commons.Tests.Repositories.NHibernate
{
    public abstract class nhibernate_test_setups
    {
        private ISessionFactory sessionFactory;
        protected ISession session;

        [FixtureSetUp]
        public void ClassSetup()
        {
            this.sessionFactory = SessionFactory.CreateSessionFactory();

            HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();
        }

        [SetUp(Order = 0)]
        public virtual void SetUp()
        {
            this.session = this.sessionFactory.OpenSession();
            SessionFactory.BuildSchema(this.session);
        }

        [TearDown(Order = 0)]
        public void TearDown()
        {
            this.session.Close();
            this.session.Dispose();
        }
    }
}