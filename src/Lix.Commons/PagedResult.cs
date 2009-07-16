using System;
using System.Collections;

namespace Lix.Commons
{
    /// <summary>
    /// Represents a collection of paged objects.
    /// </summary>
    public class PagedResult : IEnumerable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult"/> class.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalItems">The total items.</param>
        /// <param name="items">The items in the paged result.</param>
        public PagedResult(int startIndex, int pageSize, long totalItems, IEnumerable items)
        {
            this.StartIndex = startIndex;
            this.PageSize = pageSize;
            this.TotalItemCount = totalItems;
            this.InnerItems = items;
        }

        /// <summary>
        /// Gets the total item count.
        /// </summary>
        /// <value>The total item count.</value>
        public long TotalItemCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the start index.
        /// </summary>
        /// <value>The start index.</value>
        public int StartIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total number pages.
        /// </summary>
        /// <value>The total number pages.</value>
        public int TotalNumberPages
        {
            get
            {
                if (this.PageSize > 0)
                {
                    long result;
                    long a = Math.DivRem(this.TotalItemCount, this.PageSize, out result);

                    if (result > 0)
                    {
                        return Convert.ToInt32(a) + 1;
                    }
                    else
                    {
                        return Convert.ToInt32(a) + 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has more items.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has more items; otherwise, <c>false</c>.
        /// </value>
        public bool HasMoreItems
        {
            get
            {
                if (this.StartIndex + this.PageSize < this.TotalItemCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the page number.
        /// </summary>
        /// <value>The page number.</value>
        public int PageNumber
        {
            get
            {
                if (this.PageSize > 0)
                {
                    long result;
                    long a = Math.DivRem(this.StartIndex, this.PageSize, out result);

                    return Convert.ToInt32(a) + 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the zero-based index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex
        {
            get
            {
                return this.PageNumber - 1;
            }
        }

        /// <summary>
        /// Gets the next page number.
        /// </summary>
        /// <value>The next page number.</value>
        public int NextPageNumber
        {
            get
            {
                return this.PageNumber + 1;
            }
        }

        /// <summary>
        /// Gets the zero-based index of the next page.
        /// </summary>
        /// <value>The index of the next page.</value>
        public int NextPageIndex
        {
            get
            {
                return this.NextPageNumber - 1;
            }
        }

        /// <summary>
        /// Gets the previous page number.
        /// </summary>
        /// <value>The previous page number.</value>
        public int PreviousPageNumber
        {
            get
            {
                if (this.PageNumber > 1)
                {
                    return this.PageNumber - 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the zero-based index of the previous page.
        /// </summary>
        /// <value>The index of the previous page.</value>
        public int PreviousPageIndex
        {
            get
            {
                return this.PreviousPageNumber - 1;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is the first page.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is the first page; otherwise, <c>false</c>.
        /// </value>
        public bool IsFirstPage
        {
            get
            {
                return this.PageIndex == 0;
            }
        }

        /// <summary>
        /// Gets or sets the inner items.
        /// </summary>
        /// <value>The inner items.</value>
        protected IEnumerable InnerItems
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the start index given a page number and page size.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// Value representing the start index.
        /// </returns>
        public static int GetStartIndex(int? pageNumber, int pageSize)
        {
            return pageNumber.GetValueOrDefault(0) * pageSize;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public IEnumerator GetEnumerator()
        {
            return this.InnerItems.GetEnumerator();
        }
    }
}