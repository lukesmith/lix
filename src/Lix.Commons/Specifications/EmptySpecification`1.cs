using System.Linq;

namespace Lix.Commons.Specifications
{
    public class EmptySpecification<T> : IQueryableSpecification<T>
    {
        public IQueryable<T> Build(IQueryable<T> context)
        {
            return context;
        }
    }
}