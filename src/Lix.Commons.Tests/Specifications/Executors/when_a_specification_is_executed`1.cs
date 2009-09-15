using System;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Specifications.Executors
{
    public class when_a_specification_is_executed
    {
        [Test]
        [Pending]
        [ExpectedException(typeof(InvalidOperationException), "A constructor argument named 'context' could not be found.")]
        public void should_throw_if_the_executor_does_not_have_a_constructor_argument_named_context()
        {
            
        }
    }

    public abstract class when_a_specification_is_executed<TSpecification>
        where TSpecification : class, ISpecification
    {
        protected SpecificationExecutionEngine SpecificationExecutionEngine
        {
            get; private set;
        }

        protected abstract void RegisterContext();

        protected abstract TSpecification CreateSpecification();

        [SetUp]
        public virtual void SetUp()
        {
            this.SpecificationExecutionEngine = new SpecificationExecutionEngine();
        }

        [Test]
        public void should_set_the_specification_property_on_the_executor()
        {
            this.RegisterContext();

            var specification = this.CreateSpecification();
            var executor = this.SpecificationExecutionEngine.GetExecutor<TSpecification, Fish>(specification);

            executor.Specification.ShouldBeTheSameObjectAs(specification);
        }

        [Test]
        public void should_return_a_non_null_executor()
        {
            this.RegisterContext();

            var specification = this.CreateSpecification();
            var executor = this.SpecificationExecutionEngine.GetExecutor<TSpecification, Fish>(specification);

            executor.ShouldSatisfy(x => x != null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), Message = "A context could not be found for the executor Lix.Commons.Specifications.QueryableSpecificationExecutor`1[TEntity]")]
        public void should_throw_if_the_required_context_is_not_registered()
        {
            var specification = this.CreateSpecification();
            this.SpecificationExecutionEngine.GetExecutor<TSpecification, Fish>(specification);
        }
    }
}