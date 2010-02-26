using Lix.Commons.Repositories;
using StructureMap;

namespace Lix.StructureMapAdapter
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IContainer container;

        public UnitOfWorkFactory(IContainer container)
        {
            this.container = container;
        }

        public IUnitOfWork Create()
        {
            return this.container.GetInstance<IUnitOfWork>();
        }
    }
}
