using NHibernate;

namespace Lix.Commons.Specifications.NHibernate
{
    /// <summary>
    /// Represents a specification for building <see cref="ICriteria"/> instances.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to build the specification for.</typeparam>
    public abstract class NHibernateCriteriaSpecificationBase<TEntity> : INHibernateCriteriaSpecification
    {
        /// <summary>
        /// Builds the specification for the <see cref="ISession"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        public ICriteria Build(ISession context)
        {
            var criteria = context.CreateCriteria(typeof(TEntity));

            return this.Build(criteria);
        }

        /// <summary>
        /// Builds the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>
        /// An ICriteria representing the built specification.
        /// </returns>
        protected abstract ICriteria Build(ICriteria criteria);
    }
}