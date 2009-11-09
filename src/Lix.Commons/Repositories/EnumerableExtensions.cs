using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents extensions on the <see cref="Enumerable"/> object.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Creates a <see cref="PagedResult{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the objects.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A <see cref="PagedResult{T}"/> from the enumerable.
        /// </returns>
        public static PagedResult<T> PagedList<T>(this IEnumerable<T> enumerable, int startIndex, int pageSize)
        {
            var totalItems = enumerable.LongCount();
            var items = enumerable.Skip(startIndex).Take(pageSize).ToList();

            return new PagedResult<T>(startIndex, pageSize, totalItems, items);
        }
    }
}
