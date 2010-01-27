using System;

namespace Lix.Commons.Repositories
{
    public interface ICommandRepository<TEntity>
    {
        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>
        /// The <typeparamref name="TEntity"/> that was saved.
        /// </returns>
        [Obsolete("Use the Add method.")]
        TEntity Save(TEntity entity);

        /// <summary>
        /// Adds the specified entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>
        /// The <typeparamref name="TEntity"/> that was added.
        /// </returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(TEntity entity);
    }
}