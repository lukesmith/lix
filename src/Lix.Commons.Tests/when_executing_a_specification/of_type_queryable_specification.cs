using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_executing_a_specification
{
    [TestFixture]
    public class of_type_queryable_specification
    {
        private SpecificationExecutionEngine executionEngine;
        private IQueryable<Fish> context;

        [SetUp]
        public void Setup()
        {
            this.context = new List<Fish>().AsQueryable();

            this.executionEngine = new SpecificationExecutionEngine();
            this.executionEngine.RegisterContext<IQueryable<Fish>>(() => this.context);
        }

        [Test]
        public void should_find_the_queryable_specification_executor()
        {
            var specification = new EmptyFishQueryableSpecification();
            var executor = this.executionEngine.GetExecutor<IQueryableSpecification<Fish>, Fish>(specification);

            var expectedExecutorType = typeof(QueryableSpecificationExecutor<>).MakeGenericType(typeof(Fish));
            executor.GetType().ShouldBeEqualTo(expectedExecutorType);
        }
    }
}