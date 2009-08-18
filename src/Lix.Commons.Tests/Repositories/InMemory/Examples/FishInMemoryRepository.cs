using Lix.Commons.Repositories.InMemory;
using Lix.Commons.Tests.Examples;

namespace Lix.Commons.Tests.Repositories.InMemory.Examples
{
    public class FishInMemoryRepository : InMemoryRepositoryBase<Fish>
    {
        public FishInMemoryRepository(InMemoryUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}