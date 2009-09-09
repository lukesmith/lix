using System;
using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Repositories.InMemory
{
    /// <summary>
    /// Represents an in memory unit of work.
    /// </summary>
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public InMemoryUnitOfWork(InMemoryDataStore dataStore)
        {
            this.DataStore = dataStore;
            this.CurrentTransactionDataStore = new InMemoryDataStore();
        }

        public InMemoryDataStore DataStore
        {
            get;
            private set;
        }

        public InMemoryDataStore CurrentTransactionDataStore
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether unit of work is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive
        {
            get;
            private set;
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
            // TODO: Check whether a unit of work already exists for this DataStore
            //if (this.IsActive)
            //{
            //    throw new InvalidOperationException("A unit of work has already begun for this session.");
            //}

            this.CurrentTransactionDataStore = new InMemoryDataStore();
            this.CurrentTransactionDataStore.Merge(this.DataStore);
            this.IsActive = true;
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

            // Commit transactionaldatastore to InMemoryDataStore
            this.DataStore.Merge(this.CurrentTransactionDataStore);

            this.IsActive = false;

            if (begin)
            {
                this.Begin();
            }
            else
            {
                this.CurrentTransactionDataStore.Clear();
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
                this.CurrentTransactionDataStore.Clear();
                this.IsActive = false;
            }
        }
    }
}