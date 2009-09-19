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
        private SpecificationExecutorFactory executionFactory;
        private IQueryable<Fish> context;

        [SetUp]
        public void Setup()
        {
            this.context = new List<Fish>().AsQueryable();

            SpecificationExecutorFactory.Initialize().WithDefaultNHibernateExecutors();

            this.executionFactory = new SpecificationExecutorFactory();
            this.executionFactory.RegisterContext<IQueryable<Fish>>(() => this.context);
        }

        [TearDown]
        public void TearDown()
        {
            SpecificationExecutorFactory.ClearExecutors();
        }

        [Test]
        public void should_find_the_queryable_specification_executor()
        {
            var specification = new EmptyFishQueryableSpecification();
            var executor = this.executionFactory.GetExecutor<IQueryableSpecification<Fish>, Fish>(specification);

            var expectedExecutorType = typeof(QueryableSpecificationExecutor<>).MakeGenericType(typeof(Fish));
            executor.GetType().ShouldBeEqualTo(expectedExecutorType);
        }
    }
}