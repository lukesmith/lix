using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.SpecificationExecutors.when_getting_an_executor
{
    [TestFixture]
    public class when_the_specification_works_against_an_interface_the_entity_implements
    {
        [Test]
        [Pending]
        public void should_get_the_executor()
        {
            var people = Enumerable.Repeat(new Person { Id = 4 }, 3).ToList().AsQueryable();

            var specificationExecutorFactory = new SpecificationExecutorFactory();
            specificationExecutorFactory.RegisterContext<IQueryable<Person>>(() => people);
            var executor = specificationExecutorFactory.GetExecutor<FindIdentifiable, Person>(new FindIdentifiable(4));

            executor.ShouldBeTheSameTypeAs(typeof(DefaultQueryableSpecificationExecutor<Person>));
        }
    }
}