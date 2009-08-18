﻿using Lix.Commons.Repositories.InMemory;
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

        [SetUp(Order = 0)]
        public virtual void SetUp()
        {
            this.UnitOfWork = new InMemoryUnitOfWork(new InMemoryDataStore());
        }

        [TearDown(Order = 0)]
        public void TearDown()
        {
        }
    }
}