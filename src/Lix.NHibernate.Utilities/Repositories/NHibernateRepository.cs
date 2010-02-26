using System.Linq;
using Lix.Commons.Specifications.Executors;
using NHibernate;
using NHibernate.Linq;

namespace Lix.Commons.Repositories
{
    public class NHibernateRepository<TEntity> : LinqRepositoryBase<TEntity>, INHibernateRepository<TEntity>
        where TEntity : class
    {
        public NHibernateRepository(NHibernateUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        protected NHibernateUnitOfWork UnitOfWork
        {
            get; private set;
        }

        protected override IQueryable<TEntity> GetRepositoryQuery()
        {
            return this.UnitOfWork.Session.Linq<TEntity>();
        }

        public override TEntity Add(TEntity entity)
        {
            this.UnitOfWork.Session.SaveOrUpdate(entity);
            return entity;
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