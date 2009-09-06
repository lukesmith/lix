using System.Linq;

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents an empty specification.
    /// </summary>
    /// <typeparam name="TEntity">Type type of the entity to build the specification for.</typeparam>
    public class EmptySpecification<TEntity> : IQueryableSpecification<TEntity>
    {
        public object Build(object context)
        {
            return this.Build(context as IQueryable<object>);
        }

        /// <summary>
        /// Builds the specification for an <see cref="IQueryable{TEntity}"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        public IQueryable<TEntity> Build(IQueryable<TEntity> context)
        {
            return context;
        }
    }
}