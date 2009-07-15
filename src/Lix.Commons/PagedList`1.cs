using System.Collections.Generic;

namespace Lix.Commons
{
    public class PagedList<T> : PagedList
    {
        public PagedList()
            : this(0, 0, 0, new List<T>())
        {
        }

        public PagedList(int startIndex, int pageSize, long totalItems, IEnumerable<T> items)
            : base(startIndex, pageSize, totalItems, items)
        {
        }

        public new IEnumerable<T> Items
        {
            get
            {
                return (IEnumerable<T>)base.Items;
            }
            protected set
            {
                base.Items = value;
            }
        }
    }
}
