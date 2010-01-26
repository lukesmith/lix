using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Examples.Repository
{
    public class FindPeopleWithDuplicateNames : DefaultQueryableSpecification<Person>
    {
        public override IQueryable<Person> Build(IQueryable<Person> context)
        {
            return context.GroupBy(x => x.Name, x => x)
                .Where(x => x.Count() > 1)
                .SelectMany(x => x);
        }
    }
}