﻿using Lix.Commons;
using Lix.Commons.Specifications;
using Lix.Commons.Tests;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;
using Moq;
using NHibernate;

namespace Lix.NHibernate.Utilities.Tests.SpecificationExecutors.when_getting_an_executor
{
    [TestFixture]
    public class of_type_default_nhibernate_criteria_specification_executor
    {
        [Test]
        public void should_have_the_default_nhibernate_hql_specification_executor_registered()
        {
            var executor = LixObjectFactory.Container.FindTypeFor(typeof(INHibernateCriteriaSpecification));
            
            executor.ShouldSatisfy(x => x != null);
        }

        [Test]
        public void should_find_the_correct_specification_executor()
        {
            var factory = new SpecificationExecutorFactory();
            factory.RegisterContext<ISession>(() => new Mock<ISession>().Object);
            var executor = factory.GetExecutor<DefaultNHibernateCriteriaSpecification<Fish>, Fish>(new Mock<DefaultNHibernateCriteriaSpecification<Fish>>().Object);

            executor.ShouldBeTheSameTypeAs(typeof(DefaultNHibernateCriteriaSpecificationExecutor<Fish>));
        }
    }
}