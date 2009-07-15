using System;

namespace Lix.Commons.Repositories.InMemory
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
        }

        public void Begin()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public bool IsActive
        {
            get { throw new NotImplementedException(); }
        }
    }
}