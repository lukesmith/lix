using Lix.Commons.Tests.Examples.Specifications;
using Lix.Commons.Tests.Repositories.NHibernate;
using MbUnit.Framework;
using NHibernate;

namespace Lix.Commons.Tests.when_getting_an_executor
{
    [TestFixture]
    public class with_an_isession_context : with_a_specification<TestNHibernateCriteriaSpecification>
    {
        private ISessionFactory sessionFactory;

        public override void SetUp()
        {
            base.SetUp();

            this.sessionFactory = SessionFactoryFactory.CreateSessionFactory();
        }

        protected override void RegisterContext()
        {
            this.SpecificationExecutionEngine.RegisterContext<ISession>(() => this.sessionFactory.OpenSession());
        }

        protected override TestNHibernateCriteriaSpecification CreateSpecification()
        {
            return new TestNHibernateCriteriaSpecification();
        }
    }
}