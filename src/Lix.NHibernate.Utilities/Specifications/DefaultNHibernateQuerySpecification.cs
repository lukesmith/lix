using System;
using System.Collections.Generic;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications.Executors;
using NHibernate;

namespace Lix.Commons.Specifications
{
    public abstract class DefaultNHibernateQuerySpecification<TEntity> : AbstractSpecification<ISession, IQuery>, INHibernateQuerySpecification, ISpecificationExecutor<TEntity>
        where TEntity : class
    {
        IQuery INHibernateQuerySpecification.BuildCount(ISession context)
        {
            var query = context.CreateQuery(this.CountQuery());
            return this.Build(query);
        }

        protected override IQuery Build(ISession context)
        {
            var query = context.CreateQuery(this.Query());
            return this.Build(query);
        }

        protected abstract IQuery Build(IQuery query);

        /// <summary>
        /// Defines the hql query.
        /// </summary>
        /// <returns>
        /// A string representing the NHibernate HQL query.
        /// </returns>
        protected abstract string Query();

        protected virtual string CountQuery()
        {
            return string.Format("SELECT count(*) {0}", this.Query());
        }

        TEntity IExecuteGet<TEntity>.Get()
        {
            return this.Execute().UniqueResult<TEntity>();
        }

        IEnumerable<TEntity> IExecuteList<TEntity>.List()
        {
            return this.Execute().List<TEntity>();
        }

        PagedResult<TEntity> IExecuteList<TEntity>.List(int startIndex, int pageSize)
        {
            var query = this.Execute();
            var countQuery = ((INHibernateQuerySpecification)this).BuildCount(this.Context);

            return query.PagedList<TEntity>(countQuery, startIndex, pageSize);
        }

        long IExecuteCount.Count()
        {
            return this.Execute().Count();
        }

        bool IExecuteExists.Exists()
        {
            return this.Execute().Count() > 0;
        }

        public void SetRepository(IReportingRepository<TEntity> repository)
        {
            var nhibernateRepository = repository as INHibernateRepository<TEntity>;

            if (nhibernateRepository != null)
            {
                ((ISpecification<ISession, ICriteria>)this).SetContext(nhibernateRepository.CurrentSession);
            }
            else
            {
                throw new ArgumentException(string.Format("Repository is not of type {0}", typeof(INHibernateRepository<TEntity>)));
            }
        }
    }
}