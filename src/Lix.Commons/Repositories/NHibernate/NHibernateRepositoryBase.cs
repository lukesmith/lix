using System;
using System.Data.SqlClient;
using System.Linq;
using NHibernate;
using NHibernate.Exceptions;
using NHibernate.Linq;

namespace Lix.Commons.Repositories.NHibernate
{
    public abstract class NHibernateRepositoryBase<T> : RepositoryBase<T, NHibernateUnitOfWork>
        where T : class
    {
        protected NHibernateRepositoryBase(NHibernateUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.SpecificationExecutionEngine.RegisterContext<ISession>(() => unitOfWork.Session);
        }

        public new NHibernateUnitOfWork UnitOfWork
        {
            get
            {
                return base.UnitOfWork as NHibernateUnitOfWork;
            }
        }

        protected override IQueryable<T> RepositoryQuery
        {
            get
            {
                return this.UnitOfWork.Session.Linq<T>();
            }
        }

        public override T Save(T entity)
        {
            T result = default(T);

            try
            {
                this.Execute(s =>
                                 {
                                     s.SaveOrUpdate(entity);
                                     result = entity;
                                 });
            }
            catch (GenericADOException ex)
            {
                var sqlEx = ex.InnerException as SqlException;

                if (sqlEx != null && sqlEx.Number == 2627)
                {
                    throw new GenericADOException("A value must be unique.", ex);
                }
                
                throw;
            }

            return result;
        }

        public override void Remove(T entity)
        {
            this.Execute(s => s.Delete(entity));
        }

        protected void Execute(Action<ISession> action)
        {
            Execute(this.UnitOfWork.Session, action);
        }

        private static void Execute(ISession session, Action<ISession> action)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            action.Invoke(session);
        }
    }
}