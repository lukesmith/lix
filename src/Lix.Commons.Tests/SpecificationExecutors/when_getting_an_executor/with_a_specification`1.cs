using System;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.SpecificationExecutors.when_getting_an_executor
{
    public abstract class with_a_specification<TSpecification>
        where TSpecification : class, ISpecification
    {
        protected SpecificationExecutorFactory SpecificationExecutorFactory
        {
            get; private set;
        }

        protected abstract void RegisterContext();

        protected abstract TSpecification CreateSpecification();

        [SetUp]
        public virtual void SetUp()
        {
            this.SpecificationExecutorFactory = new SpecificationExecutorFactory();
        }

        [Test]
        public void should_set_the_specification_property_on_the_executor()
        {
            this.RegisterContext();

            var specification = this.CreateSpecification();
            var executor = this.SpecificationExecutorFactory.GetExecutor<TSpecification, Fish>(specification);

            executor.Specification.ShouldBeTheSameObjectAs(specification);
        }

        [Test]
        public void should_return_a_non_null_executor()
        {
            this.RegisterContext();

            var specification = this.CreateSpecification();
            var executor = this.SpecificationExecutorFactory.GetExecutor<TSpecification, Fish>(specification);

            executor.ShouldSatisfy(x => x != null);
        }

        [Test]
        public void should_throw_if_the_required_context_is_not_registered()
        {
            var specification = this.CreateSpecification();

            Exception thrownException = null;

            try
            {
                this.SpecificationExecutorFactory.GetExecutor<TSpecification, Fish>(specification);
            }
            catch (Exception ex)
            {
                thrownException = ex;
            }

            thrownException.Message.ShouldSatisfy(x => x.StartsWith("A context could not be found for the executor"));
        }
    }
}