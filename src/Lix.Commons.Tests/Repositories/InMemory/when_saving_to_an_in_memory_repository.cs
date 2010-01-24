using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.InMemory
{
    [TestFixture]
    public class when_saving_to_an_in_memory_repository : in_memory_test_setups
    {
        [Test]
        public void should_add_the_entity_to_the_current_transaction_datastore()
        {
            this.UnitOfWork.Begin();

            var fishInMemoryRepository = new InMemoryRepository<Fish>(this.UnitOfWork, null);

            var fish = new Fish();
            fishInMemoryRepository.Save(fish);

            this.UnitOfWork.CurrentTransactionDataStore.Count().ShouldBeEqualTo(1);
        }
    }

    [TestFixture]
    public class when_removing_from_an_in_memory_repository : in_memory_test_setups
    {
        [Test]
        public void should_remove_the_entity_from_the_current_transactions_datastore()
        {
            this.UnitOfWork.Begin();

            var fishInMemoryRepository = new InMemoryRepository<Fish>(this.UnitOfWork, null);

            var fish = new Fish();
            var savedFish = fishInMemoryRepository.Save(fish);

            this.UnitOfWork.CurrentTransactionDataStore.Count().ShouldBeEqualTo(1);

            fishInMemoryRepository.Remove(savedFish);

            this.UnitOfWork.CurrentTransactionDataStore.List<Fish>().Count().ShouldBeEqualTo(0);
        }
    }
}
