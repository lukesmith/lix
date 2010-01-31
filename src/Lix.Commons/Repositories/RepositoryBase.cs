using System.Collections.Generic;
using Lix.Commons.Specifications;
using Lix.Commons.Specifications.Executors;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents an implementation of <see cref="IRepository{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class RepositoryBase<TEntity> : IReportingRepository<TEntity>, IDomainRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TEntity}"/> class.
        /// </summary>
        protected RepositoryBase(ISpecificationExecutorFactory specificationExecutorFactory)
        {
            this.SpecificationExecutorFactory = specificationExecutorFactory;
        }

        protected ISpecificationExecutorFactory SpecificationExecutorFactory
        {
            get;
            private set;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>
        /// The <typeparamref name="TEntity"/> that was added.
        /// </returns>
        public abstract TEntity Add(TEntity entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public abstract void Remove(TEntity entity);

        private ISpecificationExecutor<TEntity> GetExecutor<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification
        {
            var interceptedSpecification = Specification.Interceptors.GetReplacement(specification);

            return this.SpecificationExecutorFactory.CreateExecutor<ISpecification, TEntity, IReportingRepository<TEntity>>(
                interceptedSpecification, this);
        }

        /// <summary>
        /// Gets a single <typeparamref name="TEntity"/> that matches the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// A <typeparamref name="TEntity"/> that matched the specification.
        /// </returns>
        TEntity IReportingRepository<TEntity>.Get<TSpecification>(TSpecification specification)
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
        public IEnumerable<TEntity> List<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification
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
        public PagedResult<TEntity> List<TSpecification>(TSpecification specification, int startIndex, int pageSize)
            where TSpecification : class, ISpecification
        {
            return this.GetExecutor(specification).List(startIndex, pageSize);
        }

        /// <summary>
        /// Checks whether a <typeparamref name="TEntity"/> exists in the repository that matches the specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// true if the <see cref="IRepository{TEntity}"/> contains an item matching the specification; otherwise false.
        /// </returns>
        public bool Exists<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification
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
        public long Count<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification
        {
            return this.GetExecutor(specification).Count();
        }
    }
}
