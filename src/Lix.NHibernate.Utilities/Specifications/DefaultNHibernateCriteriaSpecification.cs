using System;
using System.Collections.Generic;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications.Executors;
using NHibernate;

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification for building <see cref="ICriteria"/> instances.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to build the specification for.</typeparam>
    public abstract class DefaultNHibernateCriteriaSpecification<TEntity> : AbstractSpecification<ISession, ICriteria>, INHibernateCriteriaSpecification, ISpecificationExecutor<TEntity>
        where TEntity : class
    {
        protected override ICriteria Build(ISession context)
        {
            var criteria = context.CreateCriteria(typeof(TEntity), typeof(TEntity).Name.ToLower());

            return this.Build(criteria);
        }

        /// <summary>
        /// Builds the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>
        /// An ICriteria representing the built specification.
        /// </returns>
        protected abstract ICriteria Build(ICriteria criteria);

        void ISpecificationExecutor<TEntity>.SetRepository(IReportingRepository<TEntity> repository)
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
            return this.Execute().PagedList<TEntity>(startIndex, pageSize);
        }

        long IExecuteCount.Count()
        {
            return this.Execute().Count();
        }

        bool IExecuteExists.Exists()
        {
            return this.Execute().Count() > 0;
        }
    }
}