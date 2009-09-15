using Lix.Commons.Tests.Repositories.NHibernate;
using Lix.Commons.Tests.Specifications.Examples;
using MbUnit.Framework;
using NHibernate;

namespace Lix.Commons.Tests.Specifications.Executors
{
    [TestFixture]
    public class when_a_specification_is_executed_with_an_isession_context : when_a_specification_is_executed<TestNHibernateCriteriaSpecification>
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