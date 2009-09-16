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
    public abstract class when_listing_paged_entities_in_a_repository<TUnitOfWork, TRepository> : repository_test_setups<TUnitOfWork, TRepository, Fish>
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

            var result = this.Repository.List(new FindFishWithDescriptionSpecification("Slippery Fish"), 0, 10);

            result.Count().ShouldBeEqualTo(0);
        }

        [Test]
        public void should_intercept_the_specification_with_a_specification()
        {
            var interceptWith = new TestSpecification2();
            Specification.Intercept<FindFishWithDescriptionSpecification>().With(interceptWith);

            var result = this.Repository.List(new FindFishWithDescriptionSpecification("Not slippery fish"), 0, 2);

            result.Count().ShouldBeEqualTo(2);
        }

        [Test]
        public void should_list_all_the_entities()
        {
            var result = this.Repository.List(new TestSpecification2(), 0, 10);

            result.Count().ShouldBeEqualTo(4);
        }

        [Test]
        public void should_list_the_entities_starting_not_including_the_first()
        {
            var result = this.Repository.List(new TestSpecification2(), 1, 10);

            result.Any(f => f.Description == "Slippery Fish").ShouldBeEqualTo(false);
        }

        [Test]
        public void should_list_a_sub_selection_of_entities()
        {
            var result = this.Repository.List(new TestSpecification2(), 1, 2);

            result.Last().Description.ShouldBeEqualTo("A fish called wanda");
        }
    }
}