using System;
using NHibernate;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents an NHibernate unit of work.
    /// </summary>
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateUnitOfWork"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public NHibernateUnitOfWork(ISession session)
        {
            this.Session = session;
            this.Transaction = session.Transaction;
        }

        /// <summary>
        /// Gets a value indicating whether unit of work is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive
        {
            get
            {
                return this.Transaction.IsActive;
            }
        }

        /// <summary>
        /// Gets the transaction associated with this instance.
        /// </summary>
        /// <value>The transaction.</value>
        public ITransaction Transaction
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the session associated with this instance.
        /// </summary>
        /// <value>The session.</value>
        public ISession Session
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and begins a new instance of an <see cref="NHibernateUnitOfWork"/> for the the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>
        /// A new <see cref="NHibernateUnitOfWork"/>.
        /// </returns>
        public static NHibernateUnitOfWork Begin(ISession session)
        {
            var unitOfWork = new NHibernateUnitOfWork(session);
            unitOfWork.Begin();

            return unitOfWork;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!this.Transaction.WasCommitted && this.IsActive)
            {
                this.Transaction.Rollback();
            }

            this.Transaction.Dispose();
        }

        /// <summary>
        /// Begins a new unit of work.
        /// </summary>
        public void Begin()
        {
            if (this.IsActive)
            {
                throw new InvalidOperationException("A unit of work has already begun for this session.");
            }

            if (this.Transaction.WasCommitted || this.Transaction.WasRolledBack)
            {
                this.Transaction = this.Session.BeginTransaction();
            }

            this.Transaction.Begin();
        }

        /// <summary>
        /// Commits the unit of work.
        /// </summary>
        public void Commit()
        {
            this.Commit(false);
        }

        /// <summary>
        /// Commits the specified begin.
        /// </summary>
        /// <param name="begin">if set to <c>true</c> a new transaction is started.</param>
        public void Commit(bool begin)
        {
            if (!this.IsActive)
            {
                throw new InvalidOperationException("Unable to commit when not active.");
            }

            this.Transaction.Commit();

            if (begin)
            {
                // begin a new transaction as part of the current unit of work
                this.Transaction = this.Session.BeginTransaction();
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
        }
    }
}