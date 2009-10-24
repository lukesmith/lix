using Lix.Commons.Repositories;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.InMemory
{
    public abstract class in_memory_test_setups
    {
        protected InMemoryUnitOfWork UnitOfWork
        {
            get;
            private set;
        }

        protected InMemoryDataStore MainDataStore
        {
            get;
            private set;
        }

        [SetUp(Order = 0)]
        public virtual void SetUp()
        {
            this.MainDataStore = new InMemoryDataStore();
            this.UnitOfWork = new InMemoryUnitOfWork(this.MainDataStore);
        }

        [TearDown(Order = 0)]
        public virtual void TearDown()
        {
        }
    }
}