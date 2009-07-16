using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Specifications.NHibernate;
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
        }

        protected override IQueryable<T> RepositoryQuery
        {
            get
            {
                return this.UnitOfWork.Session.Linq<T>();
            }
        }

        public override T Get(ISpecification specification)
        {
            if (specification is INHibernateCriteriaSpecification)
            {
                return this.Get(specification as INHibernateCriteriaSpecification);
            }

            return base.Get(specification);
        }

        public override IEnumerable<T> List(ISpecification specification)
        {
            if (specification is INHibernateCriteriaSpecification)
            {
                return this.List(specification as INHibernateCriteriaSpecification);
            }

            return base.List(specification);
        }

        public override PagedResult<T> List(ISpecification specification, int startIndex, int pageSize)
        {
            if (specification is INHibernateCriteriaSpecification)
            {
                return this.List(specification as INHibernateCriteriaSpecification, startIndex, pageSize);
            }
            
            return base.List(specification, startIndex, pageSize);
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

        protected override IQueryable<T> Query(IQueryableSpecification<T> specification)
        {
            IQueryable<T> result = null;

            this.Execute(s =>
            {
                result = specification.Build(s.Linq<T>())
                    .ToList().AsQueryable();
            });

            return result;
        }

        protected override PagedResult<T> List(IQueryableSpecification<T> specification, int startIndex, int pageSize)
        {
            PagedResult<T> result = null;

            this.Execute(s =>
            {
                result = s.Linq<T>().PagedList(specification, startIndex, pageSize);
            });

            return result;
        }

        protected T Get(INHibernateCriteriaSpecification specification)
        {
            T result = null;

            this.Execute(s =>
            {
                if (specification != null)
                {
                    ICriteria criteria = specification.Build(s);
                    result = criteria.UniqueResult<T>();
                }
            });

            return result;
        }

        protected IEnumerable<T> List(INHibernateCriteriaSpecification specification)
        {
            IList<T> result = null;

            this.Execute(s =>
            {
                if (specification != null)
                {
                    ICriteria criteria = specification.Build(s);
                    result = criteria.List<T>();
                }
            });

            return result;
        }

        protected PagedResult<T> List(INHibernateCriteriaSpecification specification, int startIndex, int pageSize)
        {
            PagedResult<T> result = null;

            this.Execute(s =>
            {
                if (specification != null)
                {
                    ICriteria criteria = specification.Build(s);
                    result = criteria.PagedList<T>(s, startIndex, pageSize);
                }
            });

            return result;
        }

        protected void Execute(Action<ISession> action)
        {
            this.Execute(this.UnitOfWork.Session, action);
        }

        private void Execute(ISession session, Action<ISession> action)
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