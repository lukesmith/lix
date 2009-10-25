using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.SpecificationExecutors.when_getting_an_executor
{
    [TestFixture]
    public class of_type_default_queryable_specification_executor
    {
        private SpecificationExecutorFactory executionFactory;
        private IQueryable<Fish> context;

        [SetUp]
        public void Setup()
        {
            this.context = new List<Fish>().AsQueryable();

            this.executionFactory = new SpecificationExecutorFactory();
            this.executionFactory.RegisterContext<IQueryable<Fish>>(() => this.context);
        }

        [Test]
        public void should_find_the_queryable_specification_executor()
        {
            var specification = new EmptyFishQueryableSpecification();
            var executor = this.executionFactory.GetExecutor<IQueryableSpecification<Fish>, Fish>(specification);

            var expectedExecutorType = typeof(DefaultQueryableSpecificationExecutor<>).MakeGenericType(typeof(Fish));
            executor.GetType().ShouldBeEqualTo(expectedExecutorType);
        }
    }
}