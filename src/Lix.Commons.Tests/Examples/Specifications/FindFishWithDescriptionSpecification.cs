using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Commons.Tests.Examples.Specifications
{
    public class FindFishWithDescriptionSpecification : DefaultQueryableSpecification<Fish>
    {
        private readonly string description;

        public FindFishWithDescriptionSpecification(string description)
        {
            this.description = description;
        }

        public override IQueryable<Fish> Build(IQueryable<Fish> context)
        {
            return context.Where(x => x.Description == this.description);
        }
    }
}