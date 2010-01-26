using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Specifications.Executors;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.SpecificationExecutors.when_executing_a_specification
{
    [TestFixture]
    public class using_the_default_queryable_specification_executor : using_a_specification_executor<ISpecificationExecutor<IQueryableSpecification<Fish>, Fish>, IQueryableSpecification<Fish>>
    {
        private IQueryable<Fish> context;

        public override void PerformSetUp()
        {
            this.context = this.CreateDefaults().AsQueryable();
        }

        protected override ISpecificationExecutor<IQueryableSpecification<Fish>, Fish> GetExecutor(IQueryableSpecification<Fish> specification)
        {
            var repository = new Moq.Mock<IQueryRepository<Fish>>();
            repository.Setup(x => x.RepositoryQuery).Returns(this.context);
            return new QueryableSpecificationExecutor<Fish>(specification, repository.Object);
        }

        protected override IQueryableSpecification<Fish> GetSpecificationForMultipleUniqueResult()
        {
            return new EmptyFishQueryableSpecification();
        }

        protected override IQueryableSpecification<Fish> GetSpecificationForUniqueResult(string description)
        {
            return new FindFishWithDescriptionSpecification(description);
        }
    }
}