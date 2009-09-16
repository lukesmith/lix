using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using Lix.Commons.Tests.Specifications.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories
{
    public abstract class when_listing_entities_in_a_repository<TUnitOfWork, TRepository> : repository_test_setups<TUnitOfWork, TRepository, Fish>
        where TUnitOfWork : class, IUnitOfWork
        where TRepository : IRepository<Fish>
    {
        public override void SetUp()
        {
            base.SetUp();

            this.UnitOfWork.Begin();
            this.UnitOfWork.Save(new Fish { Description = "Slippery Fish" });
            this.UnitOfWork.Save(new Fish { Description = "Wet Fish" });
            this.UnitOfWork.Save(new Fish { Description = "A fish called wanda" });
            this.UnitOfWork.Save(new Fish { Description = "A smelly fish" });
            this.UnitOfWork.Commit(true);
        }

        public override void TearDown()
        {
            Specification.Interceptors.Clear();

            base.TearDown();
        }

        [Test]
        public void should_intercept_the_specification_with_a_lambda()
        {
            var interceptWith = new Func<IQueryable<Fish>>(() => new List<Fish>().AsQueryable());
            Specification.Intercept<FindFishWithDescriptionSpecification>().With(interceptWith);

            var result = this.Repository.List(new FindFishWithDescriptionSpecification("Slippery Fish"));

            result.Count().ShouldBeEqualTo(0);
        }

        [Test]
        public void should_intercept_the_specification_with_a_specification()
        {
            var interceptWith = new TestSpecification2();
            Specification.Intercept<FindFishWithDescriptionSpecification>().With(interceptWith);

            var result = this.Repository.List(new FindFishWithDescriptionSpecification("Not slippery fish"));

            result.Count().ShouldBeEqualTo(4);
        }

        [Test]
        public void should_get_the_entity()
        {
            var result = this.Repository.List(new FindFishWithDescriptionSpecification("Slippery Fish"));

            result.Count().ShouldBeEqualTo(1);
        }
    }
}