using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Commons.Tests.Examples.Specifications
{
    public class EmptyFishQueryableSpecification : DefaultQueryableSpecification<Fish>
    {
        public override IQueryable<Fish> Build(IQueryable<Fish> context)
        {
            return context;
        }
    }
}