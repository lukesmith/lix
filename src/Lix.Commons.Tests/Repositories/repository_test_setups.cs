using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories
{
    public abstract class repository_test_setups<TUnitOfWork, TRepository, TEntity>
        where TUnitOfWork : class, IUnitOfWork
        where TRepository : IRepository<TEntity>
    {
        private TUnitOfWork unitOfWork;

        protected abstract TUnitOfWork CreateUnitOfWork();

        protected abstract TRepository CreateRepository();

        protected TUnitOfWork UnitOfWork
        {
            get
            {
                if (this.unitOfWork == null)
                {
                    this.unitOfWork = this.CreateUnitOfWork();
                }

                return this.unitOfWork;
            }
        }

        protected TRepository Repository
        {
            get; set;
        }

        [SetUp]
        public virtual void SetUp()
        {
            this.Repository = this.CreateRepository();
        }

        [TearDown]
        public virtual void TearDown()
        {
            this.unitOfWork = null;
        }
    }
}