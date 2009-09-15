using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Repositories.InMemory.Examples;
using Lix.Commons.Tests.Specifications.Examples;
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
            this.Repository.Save(new Fish { Id = 2 });
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
            Specification.Intercept<FindFishWithIdSpecification>().With(interceptWith);

            var fish = this.Repository.Get(new FindFishWithIdSpecification(2));

            fish.ShouldBeEqualTo(null);
        }

        [Test]
        public void should_intercept_the_specification_with_a_specification()
        {
            var interceptWith = new TestSpecification2();
            Specification.Intercept<FindFishWithIdSpecification>().With(interceptWith);

            var fish = this.Repository.Get(new FindFishWithIdSpecification(3));

            fish.Id.ShouldBeEqualTo(2);
        }

        [Test]
        public void should_get_the_entity()
        {
            var fish = this.Repository.Get(new FindFishWithIdSpecification(2));

            fish.Id.ShouldBeEqualTo(2);
        }
    }
}