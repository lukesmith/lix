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
    public abstract class when_checking_an_entity_exists_in_a_repository<TUnitOfWork, TRepository> : repository_test_setups<TUnitOfWork, TRepository, Fish>
        where TUnitOfWork : class, IUnitOfWork
        where TRepository : IRepository<Fish>
    {
        public override void SetUp()
        {
            base.SetUp();

            this.UnitOfWork.Begin();
            this.Repository.Save(new Fish { Id = 2 });
            this.Repository.Save(new Fish { Id = 5 });
            this.Repository.Save(new Fish { Id = 9 });
            this.UnitOfWork.Commit(true);
        }

        public override void TearDown()
        {
            Specification.Interceptors.Clear();

            base.TearDown();
        }

        [Test]
        public void should_intercept_the_specification_with_a_lambda_and_not_find_the_entity()
        {
            var interceptWith = new Func<IQueryable<Fish>>(() => new List<Fish>().AsQueryable());
            Specification.Intercept<FindFishWithIdSpecification>().With(interceptWith);

            var result = this.Repository.Exists(new FindFishWithIdSpecification(2));

            result.ShouldBeEqualTo(false);
        }

        [Test]
        public void should_intercept_the_specification_with_a_lambda_and_find_the_entity()
        {
            var interceptWith = new Func<IQueryable<Fish>>(() => new List<Fish> { new Fish { Id = 2 } }.AsQueryable());
            Specification.Intercept<FindFishWithIdSpecification>().With(interceptWith);

            var result = this.Repository.Exists(new FindFishWithIdSpecification(2));

            result.ShouldBeEqualTo(true);
        }

        [Test]
        public void should_intercept_the_specification_with_another_specification_and_find_the_entity()
        {
            var interceptWith = new FindFishWithIdSpecification(5);
            Specification.Intercept<FindFishWithIdSpecification>().With(interceptWith);

            var result = this.Repository.Exists(new FindFishWithIdSpecification(3));

            result.ShouldBeEqualTo(true);
        }

        [Test]
        public void should_intercept_the_specification_with_another_specification_and_not_find_the_entity()
        {
            var interceptWith = new FindFishWithIdSpecification(3);
            Specification.Intercept<FindFishWithIdSpecification>().With(interceptWith);

            var result = this.Repository.Exists(new FindFishWithIdSpecification(5));

            result.ShouldBeEqualTo(false);
        }

        [Test]
        public void should_find_the_entity()
        {
            var result = this.Repository.Exists(new FindFishWithIdSpecification(2));

            result.ShouldBeEqualTo(true);
        }
    }
}