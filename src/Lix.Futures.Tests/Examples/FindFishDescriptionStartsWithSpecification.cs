using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Futures.Extensions;

namespace Lix.Futures.Tests.Examples
{
    public class FindFishDescriptionStartsWithSpecification : DefaultQueryableSpecification<Fish>
    {
        private readonly string description;

        public FindFishDescriptionStartsWithSpecification(string description)
        {
            this.description = description;
        }

        public override IQueryable<Fish> Build(IQueryable<Fish> context)
        {
            return context.Like(x => x.Description, this.description, ComparisonType.StartsWith);
        }
    }
}