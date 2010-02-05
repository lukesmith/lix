using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification for building <see cref="IQueryable"/> instances.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to build the specification for.</typeparam>
    public abstract class DefaultQueryableSpecification<TEntity> : IQueryableSpecification<TEntity>
    {
        /// <summary>
        /// Builds the specification for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        public abstract IQueryable<TEntity> Build(IQueryable<TEntity> context);

        /// <summary>
        /// Builds the specification for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        public object Build(object context)
        {
            return this.Build(context as IQueryable<TEntity>);
        }

        /// <summary>
        /// Determines whether the specification satisfies the entity.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to check the specification against.</param>
        /// <returns>
        /// Returns true if the <paramref name="entity"/> satisfies the specification.
        /// </returns>
        public bool IsSatisfiedBy(TEntity entity)
        {
            var data = new List<TEntity> { entity };
            return this.Build(data.AsQueryable()).Any();
        }
    }
}