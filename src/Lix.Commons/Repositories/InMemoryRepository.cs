using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents an in memory repository.
    /// </summary>
    /// <typeparam name="TEntity">Type type of the entity contained within the repository.</typeparam>
    public class InMemoryRepository<TEntity> : RepositoryBase<TEntity, InMemoryUnitOfWork>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public InMemoryRepository(InMemoryUnitOfWork unitOfWork, ISpecificationExecutorFactory specificationExecutorFactory)
            : base(unitOfWork, specificationExecutorFactory)
        {
        }

        public new InMemoryUnitOfWork UnitOfWork
        {
            get
            {
                return base.UnitOfWork as InMemoryUnitOfWork;
            }
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
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>
        /// The <typeparamref name="TEntity"/> that was saved.
        /// </returns>
        public override TEntity Save(TEntity entity)
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