using Lix.Commons.Specifications;

namespace Lix.Commons.Repositories
{
    public interface IDomainRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
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
    }
}