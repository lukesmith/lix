using System.Linq;
using Lix.Commons.Extensions;
using Lix.Commons.Specifications;

namespace Lix.Commons.Tests.Examples.Specifications
{
    public class FindFishDescriptionContainingSpecification : IQueryableSpecification<Fish>
    {
        private readonly string description;

        public FindFishDescriptionContainingSpecification(string description)
        {
            this.description = description;
        }

        public IQueryable<Fish> Build(IQueryable<Fish> context)
        {
            return context.Like(x => x.Description, this.description, ComparisonType.Contains);
        }
    }
}