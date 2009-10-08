using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Specifications;

namespace Lix.Commons.Testing
{
    /// <summary>
    /// Represents a helper class for testing specifications.
    /// </summary>
    public static class SpecificationTestHelper
    {
        /// <summary>
        /// Tests a the <paramref name="specification"/> against the <paramref name="sourceData"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="specification">The specification to test.</param>
        /// <param name="sourceData">The data to test the specification against.</param>
        /// <returns>
        /// An <see cref="IEnumerable{TEntity}"/> of the specification result.
        /// </returns>
        public static IEnumerable<TEntity> Test<TEntity>(IQueryableSpecification<TEntity> specification, IEnumerable<TEntity> sourceData)
        {
            if (specification == null)
            {
                return Enumerable.Empty<TEntity>();
            }

            if (sourceData == null)
            {
                return Enumerable.Empty<TEntity>();
            }

            var result = specification.Build(sourceData.AsQueryable());

            return result;
        }
    }
}
