using System;

namespace Lix.Commons.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
        bool IsActive { get; }
    }
}