using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Commons.Tests.Examples.Specifications
{
    public class FindFishWithIdSpecification : DefaultQueryableSpecification<Fish>
    {
        private readonly int id;

        public FindFishWithIdSpecification(int id)
        {
            this.id = id;
        }

        protected override IQueryable<Fish> Build(IQueryable<Fish> context)
        {
            return context.Where(x => x.Id == this.id);
        }
    }
}