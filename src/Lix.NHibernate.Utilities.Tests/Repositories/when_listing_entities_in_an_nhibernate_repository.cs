using Lix.Commons.Repositories;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Repositories;
using MbUnit.Framework;
using NHibernate;

namespace Lix.NHibernate.Utilities.Tests.Repositories
{
    [TestFixture]
    public class when_listing_entities_in_an_nhibernate_repository : when_listing_entities_in_a_repository<NHibernateUnitOfWork, NHibernateRepository<Fish>>
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

        [FixtureSetUp]
        public void ClassSetup()
        {
            this.SessionFactory = SessionFactoryFactory.CreateSessionFactory();
        }

        public override void SetUp()
        {
            this.Session = this.SessionFactory.OpenSession();

            using (var tx = this.Session.BeginTransaction())
            {
                SessionFactoryFactory.BuildSchema(this.Session);

                tx.Commit();
            }

            base.SetUp();
        }

        public override void TearDown()
        {
            this.UnitOfWork.Commit();
            this.Session.Close();
            this.Session.Dispose();
            this.Session = null;

            base.TearDown();
        }

        protected override void SaveToUnitOfWork(NHibernateUnitOfWork unitOfWork, Fish entity)
        {
            unitOfWork.Session.Save(entity);
        }

        protected override NHibernateRepository<Fish> CreateRepository()
        {
            return new NHibernateRepository<Fish>(this.UnitOfWork);
        }

        protected override NHibernateUnitOfWork CreateUnitOfWork()
        {
            return new NHibernateUnitOfWork(this.Session);
        }
    }
}