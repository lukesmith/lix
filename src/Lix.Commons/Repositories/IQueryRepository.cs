using System.Collections.Generic;
using Lix.Commons.Specifications;

namespace Lix.Commons.Repositories
{
    public interface IQueryRepository<TEntity>
    {
        /// <summary>
        /// Gets a single <typeparamref name="TEntity"/> that matches the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// A <typeparamref name="TEntity"/> that matched the specification.
        /// </returns>
        TEntity Get<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification;

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
        /// A <see cref="PagedResult{TEntity}"/> collection of <typeparamref name="TEntity"/> items that matched the specification.
        /// </returns>
        PagedResult<TEntity> List(ISpecification specification, int startIndex, int pageSize);

        /// <summary>
        /// Checks whether a <typeparamref name="TEntity"/> exists in the repository that matches the specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// true if the <see cref="IRepository{TEntity}"/> contains an item matching the specification; otherwise false.
        /// </returns>
        bool Exists(ISpecification specification);

        /// <summary>
        /// Determines how many <typeparamref name="TEntity"/> matching the <paramref name="specification"/> are contained within the repository.
        /// </summary>
        /// <param name="specification">The specification</param>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/> matching the specification.
        /// </returns>
        long Count(ISpecification specification);

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        IUnitOfWork UnitOfWork
        {
            get;
        }
    }
}