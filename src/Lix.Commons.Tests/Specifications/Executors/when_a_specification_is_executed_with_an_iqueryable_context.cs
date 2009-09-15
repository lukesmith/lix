using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Specifications.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Specifications.Executors
{
    [TestFixture]
    public class when_a_specification_is_executed_with_an_iqueryable_context : when_a_specification_is_executed<TestSpecification>
    {
        protected override void RegisterContext()
        {
            this.SpecificationExecutionEngine.RegisterContext<IQueryable<Fish>>(() => new List<Fish>().AsQueryable());
        }

        protected override TestSpecification CreateSpecification()
        {
            return new TestSpecification();
        }
    }
}