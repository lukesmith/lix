using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.NHibernate.Utilities.Tests.Examples;
using MbUnit.Framework;

namespace Lix.NHibernate.Utilities.Tests.SpecificationExecutors.when_executing_a_specification
{
    [TestFixture]
    public class using_the_default_critieria_specification_executor : using_a_nhibernate_specification_executor<DefaultNHibernateCriteriaSpecificationExecutor<Fish>, DefaultNHibernateCriteriaSpecification<Fish>>
    {
        protected override DefaultNHibernateCriteriaSpecificationExecutor<Fish> GetExecutor(DefaultNHibernateCriteriaSpecification<Fish> specification)
        {
            return new DefaultNHibernateCriteriaSpecificationExecutor<Fish>(specification, this.Session);
        }

        protected override DefaultNHibernateCriteriaSpecification<Fish> GetSpecificationForUniqueResult(string description)
        {
            return new FindFishWithDescriptionNHibernateCriteriaSpecification(description);
        }

        protected override DefaultNHibernateCriteriaSpecification<Fish> GetSpecificationForMultipleUniqueResult()
        {
            return new EmptyNHibernateCriteriaSpecification();
        }
    }
}