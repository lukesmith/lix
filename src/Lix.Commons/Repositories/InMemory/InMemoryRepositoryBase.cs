using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Repositories.InMemory
{
    /// <summary>
    /// Represents an in memory repository.
    /// </summary>
    /// <typeparam name="TEntity">Type type of the entity contained within the repository.</typeparam>
    public abstract class InMemoryRepositoryBase<TEntity> : RepositoryBase<TEntity, InMemoryUnitOfWork>
        where TEntity : class
    {
        private IList<TEntity> repository = new List<TEntity>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepositoryBase{TEntity}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        protected InMemoryRepositoryBase(InMemoryUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>The repository.</value>
        protected IList<TEntity> Repository
        {
            get
            {
                return this.repository;
            }
        }

        /// <summary>
        /// Gets the repository query.
        /// </summary>
        /// <value>The repository query.</value>
        protected override IQueryable<TEntity> RepositoryQuery
        {
            get { return this.Repository.AsQueryable(); }
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
            this.Repository.Add(entity);

            return entity;
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public override void Remove(TEntity entity)
        {
            this.Repository.Remove(entity);
        }
    }
}
