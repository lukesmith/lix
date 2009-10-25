using Lix.Commons;
using Lix.Commons.Tests.SpecificationExecutors.when_getting_an_executor;
using Lix.NHibernate.Utilities.Tests.Examples;
using Lix.NHibernate.Utilities.Tests.Repositories;
using MbUnit.Framework;
using NHibernate;

namespace Lix.NHibernate.Utilities.Tests.SpecificationExecutors.when_getting_an_executor
{
    [TestFixture]
    public class with_an_isession_context : with_a_specification<EmptyNHibernateCriteriaSpecification>
    {
        private ISessionFactory sessionFactory;

        public override void SetUp()
        {
            LixObjectFactory.Initialize(x => x.WithDefaultNHibernateExecutors());

            base.SetUp();

            this.sessionFactory = SessionFactoryFactory.CreateSessionFactory();
        }

        protected override void RegisterContext()
        {
            this.SpecificationExecutorFactory.RegisterContext<ISession>(() => this.sessionFactory.OpenSession());
        }

        protected override EmptyNHibernateCriteriaSpecification CreateSpecification()
        {
            return new EmptyNHibernateCriteriaSpecification();
        }
    }
}