using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.NHibernate.Utilities.Tests.Examples;
using MbUnit.Framework;
using NHibernate;

namespace Lix.NHibernate.Utilities.Tests.SpecificationExecutors.when_executing_a_specification
{
    [TestFixture]
    public class using_the_default_critieria_specification_executor : using_a_nhibernate_specification_executor<ISpecificationExecutor<INHibernateCriteriaSpecification<Fish>, Fish>, INHibernateCriteriaSpecification<Fish>, ICriteria>
    {
        protected override ISpecificationExecutor<INHibernateCriteriaSpecification<Fish>, Fish> GetExecutor(INHibernateCriteriaSpecification<Fish> specification)
        {
            var repository = new Moq.Mock<INHibernateRepository<Fish>>();
            repository.Setup(x => x.CurrentSession).Returns(this.Session);
            return new DefaultNHibernateCriteriaSpecificationExecutor<Fish>(specification, repository.Object);
        }

        protected override INHibernateCriteriaSpecification<Fish> GetSpecificationForUniqueResult(string description)
        {
            return new FindFishWithDescriptionNHibernateCriteriaSpecification(description);
        }

        protected override INHibernateCriteriaSpecification<Fish> GetSpecificationForMultipleUniqueResult()
        {
            return new EmptyNHibernateCriteriaSpecification();
        }
    }
}