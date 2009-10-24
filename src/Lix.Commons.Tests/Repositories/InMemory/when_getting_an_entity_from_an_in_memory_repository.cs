using Lix.Commons.Repositories;
using Lix.Commons.Tests.Repositories.InMemory.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.InMemory
{
    [TestFixture]
    public class when_getting_an_entity_from_an_in_memory_repository : when_getting_an_entity_from_a_repository<InMemoryUnitOfWork, FishInMemoryRepository>
    {
        protected override FishInMemoryRepository CreateRepository()
        {
            return new FishInMemoryRepository(this.UnitOfWork);
        }

        protected override InMemoryUnitOfWork CreateUnitOfWork()
        {
            var dataStore = new InMemoryDataStore();
            return new InMemoryUnitOfWork(dataStore);
        }
    }
}
