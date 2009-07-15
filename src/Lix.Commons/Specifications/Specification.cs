
namespace Lix.Commons.Specifications
{
    public class Specification
    {
        public static IQueryableSpecification<T> Empty<T>()
        {
            return new EmptySpecification<T>();
        }
    }
}
