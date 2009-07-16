using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons
{
    public class PagedResult<T> : PagedResult, IEnumerable<T>
    {
        public PagedResult()
            : this(0, 0, 0, Enumerable.Empty<T>())
        {
        }

        public PagedResult(int startIndex, int pageSize, long totalItems, IEnumerable<T> items)
            : base(startIndex, pageSize, totalItems, items)
        {
        }

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
