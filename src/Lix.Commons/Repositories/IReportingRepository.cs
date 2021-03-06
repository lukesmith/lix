using System.Collections.Generic;
using Lix.Commons.Specifications;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents the interface for a repository used for reporting purposes.
    /// </summary>
    /// <typeparam name="TEntity">The entity type of the repository.</typeparam>
    public interface IReportingRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets a single <typeparamref name="TEntity"/> that matches the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <typeparam name="TSpecification">The <see cref="ISpecification"/> type.</typeparam>
        /// <returns>
        /// A <typeparamref name="TEntity"/> that matched the specification.
        /// </returns>
        TEntity Get<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification;

        /// <summary>
        /// Lists all the <typeparamref name="TEntity"/> that match the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <typeparam name="TSpecification">The <see cref="ISpecification"/> type.</typeparam>
        /// <returns>
        /// An enumerable collection of <typeparamref name="TEntity"/> that matched the specification.
        /// </returns>
        IEnumerable<TEntity> List<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification;

        /// <summary>
        /// Lists all the <typeparamref name="TEntity"/> that match the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <param name="startIndex">The index to start the list from.</param>
        /// <param name="pageSize">The number of items to return.</param>
        /// <typeparam name="TSpecification">The <see cref="ISpecification"/> type.</typeparam>
        /// <returns>
        /// A <see cref="PagedResult{TEntity}"/> collection of <typeparamref name="TEntity"/> items that matched the specification.
        /// </returns>
        PagedResult<TEntity> List<TSpecification>(TSpecification specification, int startIndex, int pageSize)
            where TSpecification : class, ISpecification;

        /// <summary>
        /// Checks whether a <typeparamref name="TEntity"/> exists in the repository that matches the specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <typeparam name="TSpecification">The <see cref="ISpecification"/> type.</typeparam>
        /// <returns>
        /// true if the <see cref="IRepository{TEntity}"/> contains an item matching the specification; otherwise false.
        /// </returns>
        bool Exists<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification;

        /// <summary>
        /// Determines how many <typeparamref name="TEntity"/> matching the <paramref name="specification"/> are contained within the repository.
        /// </summary>
        /// <param name="specification">The specification</param>
        /// <typeparam name="TSpecification">The <see cref="ISpecification"/> type.</typeparam>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/> matching the specification.
        /// </returns>
        long Count<TSpecification>(TSpecification specification)
            where TSpecification : class, ISpecification;
    }
}