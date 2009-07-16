using System.Collections.Generic;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;

namespace Lix.Commons.Services
{
    public abstract class ServiceBase<T, TRepository> : IService<T>
        where TRepository : class, IRepository<T>
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

        public virtual IQueryableSpecification<T> GetListSpecification()
        {
            return Specification.Empty<T>();
        }

        public IEnumerable<T> List()
        {
            return this.Repository.List(this.GetListSpecification());
        }

        public PagedResult<T> List(int startIndex, int pageSize)
        {
            return this.Repository.List(this.GetListSpecification(), startIndex, pageSize);
        }

        public virtual T Save(T entity)
        {
            return this.Repository.Save(entity);
        }

        public virtual void Delete(T entity)
        {
            this.Repository.Remove(entity);
        }
    }
}
