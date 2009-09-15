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

        /// <summary>
        /// Builds the specification for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        public override IQueryable<Fish> Build(IQueryable<Fish> context)
        {
            return context.Where(x => x.Id == this.id);
        }
    }
}