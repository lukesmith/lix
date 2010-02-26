using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Futures.Extensions;

namespace Lix.Futures.Tests.Examples
{
    public class FindFishEndingWithDescription : DefaultQueryableSpecification<Fish>
    {
        private readonly string description;

        public FindFishEndingWithDescription(string description)
        {
            this.description = description;
        }

        protected override IQueryable<Fish> Build(IQueryable<Fish> context)
        {
            return context.Where(x => x.Description.Like(this.description, ComparisonType.EndsWith));
        }
    }
}