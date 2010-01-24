using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.SpecificationExecutors;
using Lix.NHibernate.Utilities.Tests.Repositories;
using MbUnit.Framework;
using NHibernate;

namespace Lix.NHibernate.Utilities.Tests
{
    [TestFixture]
    public abstract class using_a_nhibernate_specification_executor<TExecutor, TSpecification, TResult> : using_a_specification_executor<TExecutor, TSpecification>
        where TExecutor : ISpecificationExecutor<TSpecification, Fish>
        where TSpecification : INHibernateSpecification<TResult>
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

        [FixtureSetUp]
        public void ClassSetup()
        {
            this.SessionFactory = SessionFactoryFactory.CreateSessionFactory();
        }

        public override void PerformSetUp()
        {
            this.Session = this.SessionFactory.OpenSession();
            this.Session.FlushMode = FlushMode.Commit;

            using (var tx = this.Session.BeginTransaction())
            {
                SessionFactoryFactory.BuildSchema(this.Session);

                tx.Commit();
            }

            using (var tx = this.Session.BeginTransaction())
            {
                this.CreateDefaults().ToList().ForEach(x => this.Session.Save(x));
                
                tx.Commit();
            }
        }

        public override void PerformTearDown()
        {
            this.Session.Close();
            this.Session.Dispose();
        }

// ReSharper disable RedundantOverridenMember
        public override void should_count_the_datasource_given_the_specification()
        {
            base.should_count_the_datasource_given_the_specification();
        }

        public override void should_get_the_entity_matching_the_specification()
        {
            base.should_get_the_entity_matching_the_specification();
        }

        public override void should_list_a_sub_selection_of_entities()
        {
            base.should_list_a_sub_selection_of_entities();
        }

        public override void should_list_all_the_entities()
        {
            base.should_list_all_the_entities();
        }

        public override void should_list_the_entities_starting_not_including_the_first()
        {
            base.should_list_the_entities_starting_not_including_the_first();
        }

        public override void should_return_whether_items_exist_given_the_specification()
        {
            base.should_return_whether_items_exist_given_the_specification();
        }
// ReSharper restore RedundantOverridenMember
    }
}