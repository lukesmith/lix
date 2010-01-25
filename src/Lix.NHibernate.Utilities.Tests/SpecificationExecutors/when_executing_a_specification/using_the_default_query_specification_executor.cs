using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;
using NHibernate;

namespace Lix.NHibernate.Utilities.Tests.SpecificationExecutors.when_executing_a_specification
{
    [TestFixture]
    public class using_the_default_query_specification_executor : using_a_nhibernate_specification_executor<ISpecificationExecutor<INHibernateQuerySpecification, Fish>, INHibernateQuerySpecification, IQuery>
    {
        protected override ISpecificationExecutor<INHibernateQuerySpecification, Fish> GetExecutor(INHibernateQuerySpecification specification)
        {
            var repository = new Moq.Mock<INHibernateRepository<Fish>>();
            repository.Setup(x => x.CurrentSession).Returns(this.Session);
            return new NHibernateQuerySpecificationExecutor<Fish>(specification, repository.Object);
        }

        protected override INHibernateQuerySpecification GetSpecificationForUniqueResult(string description)
        {
            return new FindFishWithDescriptionNHibernateQuerySpecification(description);
        }

        protected override INHibernateQuerySpecification GetSpecificationForMultipleUniqueResult()
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