using Lix.Commons.Repositories;

namespace Lix.Commands.Tests
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new InMemoryUnitOfWork(new InMemoryDataStore());
        }
    }
}