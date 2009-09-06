using System.Linq;

namespace Lix.Commons.Specifications
{
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

        public object Build(object context)
        {
            return this.Build(context as IQueryable<object>);
        }
    }
}