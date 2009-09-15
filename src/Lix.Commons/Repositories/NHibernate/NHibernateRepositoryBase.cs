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

        protected override IEnumerable<T> PerformList(ISpecification specification)
        {
            if (specification is INHibernateCriteriaSpecification)
            {
                return this.List(specification as INHibernateCriteriaSpecification);
            }

            return base.PerformList(specification);
        }

        protected override PagedResult<T> PerformList(ISpecification specification, int startIndex, int pageSize)
        {
            if (specification is INHibernateCriteriaSpecification)
            {
                return this.List(specification as INHibernateCriteriaSpecification, startIndex, pageSize);
            }

            return base.PerformList(specification, startIndex, pageSize);
        }

        protected override long PerformCount(ISpecification specification)
        {
            if (specification is INHibernateCriteriaSpecification)
            {
                return this.Count(specification as INHibernateCriteriaSpecification);
            }

            return base.PerformCount(specification);
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
                result = specification.Build(s.Linq<T>()).ToList().AsQueryable();
            });

            return result;
        }

        protected override PagedResult<T> List(IQueryableSpecification<T> specification, int startIndex, int pageSize)
        {
            PagedResult<T> result = null;

            this.Execute(s =>
            {
                result = s.PagedList(specification, startIndex, pageSize);
            });

            return result;
        }

        protected long Count(INHibernateCriteriaSpecification specification)
        {
            long result = 0;

            if (specification != null)
            {
                this.Execute(s =>
                {
                    ICriteria criteria = specification.Build(s);
                    result = criteria.Count();
                });
            }

            return result;
        }

        protected T Get(INHibernateCriteriaSpecification specification)
        {
            T result = null;

            if (specification != null)
            {
                this.Execute(s =>
                                 {
                                     ICriteria criteria = specification.Build(s);
                                     result = criteria.UniqueResult<T>();
                                 });
            }

            return result;
        }

        protected IEnumerable<T> List(INHibernateCriteriaSpecification specification)
        {
            IList<T> result = null;

            if (specification != null)
            {
                this.Execute(s =>
                                 {
                                     ICriteria criteria = specification.Build(s);
                                     result = criteria.List<T>();
                                 });
            }

            return result;
        }

        protected PagedResult<T> List(INHibernateCriteriaSpecification specification, int startIndex, int pageSize)
        {
            PagedResult<T> result = null;

            if (specification != null)
            {
                this.Execute(s =>
                                 {

                                     ICriteria criteria = specification.Build(s);
                                     result = criteria.PagedList<T>(startIndex, pageSize);
                                 });
            }

            return result;
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