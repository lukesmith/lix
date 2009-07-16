using System.Collections.Generic;
using Lix.Commons.Specifications;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents a generic repository
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity the repository is for.</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>
        /// The <typeparamref name="TEntity"/> that was saved.
        /// </returns>
        TEntity Save(TEntity entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Gets a single <typeparamref name="TEntity"/> that matches the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// A <typeparamref name="TEntity"/> that matched the specification.
        /// </returns>
        TEntity Get(ISpecification specification);

        /// <summary>
        /// Lists all the <typeparamref name="TEntity"/> that match the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// An enumerable collection of <typeparamref name="TEntity"/> that matched the specification.
        /// </returns>
        IEnumerable<TEntity> List(ISpecification specification);

        /// <summary>
        /// Lists all the <typeparamref name="TEntity"/> that match the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <param name="startIndex">The index to start the list from.</param>
        /// <param name="pageSize">The number of items to return.</param>
        /// <returns>
        /// A <see cref="PagedResult{T}"/> collection of <typeparamref name="TEntity"/> items that matched the specification.
        /// </returns>
        PagedResult<TEntity> List(ISpecification specification, int startIndex, int pageSize);

        /// <summary>
        /// Checks whether a <typeparamref name="TEntity"/> exists in the repository that matches the specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// true if the <see cref="IRepository{TEntity}"/> contains an item matching the specification; otherwise false.
        /// </returns>
        bool Exists(IQueryableSpecification<TEntity> specification);
    }
}
