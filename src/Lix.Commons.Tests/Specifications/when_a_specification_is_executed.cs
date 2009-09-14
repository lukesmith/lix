using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Specifications.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Specifications
{
    [TestFixture]
    public class when_a_specification_is_executed
    {
        [Test]
        public void should_set_the_specification_property_on_the_executor()
        {
            var specificationExecutionEngine = new SpecificationExecutionEngine();
            specificationExecutionEngine.RegisterContext<IQueryable<Fish>>(() => new List<Fish>().AsQueryable());

            var specification = new TestSpecification();
            var executor = specificationExecutionEngine.GetExecutor<IQueryableSpecification<Fish>, Fish>(specification);

            executor.Specification.ShouldBeTheSameObjectAs(specification);
        }

        [Test]
        public void should_return_a_non_null_executor()
        {
            var specificationExecutionEngine = new SpecificationExecutionEngine();
            specificationExecutionEngine.RegisterContext<IQueryable<Fish>>(() => new List<Fish>().AsQueryable());

            var specification = new TestSpecification();
            var executor = specificationExecutionEngine.GetExecutor<IQueryableSpecification<Fish>, Fish>(specification);

            executor.ShouldSatisfy(x => x != null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), Message = "A context could not be found for the executor Lix.Commons.Specifications.QueryableSpecificationExecutor`1[TEntity]")]
        public void should_throw_if_the_required_context_is_not_registered()
        {
            var specificationExecutionEngine = new SpecificationExecutionEngine();

            var specification = new TestSpecification();
            specificationExecutionEngine.GetExecutor<IQueryableSpecification<Fish>, Fish>(specification);
        }
    }
}
