using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.HelperExtensions;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_using_an_in_memory_datastore
{
    [TestFixture]
    public class and_beginning_a_transaction
    {
        private InMemoryDataStore datastore;

        [SetUp]
        public void SetUp()
        {
            this.datastore = new InMemoryDataStore();
            this.datastore.SaveEntities<Fish>(5);
            this.datastore.SaveEntities<Person>(2);
        }

        [Test]
        public void should_populate_the_transaction_data_store()
        {
            var transaction = this.datastore.BeginTransaction();

            transaction.CurrentTransactionDataStore.Count().ShouldBeEqualTo(2);
        }
    }
}