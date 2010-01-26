using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using Lix.Commons.Tests.HelperExtensions;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories
{
    public abstract class when_checking_an_entity_exists_in_a_repository<TUnitOfWork, TRepository> : repository_test_setups<TUnitOfWork, TRepository, Fish>
        where TUnitOfWork : class, IUnitOfWork
        where TRepository : IQueryRepository<Fish>
    {
        public override void SetUp()
        {
            base.SetUp();

            this.UnitOfWork.Begin();
            this.SaveToUnitOfWork(this.UnitOfWork, new Fish { Id = 2 });
            this.SaveToUnitOfWork(this.UnitOfWork, new Fish { Id = 5 });
            this.SaveToUnitOfWork(this.UnitOfWork, new Fish { Id = 9 });
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
                <TRepository, Fish, FindAll<Fish>>(this.Repository, (x, y) => x.Exists(y));
        }
    }
}