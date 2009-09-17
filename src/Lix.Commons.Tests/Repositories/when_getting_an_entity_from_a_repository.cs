using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories
{
    public abstract class when_getting_an_entity_from_a_repository<TUnitOfWork, TRepository> : repository_test_setups<TUnitOfWork, TRepository, Fish>
        where TUnitOfWork : class, IUnitOfWork
        where TRepository : IRepository<Fish>
    {
        public override void SetUp()
        {
            base.SetUp();

            this.UnitOfWork.Begin();
            this.UnitOfWork.Save(new Fish { Description = "Slippery Fish" });
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

            var fish = this.Repository.Get(new FindFishWithDescriptionSpecification("Slippery Fish"));

            fish.ShouldBeEqualTo(null);
        }

        [Test]
        public void should_intercept_the_specification_with_a_specification()
        {
            var interceptWith = new EmptyFishQueryableSpecification2();
            Specification.Intercept<FindFishWithDescriptionSpecification>().With(interceptWith);

            var fish = this.Repository.Get(new FindFishWithDescriptionSpecification("Not slippery fish"));

            fish.Description.ShouldBeEqualTo("Slippery Fish");
        }

        [Test]
        public void should_get_the_entity()
        {
            var fish = this.Repository.Get(new FindFishWithDescriptionSpecification("Slippery Fish"));

            fish.Description.ShouldBeEqualTo("Slippery Fish");
        }
    }
}