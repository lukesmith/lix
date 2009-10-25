using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.SpecificationExecutors.when_executing_a_specification
{
    [TestFixture]
    public class using_the_default_queryable_specification_executor : using_a_specification_executor<DefaultQueryableSpecificationExecutor<Fish>, DefaultQueryableSpecification<Fish>>
    {
        private IQueryable<Fish> context;

        public override void PerformSetUp()
        {
            this.context = this.CreateDefaults().AsQueryable();
        }

        protected override DefaultQueryableSpecificationExecutor<Fish> GetExecutor(DefaultQueryableSpecification<Fish> specification)
        {
            return new DefaultQueryableSpecificationExecutor<Fish>(specification, this.context);
        }

        protected override DefaultQueryableSpecification<Fish> GetSpecificationForMultipleUniqueResult()
        {
            return new EmptyFishQueryableSpecification();
        }

        protected override DefaultQueryableSpecification<Fish> GetSpecificationForUniqueResult(string description)
        {
            return new FindFishWithDescriptionSpecification(description);
        }
    }
}