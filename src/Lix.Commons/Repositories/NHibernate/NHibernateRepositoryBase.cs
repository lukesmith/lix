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
            this.SpecificationExecutorFactory.RegisterContext<ISession>(() => unitOfWork.Session);
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
            try
            {
                this.UnitOfWork.Session.SaveOrUpdate(entity);
                return entity;
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
        }

        public override void Remove(T entity)
        {
            this.UnitOfWork.Session.Delete(entity);
        }
    }
}