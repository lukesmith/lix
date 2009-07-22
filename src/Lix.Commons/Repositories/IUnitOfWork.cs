using System;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents a unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether unit of work is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        bool IsActive { get; }

        /// <summary>
        /// Begins a new unit of work.
        /// </summary>
        void Begin();

        /// <summary>
        /// Commits the unit of work.
        /// </summary>
        void Commit();

        /// <summary>
        /// Commits the unit of work.
        /// </summary>
        void Commit(bool begin);

        /// <summary>
        /// Rollbacks the unit of work.
        /// </summary>
        void Rollback();
    }
}