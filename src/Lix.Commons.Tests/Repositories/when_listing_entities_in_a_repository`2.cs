using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using Lix.Commons.Tests.HelperExtensions;
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
        public void should_intercept_the_specification()
        {
            RepositoryTestHelpers.TestRepositoryMethodInterceptsTheSpecification
                <TRepository, Fish, EmptyFishQueryableSpecification>(this.Repository, (x, y) => x.List(y));
        }

        [Test]
        public void should_list_the_entities()
        {
            var result = this.Repository.List(new FindFishWithDescriptionSpecification("Slippery Fish"));

            result.Count().ShouldBeEqualTo(1);
        }
    }
}