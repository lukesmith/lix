using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents an implementation of <see cref="IRepository{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TUnitOfWork">The type of the unit of work.</typeparam>
    public abstract class RepositoryBase<TEntity, TUnitOfWork> : IRepository<TEntity>
        where TEntity : class
        where TUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TEntity, TUnitOfWork}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        protected RepositoryBase(TUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;

            this.SpecificationExecutionEngine = new SpecificationExecutionEngine();
            this.RegisterContext();
        }

        private void RegisterContext()
        {
            this.SpecificationExecutionEngine.RegisterContext<IQueryable<TEntity>>(() => this.RepositoryQuery);
            //this.SpecificationExecutionEngine.RegisterContext(this.RepositoryQuery);
        }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        public virtual IUnitOfWork UnitOfWork
        {
            get;
            private set;
        }

        protected SpecificationExecutionEngine SpecificationExecutionEngine
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the repository query.
        /// </summary>
        /// <value>The repository query.</value>
        protected abstract IQueryable<TEntity> RepositoryQuery
        {
            get;
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>
        /// The <typeparamref name="TEntity"/> that was saved.
        /// </returns>
        public abstract TEntity Save(TEntity entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public abstract void Remove(TEntity entity);

        private ISpecificationExecutor<TEntity> GetExecutor<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification
        {
            return this.SpecificationExecutionEngine.GetExecutor<TSpecification, TEntity>(specification, true);
        }

        /// <summary>
        /// Gets a single <typeparamref name="TEntity"/> that matches the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// A <typeparamref name="TEntity"/> that matched the specification.
        /// </returns>
        public TEntity Get<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification
        {
            return this.GetExecutor(specification).Get();
        }

        /// <summary>
        /// Lists all the <typeparamref name="TEntity"/> that match the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// An enumerable collection of <typeparamref name="TEntity"/> that matched the specification.
        /// </returns>
        public IEnumerable<TEntity> List(ISpecification specification)
        {
            return this.GetExecutor(specification).List();
        }

        /// <summary>
        /// Lists all the <typeparamref name="TEntity"/> that match the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <param name="startIndex">The index to start the list from.</param>
        /// <param name="pageSize">The number of items to return.</param>
        /// <returns>
        /// A <see cref="PagedResult{TEntity}"/> collection of <typeparamref name="TEntity"/> items that matched the specification.
        /// </returns>
        public PagedResult<TEntity> List(ISpecification specification, int startIndex, int pageSize)
        {
            return this.PerformList(Intercept(specification), startIndex, pageSize);
        }

        /// <summary>
        /// Checks whether a <typeparamref name="TEntity"/> exists in the repository that matches the specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// true if the <see cref="IRepository{TEntity}"/> contains an item matching the specification; otherwise false.
        /// </returns>
        public bool Exists(ISpecification specification)
        {
            return this.GetExecutor(specification).Exists();
        }

        /// <summary>
        /// Determines how many <typeparamref name="TEntity"/> matching the <paramref name="specification"/> are contained within the repository.
        /// </summary>
        /// <param name="specification">The specification</param>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/> matching the specification.
        /// </returns>
        public long Count(ISpecification specification)
        {
            return this.GetExecutor(specification).Count();
        }

        /// <summary>
        /// Lists all the <typeparamref name="TEntity"/> that match the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <param name="startIndex">The index to start the list from.</param>
        /// <param name="pageSize">The number of items to return.</param>
        /// <returns>
        /// A <see cref="PagedResult{TEntity}"/> collection of <typeparamref name="TEntity"/> items that matched the specification.
        /// </returns>
        protected virtual PagedResult<TEntity> List(IQueryableSpecification<TEntity> specification, int startIndex, int pageSize)
        {
            var specificationQuery = this.Query(specification);

            return specificationQuery.PagedList(startIndex, pageSize);
        }

        /// <summary>
        /// Queries the repository for items that match the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// A <see cref="IQueryable{TEntity}"/> query of the specification.
        /// </returns>
        protected virtual IQueryable<TEntity> Query(IQueryableSpecification<TEntity> specification)
        {
            return specification.Build(this.RepositoryQuery);
        }

        protected virtual PagedResult<TEntity> PerformList(ISpecification specification, int startIndex, int pageSize)
        {
            if (specification is IQueryableSpecification<TEntity>)
            {
                return this.List(specification as IQueryableSpecification<TEntity>, startIndex, pageSize);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static ISpecification Intercept(ISpecification specification)
        {
            return Specification.Interceptors.GetReplacement(specification);
        }
    }
}
