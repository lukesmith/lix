using System.Linq;
using Lix.Commons.Specifications.Executors;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents an in memory repository.
    /// </summary>
    /// <typeparam name="TEntity">Type type of the entity contained within the repository.</typeparam>
    public class InMemoryRepository<TEntity> : RepositoryBase<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public InMemoryRepository(InMemoryUnitOfWork unitOfWork, ISpecificationExecutorFactory specificationExecutorFactory)
            : base(specificationExecutorFactory)
        {
            this.UnitOfWork = unitOfWork;
        }

        protected InMemoryUnitOfWork UnitOfWork
        {
            get; private set;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>The repository.</value>
        protected InMemoryDataStore DataStore
        {
            get
            {
                return this.UnitOfWork.CurrentTransactionDataStore;
            }
        }

        /// <summary>
        /// Gets the repository query.
        /// </summary>
        /// <value>The repository query.</value>
        protected override IQueryable<TEntity> GetRepositoryQuery()
        {
            return this.DataStore.List<TEntity>().AsQueryable();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>
        /// The <typeparamref name="TEntity"/> that was added.
        /// </returns>
        public override TEntity Add(TEntity entity)
        {
            this.DataStore.Save(entity);

            return entity;
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public override void Remove(TEntity entity)
        {
            this.DataStore.Remove(entity);
        }
    }
}