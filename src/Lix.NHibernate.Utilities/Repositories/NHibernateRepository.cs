using System.Data.SqlClient;
using System.Linq;
using Lix.Commons.Specifications;
using NHibernate;
using NHibernate.Exceptions;
using NHibernate.Linq;

namespace Lix.Commons.Repositories
{
    public class NHibernateRepository<TEntity> : RepositoryBase<TEntity, NHibernateUnitOfWork>, INHibernateRepository<TEntity>
        where TEntity : class
    {
        public NHibernateRepository(NHibernateUnitOfWork unitOfWork, ISpecificationExecutorFactory specificationExecutorFactory)
            : base(unitOfWork, specificationExecutorFactory)
        {
        }

        public new NHibernateUnitOfWork UnitOfWork
        {
            get
            {
                return base.UnitOfWork as NHibernateUnitOfWork;
            }
        }

        protected override IQueryable<TEntity> GetRepositoryQuery()
        {
            return this.UnitOfWork.Session.Linq<TEntity>();
        }

        public override TEntity Save(TEntity entity)
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

        public override void Remove(TEntity entity)
        {
            this.UnitOfWork.Session.Delete(entity);
        }

        ISession INHibernateRepository<TEntity>.CurrentSession
        {
            get { return this.UnitOfWork.Session; }
        }
    }
}