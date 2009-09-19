﻿using System;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_getting_an_executor
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
            SpecificationExecutorFactory.Initialize();
            this.SpecificationExecutorFactory = new SpecificationExecutorFactory();
        }

        [TearDown]
        public virtual void TearDown()
        {
            SpecificationExecutorFactory.ClearExecutors();
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
        [ExpectedException(typeof(InvalidOperationException), Message = "A context could not be found for the executor Lix.Commons.Specifications.QueryableSpecificationExecutor`1[TEntity]")]
        public void should_throw_if_the_required_context_is_not_registered()
        {
            var specification = this.CreateSpecification();
            this.SpecificationExecutorFactory.GetExecutor<TSpecification, Fish>(specification);
        }
    }
}