using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons
{
    /// <summary>
    /// Represents a generic collection of paged objects.
    /// </summary>
    /// <typeparam name="T">The type of the objects contained within the paged result.</typeparam>
    public class PagedResult<T> : PagedResult, IEnumerable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
        /// </summary>
        public PagedResult()
            : this(0, 0, 0, Enumerable.Empty<T>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalItems">The total items.</param>
        /// <param name="items">The items in the paged result.</param>
        public PagedResult(int startIndex, int pageSize, long totalItems, IEnumerable<T> items)
            : base(startIndex, pageSize, totalItems, items)
        {
        }

        /// <summary>
        /// Gets or sets the inner items.
        /// </summary>
        /// <value>The inner items.</value>
        protected new IEnumerable<T> InnerItems
        {
            get
            {
                return (IEnumerable<T>)base.InnerItems;
            }

            set
            {
                base.InnerItems = value;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public new IEnumerator<T> GetEnumerator()
        {
            return this.InnerItems.GetEnumerator();
        }
    }
}
