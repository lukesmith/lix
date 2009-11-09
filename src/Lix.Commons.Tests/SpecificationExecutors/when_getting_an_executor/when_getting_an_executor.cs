using System;
using System.Linq;
using System.Linq.Expressions;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.SpecificationExecutors.when_getting_an_executor
{
    [TestFixture]
    public class when_getting_an_executor
    {
        [Test]
        [Pending]
        [ExpectedException(typeof(InvalidOperationException), "A constructor argument named 'context' could not be found.")]
        public void should_throw_if_the_executor_does_not_have_a_constructor_argument_named_context()
        {
        }

        [Test]
        public void should_throw_as_specification_cannot_use_the_context()
        {
            var people = Enumerable.Repeat(new Person { Id = 4 }, 3).ToList().AsQueryable();

            var specificationExecutorFactory = new SpecificationExecutorFactory();
            specificationExecutorFactory.RegisterContext<IQueryable<Person>>(() => people);

            Exception exception = null;

            try
            {
                specificationExecutorFactory.GetExecutor<FindWithName, Person>(new FindWithName(string.Empty));
            }
            catch (InvalidOperationException ex)
            {
                exception = ex;
            }

            exception.Message.ShouldBeEqualTo("The specification cannot be used against the context.");
        }

        private class FindWithName : DefaultQueryableSpecification<INameable>
        {
            private readonly string name;

            public FindWithName(string name)
            {
                this.name = name;
            }

            protected override Expression<Func<INameable, bool>> Predicate
            {
                get
                {
                    return x => x.Name.CompareTo(this.name) == 0;
                }
            }
        }

        private interface INameable
        {
            string Name { get; set; }
        }
    }
}