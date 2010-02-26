using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications.Executors;

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification for building <see cref="IQueryable"/> instances.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to build the specification for.</typeparam>
    public abstract class DefaultQueryableSpecification<TEntity> : AbstractSpecification<IQueryable<TEntity>, IQueryable<TEntity>>, IQueryableSpecification<TEntity>, ISpecificationExecutor<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Determines whether the specification satisfies the entity.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to check the specification against.</param>
        /// <returns>
        /// Returns true if the <paramref name="entity"/> satisfies the specification.
        /// </returns>
        public bool IsSatisfiedBy(TEntity entity)
        {
            var data = new List<TEntity> { entity };
            return this.Build(data.AsQueryable()).Any();
        }

        TEntity IExecuteGet<TEntity>.Get()
        {
            return this.Execute().SingleOrDefault();
        }

        IEnumerable<TEntity> IExecuteList<TEntity>.List()
        {
            return this.Execute().ToList();
        }

        PagedResult<TEntity> IExecuteList<TEntity>.List(int startIndex, int pageSize)
        {
            return this.Execute().PagedList(startIndex, pageSize);
        }

        long IExecuteCount.Count()
        {
            return this.Execute().Count();
        }

        bool IExecuteExists.Exists()
        {
            return this.Execute().Count() > 0;
        }

        void ISpecificationExecutor<TEntity>.SetRepository(IReportingRepository<TEntity> repository)
        {
            var linqRepository = repository as ILinqEnabledRepository<TEntity>;

            if (linqRepository != null)
            {
                ((ISpecification<IQueryable<TEntity>, IQueryable<TEntity>>)this).SetContext(linqRepository.RepositoryQuery);
            }
            else
            {
                throw new ArgumentException(string.Format("Repository is not of type {0}", typeof(ILinqEnabledRepository<TEntity>)));
            }
        }
    }
}