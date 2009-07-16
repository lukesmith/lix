using System.Collections.Generic;
using Lix.Commons.Specifications;

namespace Lix.Commons.Services
{
    /// <summary>
    /// Represents a generic service.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity this service is for.</typeparam>
    public interface IService<TEntity>
    {
        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>
        ///     The entity that has been saved.
        /// </returns>
        TEntity Save(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Gets the specification to list entities.
        /// </summary>
        /// <returns>
        ///     The <see cref="IQueryableSpecification{T}"/> that represents the specification for listing <typeparamref name="TEntity"/>.
        /// </returns>
        IQueryableSpecification<TEntity> GetListSpecification();

        /// <summary>
        /// Lists the instances of <typeparamref name="TEntity"/>.
        /// </summary>
        /// <returns>
        ///     <see cref="IEnumerable{T}"/> collection of entities discovered by the service.
        /// </returns>
        IEnumerable<TEntity> List();

        /// <summary>
        /// Lists the instances of <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="startIndex">The index to start the list from.</param>
        /// <param name="pageSize">The number of items to return.</param>
        /// <returns>
        ///     A <see cref="PagedResult{T}"/> collection of <typeparamref name="TEntity"/> items.
        /// </returns>
        PagedResult<TEntity> List(int startIndex, int pageSize);
    }
}
