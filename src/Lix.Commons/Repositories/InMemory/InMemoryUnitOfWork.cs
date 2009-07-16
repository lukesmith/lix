using System;

namespace Lix.Commons.Repositories.InMemory
{
    /// <summary>
    /// Represents an in memory unit of work.
    /// </summary>
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets a value indicating whether unit of work is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Begins a new unit of work.
        /// </summary>
        public void Begin()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Commits the unit of work.
        /// </summary>
        public void Commit()
        {
        }

        /// <summary>
        /// Rollbacks the unit of work.
        /// </summary>
        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}