using System;
using System.Data;
using System.Data.Common;
using System.Data.Linq;

namespace Lix.Commons.Repositories.Linq2Sql
{
    public class Linq2SqlUnitOfWork : IUnitOfWork
    {
        private bool wasTransactionCommitted;
        private bool wasTransactionRolledback;

        public Linq2SqlUnitOfWork(DataContext dataContext)
        {
            this.DataContext = dataContext;
        }

        /// <summary>
        /// Gets the transaction associated with this instance.
        /// </summary>
        /// <value>The transaction.</value>
        public DbTransaction Transaction
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the session associated with this instance.
        /// </summary>
        /// <value>The session.</value>
        public DataContext DataContext
        {
            get;
            private set;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (!wasTransactionCommitted && !this.wasTransactionRolledback && this.IsActive)
            {
                this.DataContext.Transaction.Rollback();
                this.wasTransactionRolledback = true;
            }

            if (this.DataContext.Transaction != null)
            {
                this.DataContext.Transaction.Dispose();
            }
        }

        /// <summary>
        /// Gets a value indicating whether unit of work is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive
        {
            get { return this.Transaction != null; }
        }

        /// <summary>
        /// Begins a new unit of work.
        /// </summary>
        public void Begin()
        {
            if (this.IsActive || this.DataContext.Transaction != null)
            {
                throw new InvalidOperationException("A unit of work has already begun for this session.");
            }

            this.Transaction = this.DataContext.Connection.BeginTransaction();
            this.DataContext.Transaction = this.Transaction;
            this.wasTransactionCommitted = false;
            this.wasTransactionRolledback = false;
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

            this.DataContext.SubmitChanges();
            this.Transaction.Commit();
            this.Transaction.Dispose();
            this.Transaction = null;
            this.DataContext.Transaction = null;

            this.wasTransactionCommitted = true;
            this.wasTransactionRolledback = false;

            if (begin)
            {
                // begin a new transaction as part of the current unit of work
                this.Transaction = this.DataContext.Connection.BeginTransaction();
                this.DataContext.Transaction = this.Transaction;
                this.wasTransactionCommitted = false;
                this.wasTransactionRolledback = false;
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

            this.Transaction.Rollback();
            this.wasTransactionRolledback = true;
        }
    }
}
