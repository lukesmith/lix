using System;
using NHibernate;

namespace Lix.Commons.Repositories.NHibernate
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        public NHibernateUnitOfWork(ISession session)
        {
            this.Session = session;
            this.Transaction = session.Transaction;
        }

        public static NHibernateUnitOfWork Begin(ISession session)
        {
            var unitOfWork = new NHibernateUnitOfWork(session);
            unitOfWork.Begin();

            return unitOfWork;
        }

        public bool IsActive
        {
            get
            {
                return this.Transaction.IsActive;
            }
        }

        public ITransaction Transaction
        {
            get;
            private set;
        }

        public ISession Session
        {
            get;
            private set;
        }

        public void Dispose()
        {
            if (!this.Transaction.WasCommitted && this.IsActive)
            {
                this.Transaction.Rollback();
            }

            this.Transaction.Dispose();
        }

        public void Begin()
        {
            if (this.Transaction.IsActive)
            {
                throw new InvalidOperationException("A unit of work has already begun for this session.");
            }

            this.Transaction.Begin();
        }

        public void Commit()
        {
            this.Commit(false);
        }

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