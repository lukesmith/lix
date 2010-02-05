using System.Linq;

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a <see cref="IQueryable{TEntity}"/> <see cref="ISpecification"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to build a specification for.</typeparam>
    public interface IQueryableSpecification<TEntity> : ISpecification<IQueryable<TEntity>, IQueryable<TEntity>>
    {
        /// <summary>
        /// Determines whether the specification satisfies the entity.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to check the specification against.</param>
        /// <returns>
        /// Returns true if the <paramref name="entity"/> satisfies the specification.
        /// </returns>
        bool IsSatisfiedBy(TEntity entity);
    }
}
