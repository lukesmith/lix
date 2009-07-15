using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Commons.Repositories
{
    public abstract class RepositoryBase<T, TUnitOfWork> : IRepository<T>
        where T : class
        where TUnitOfWork : IUnitOfWork
    {
        protected RepositoryBase(TUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public TUnitOfWork UnitOfWork
        {
            get;
            private set;
        }

        protected abstract IQueryable<T> RepositoryQuery
        {
            get;
        }

        public abstract T Save(T entity);

        public abstract void Remove(T entity);

        protected virtual IQueryable<T> Query(IQueryableSpecification<T> specification)
        {
            return specification.Build(this.RepositoryQuery);
        }

        protected virtual T Get(IQueryableSpecification<T> specification)
        {
            return this.Query(specification).SingleOrDefault();
        }

        protected virtual IEnumerable<T> List(IQueryableSpecification<T> specification)
        {
            return this.Query(specification).ToList();
        }

        protected virtual PagedList<T> List(IQueryableSpecification<T> specification, int startIndex, int pageSize)
        {
            var specificationQuery = this.Query(specification);

            return specificationQuery.PagedList(startIndex, pageSize);
        }

        public bool Exists(IQueryableSpecification<T> specification)
        {
            return specification.Build(this.RepositoryQuery).FirstOrDefault() != null;
        }

        public virtual T Get(ISpecification specification)
        {
            if (specification is IQueryableSpecification<T>)
            {
                return this.Get(specification as IQueryableSpecification<T>);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public virtual IEnumerable<T> List(ISpecification specification)
        {
            if (specification is IQueryableSpecification<T>)
            {
                return this.List(specification as IQueryableSpecification<T>);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public virtual PagedList<T> List(ISpecification specification, int startIndex, int pageSize)
        {
            if (specification is IQueryableSpecification<T>)
            {
                return this.List(specification as IQueryableSpecification<T>, startIndex, pageSize);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
