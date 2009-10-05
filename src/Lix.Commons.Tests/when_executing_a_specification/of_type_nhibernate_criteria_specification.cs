using Lix.Commons.Specifications;
using Lix.Commons.Specifications.NHibernate;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using Lix.Commons.Tests.Repositories.NHibernate;
using MbUnit.Framework;
using NHibernate;

namespace Lix.Commons.Tests.when_executing_a_specification
{
    [TestFixture]
    public class of_type_nhibernate_criteria_specification
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private SpecificationExecutorFactory executionFactory;

        [FixtureSetUp]
        public void ClassSetup()
        {
            this.sessionFactory = SessionFactoryFactory.CreateSessionFactory();
        }

        [SetUp]
        public void SetUp()
        {
            this.session = this.sessionFactory.OpenSession();

            using (var tx = this.session.BeginTransaction())
            {
                SessionFactoryFactory.BuildSchema(this.session);

                tx.Commit();
            }

            this.session.Save(new Fish {Description = "A slippery fish"});
            this.session.Flush();

            LixObjectFactory.Initialize(x => x.WithDefaultNHibernateExecutors());

            this.executionFactory = new SpecificationExecutorFactory();
            this.executionFactory.RegisterContext<ISession>(() => this.session);
        }

        [Test]
        public void should_get_the_entity()
        {
            const string description = "A slippery fish";
            var specification = new FindFishWithDescriptionNHibernateCriteriaSpecification(description);
            var executor = this.executionFactory.GetExecutor<INHibernateCriteriaSpecification, Fish>(specification);

            executor.Get().Description.ShouldBeEqualTo(description);
        }
    }
}