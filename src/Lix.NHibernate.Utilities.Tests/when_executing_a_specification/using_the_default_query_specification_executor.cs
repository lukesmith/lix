using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;
using NHibernate;

namespace Lix.NHibernate.Utilities.Tests.when_executing_a_specification
{
    [TestFixture]
    public class using_the_default_query_specification_executor : using_a_nhibernate_specification_executor<DefaultNHibernateQuerySpecificationExecutor<Fish>, DefaultNHibernateQuerySpecification>
    {
        protected override DefaultNHibernateQuerySpecificationExecutor<Fish> GetExecutor(DefaultNHibernateQuerySpecification specification)
        {
            return new DefaultNHibernateQuerySpecificationExecutor<Fish>(specification, this.Session);
        }

        protected override DefaultNHibernateQuerySpecification GetSpecificationForUniqueResult(string description)
        {
            return new FindFishWithDescriptionNHibernateQuerySpecification(description);
        }

        protected override DefaultNHibernateQuerySpecification GetSpecificationForMultipleUniqueResult()
        {
            return new EmptyFishNHibernateQuerySpecification();
        }

        private class EmptyFishNHibernateQuerySpecification : DefaultNHibernateQuerySpecification
        {
            protected override string Query()
            {
                return "from f in Fish";
            }

            protected override IQuery Build(IQuery query)
            {
                return query;
            }
        }

        private class FindFishWithDescriptionNHibernateQuerySpecification : DefaultNHibernateQuerySpecification
        {
            private readonly string description;

            public FindFishWithDescriptionNHibernateQuerySpecification(string description)
            {
                this.description = description;
            }

            protected override string Query()
            {
                return "from f in Fish where f.Description = :description";
            }

            protected override IQuery Build(IQuery query)
            {
                return query.SetString("description", this.description);
            }
        }
    }
}