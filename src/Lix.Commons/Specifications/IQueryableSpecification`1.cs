using System.Linq;

namespace Lix.Commons.Specifications
{
    public interface IQueryableSpecification<T> : ISpecification<IQueryable<T>, IQueryable<T>>
    {
    }
}
