using System.Collections.Generic;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;

namespace Lix.Commons.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IService{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    public abstract class ServiceBase<TEntity, TRepository> : IService<TEntity>
        where TRepository : class, IRepository<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase{TEntity,TRepository}"/> class.
        /// </summary>
        /// <param name="repository">The repository to use against the service.</param>
        protected ServiceBase(TRepository repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Gets the specification to list entities.
        /// </summary>
        /// <returns>
        ///     The <see cref="IQueryableSpecification{T}"/> that represents the specification for listing <typeparamref name="TEntity"/>.
        /// </returns>
        protected virtual IQueryableSpecification<TEntity> GetListSpecification()
        {
            return Specification.Empty<TEntity>();
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>The repository.</value>
        protected TRepository Repository
        {
            get;
            private set;
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>The entity that has been saved.</returns>
        public virtual TEntity Save(TEntity entity)
        {
            return this.Repository.Save(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public virtual void Delete(TEntity entity)
        {
            this.Repository.Remove(entity);
        }

        /// <summary>
        /// Lists the instances of <typeparamref name="TEntity"/>.
        /// </summary>
        /// <returns>
        /// <see cref="IEnumerable{T}"/> collection of entities discovered by the service.
        /// </returns>
        public virtual IEnumerable<TEntity> List()
        {
            return this.Repository.List(this.GetListSpecification());
        }

        /// <summary>
        /// Lists the instances of <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="startIndex">The index to start the list from.</param>
        /// <param name="pageSize">The number of items to return.</param>
        /// <returns>
        /// A <see cref="PagedResult{T}"/> collection of <typeparamref name="TEntity"/> items.
        /// </returns>
        public virtual PagedResult<TEntity> List(int startIndex, int pageSize)
        {
            return this.Repository.List(this.GetListSpecification(), startIndex, pageSize);
        }
    }
}
