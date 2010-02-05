using System;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents an in memory unit of work.
    /// </summary>
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public InMemoryUnitOfWork(InMemoryDataStore dataStore)
        {
            this.DataStore = dataStore;
        }

        public InMemoryDataStore CurrentTransactionDataStore
        {
            get
            {
                if (this.IsActive)
                {
                    return this.DataStore.Transaction.CurrentTransactionDataStore;
                }
                
                throw new InvalidOperationException("No transaction has begun.");
            }
        }

        /// <summary>
        /// Gets a value indicating whether unit of work is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive
        {
            get
            {
                return this.DataStore.Transaction != null;
            }
        }

        private InMemoryDataStore DataStore
        {
            get;
            set;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.IsActive)
            {
                this.DataStore.Transaction.Rollback();
                this.DataStore.Transaction = null;
            }
        }

        /// <summary>
        /// Begins a new unit of work.
        /// </summary>
        public void Begin()
        {
            // TODO: Check whether a unit of work already exists for this DataStore
            if (this.IsActive)
            {
                throw new InvalidOperationException("A unit of work has already begun for this session.");
            }

            this.DataStore.BeginTransaction();
        }

        /// <summary>
        /// Commits the unit of work.
        /// </summary>
        public void Commit()
        {
            this.Commit(false);
        }

        /// <summary>
        /// Commits the unit of work.
        /// </summary>
        public void Commit(bool begin)
        {
            if (!this.IsActive)
            {
                throw new InvalidOperationException("Unable to commit when not active.");
            }

            this.DataStore.Transaction.Commit();
            this.DataStore.Transaction = null;

            if (begin)
            {
                this.Begin();
            }
        }

        /// <summary>
        /// Rollbacks the unit of work.
        /// </summary>
        public void Rollback()
        {
            if (!this.IsActive)
            {
                throw new InvalidOperationException("Unable to rollback when not active.");
            }
            else
            {
                this.DataStore.Transaction.Rollback();
            }
        }
    }
}