using System;
using System.Linq;
using System.Linq.Expressions;

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
        public virtual IQueryable<TEntity> Build(IQueryable<TEntity> context)
        {
            return context.Where(this.Predicate);
        }

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
        /// <returns>
        /// Returns true if the <param name="entity"></param> satisfies the specification.
        /// </returns>
        public bool IsSatisfiedBy(TEntity entity)
        {
            return this.Predicate.Compile()(entity);
        }

        protected abstract Expression<Func<TEntity, bool>> Predicate
        {
            get;
        }
    }
}