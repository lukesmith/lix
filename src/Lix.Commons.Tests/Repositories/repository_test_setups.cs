using Lix.Commons.Repositories;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories
{
    public abstract class repository_test_setups<TUnitOfWork, TRepository, TEntity>
        where TUnitOfWork : class, IUnitOfWork
        where TRepository : IQueryRepository<TEntity>
        where TEntity : class
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

        protected abstract void SaveToUnitOfWork(TUnitOfWork unitOfWork, TEntity entity);

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