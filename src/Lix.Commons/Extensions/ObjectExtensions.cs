using Lix.Commons.Specifications;

namespace Lix.Commons.Extensions
{
    public static class ObjectExtensions
    {
        public static bool Satisfies<T>(this T obj, IQueryableSpecification<T> specification)
        {
            return specification.IsSatisfiedBy(obj);
        }
    }
}
