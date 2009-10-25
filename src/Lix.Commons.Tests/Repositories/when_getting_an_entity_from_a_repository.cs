using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using Lix.Commons.Tests.HelperExtensions;
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
            this.SaveToUnitOfWork(this.UnitOfWork, new Fish { Description = "Slippery Fish" });
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
                <TRepository, Fish, EmptyFishQueryableSpecification>(this.Repository, (x, y) => x.Get(y));
        }

        [Test]
        public void should_get_the_entity()
        {
            var fish = this.Repository.Get(new FindFishWithDescriptionSpecification("Slippery Fish"));

            fish.Description.ShouldBeEqualTo("Slippery Fish");
        }
    }
}