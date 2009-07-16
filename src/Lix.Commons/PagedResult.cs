using System;
using System.Collections;

namespace Lix.Commons
{
    public class PagedResult : IEnumerable
    {
        public static int GetStartIndex(int? pageNumber, int pageSize)
        {
            return pageNumber.GetValueOrDefault(0) * pageSize;
        }

        public PagedResult(int startIndex, int pageSize, long totalItems, IEnumerable items)
        {
            this.StartIndex = startIndex;
            this.PageSize = pageSize;
            this.TotalItemCount = totalItems;
            this.InnerItems = items;
        }

        protected IEnumerable InnerItems
        {
            get;
            set;
        }

        public long TotalItemCount
        {
            get;
            private set;
        }

        public int StartIndex
        {
            get;
            private set;
        }

        public int PageSize
        {
            get;
            private set;
        }

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

        public int PageIndex
        {
            get
            {
                return this.PageNumber - 1;
            }
        }

        public int NextPageNumber
        {
            get
            {
                return this.PageNumber + 1;
            }
        }

        public int NextPageIndex
        {
            get
            {
                return this.NextPageNumber - 1;
            }
        }

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

        public int PreviousPageIndex
        {
            get
            {
                return this.PreviousPageNumber - 1;
            }
        }

        public bool IsFirstPage
        {
            get
            {
                return this.PageIndex == 0;
            }
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