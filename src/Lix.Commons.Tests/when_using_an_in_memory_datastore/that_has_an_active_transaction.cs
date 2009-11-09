using System;
using Lix.Commons.Repositories;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;
using System.Linq;

namespace Lix.Commons.Tests.when_using_an_in_memory_datastore
{
    [TestFixture]
    public class that_has_an_active_transaction
    {
        private InMemoryDataStore dataStore;
        private InMemoryTransaction transaction;
        private IDisposable useCurrentTransactionDataStoreDisposable;

        [SetUp]
        public void SetUp()
        {
            this.dataStore = new InMemoryDataStore();
            this.transaction = this.dataStore.BeginTransaction();
            this.useCurrentTransactionDataStoreDisposable = this.dataStore.CurrentTransactionDataStore();
        }
        
        [TearDown]
        public void TearDown()
        {
            this.transaction.Rollback();
            this.useCurrentTransactionDataStoreDisposable.Dispose();
            this.useCurrentTransactionDataStoreDisposable = null;
            this.transaction = null;
            this.dataStore = null;
        }

        [Test]
        public void should_save_to_the_transaction_data_store()
        {
            var entity = new Fish {Description = "slippery fish"};

            this.dataStore.Save(entity);
            this.transaction.CurrentTransactionDataStore.Contains(entity).ShouldBeEqualTo(true);
        }

        [Test]
        public void should_remove_from_the_transaction_data_store()
        {
            var entity = new Fish { Description = "slippery fish" };

            this.dataStore.Save(entity);
            this.dataStore.Remove(entity);

            this.transaction.CurrentTransactionDataStore.Contains(entity).ShouldBeEqualTo(false);
        }

        [Test]
        public void should_use_the_transaction_data_store_when_looking_whether_an_entity_is_contained()
        {
            var entity = new Fish { Description = "slippery fish" };

            this.dataStore.Save(entity);

            this.dataStore.Contains(entity).ShouldBeEqualTo(true);
        }

        [Test]
        public void should_clear_the_transaction_data_store()
        {
            var entity = new Fish { Description = "slippery fish" };

            this.dataStore.Save(entity);
            this.dataStore.Clear();
            
            this.dataStore.Transaction.CurrentTransactionDataStore.Count().ShouldBeEqualTo(0);
        }

        [Test]
        public void should_list_the_transaction_data_store_entities()
        {
            var entity = new Fish { Description = "slippery fish" };

            this.dataStore.Save(entity);

            this.dataStore.List<Fish>().Any().ShouldBeEqualTo(true);
        }

        [Test]
        public void should_enumerate_the_transaction_data_store()
        {
            var entity = new Fish { Description = "slippery fish" };

            this.dataStore.Save(entity);

            this.dataStore.GetEnumerator().ShouldBeTheSameObjectAs(
                this.dataStore.Transaction.CurrentTransactionDataStore.GetEnumerator());
        }
    }
}