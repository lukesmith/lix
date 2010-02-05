using Lix.Commons.Specifications;

namespace Lix.Commons.Extensions
{
    /// <summary>
    /// Represents extensions on the <see cref="object"/> type.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Determines whether the <paramref name="obj"/> is satisfied by the <paramref name="specification"/>.
        /// </summary>
        /// <typeparam name="T">Generic type of the <paramref name="obj"/>.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <param name="specification">The <see cref="IQueryableSpecification{TEntity}"/> to use.</param>
        /// <returns>[true] if <paramref name="obj"/> satisfies the <paramref name="specification"/>, otherwise [false].</returns>
        public static bool Satisfies<T>(this T obj, IQueryableSpecification<T> specification)
        {
            return specification.IsSatisfiedBy(obj);
        }
    }
}
