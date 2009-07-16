
namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification.
    /// </summary>
    public class Specification
    {
        /// <summary>
        /// Creates an <see cref="EmptySpecification{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to create the specification for.</typeparam>
        /// <returns>
        /// An <see cref="EmptySpecification{TEntity}"/> object.
        /// </returns>
        public static IQueryableSpecification<TEntity> Empty<TEntity>()
        {
            return new EmptySpecification<TEntity>();
        }
    }
}
