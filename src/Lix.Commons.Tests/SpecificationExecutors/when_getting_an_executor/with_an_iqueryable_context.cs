using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.SpecificationExecutors.when_getting_an_executor
{
    [TestFixture]
    public class with_an_iqueryable_context : with_a_specification<EmptyFishQueryableSpecification>
    {
        protected override void RegisterContext()
        {
            this.SpecificationExecutorFactory.RegisterContext<IQueryable<Fish>>(() => new List<Fish>().AsQueryable());
        }

        protected override EmptyFishQueryableSpecification CreateSpecification()
        {
            return new EmptyFishQueryableSpecification();
        }
    }
}