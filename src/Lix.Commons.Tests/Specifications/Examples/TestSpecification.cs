using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;

namespace Lix.Commons.Tests.Specifications.Examples
{
    public class TestSpecification : DefaultQueryableSpecification<Fish>
    {
        public override IQueryable<Fish> Build(IQueryable<Fish> context)
        {
            return context;
        }
    }
}