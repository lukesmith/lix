using Lix.Commons.Repositories;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.InMemory
{
    [TestFixture]
    public class when_listing_entities_in_an_in_memory_repository : when_listing_entities_in_a_repository<InMemoryUnitOfWork, InMemoryRepository<Fish>>
    {
        protected override void SaveToUnitOfWork(InMemoryUnitOfWork unitOfWork, Fish entity)
        {
            unitOfWork.CurrentTransactionDataStore.Save(entity);
        }

        protected override InMemoryRepository<Fish> CreateRepository()
        {
            return new InMemoryRepository<Fish>(this.UnitOfWork, null);
        }

        protected override InMemoryUnitOfWork CreateUnitOfWork()
        {
            var dataStore = new InMemoryDataStore();
            return new InMemoryUnitOfWork(dataStore);
        }
    }
}
