using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Commons.Tests.Examples.Specifications
{
    public class FindIdentifiable : DefaultQueryableSpecification<IIdentifiable>
    {
        private readonly int id;

        public FindIdentifiable(int id)
        {
            this.id = id;
        }

        protected override IQueryable<IIdentifiable> Build(IQueryable<IIdentifiable> context)
        {
            return context.Where(x => x.Id == this.id);
        }
    }
}