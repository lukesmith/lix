using System.Collections.Generic;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;

namespace Lix.Commons.Services
{
    public abstract class ServiceBase<TEntity, TRepository> : IService<TEntity>
        where TRepository : class, IRepository<TEntity>
    {
        protected ServiceBase(TRepository repository)
        {
            this.Repository = repository;
        }

        protected TRepository Repository
        {
            get;
            private set;
        }

        public virtual IQueryableSpecification<TEntity> GetListSpecification()
        {
            return Specification.Empty<TEntity>();
        }

        public IEnumerable<TEntity> List()
        {
            return this.Repository.List(this.GetListSpecification());
        }

        public PagedResult<TEntity> List(int startIndex, int pageSize)
        {
            return this.Repository.List(this.GetListSpecification(), startIndex, pageSize);
        }

        public virtual TEntity Save(TEntity entity)
        {
            return this.Repository.Save(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            this.Repository.Remove(entity);
        }
    }
}
