using Lix.Commons.Specifications;
using NHibernate;

namespace Lix.Commons.Tests.Examples.Specifications
{
    public class EmptyNHibernateCriteriaSpecification : DefaultNHibernateCriteriaSpecification<Fish>
    {
        /// <summary>
        /// Builds the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>
        /// An ICriteria representing the built specification.
        /// </returns>
        protected override ICriteria Build(ICriteria criteria)
        {
            return criteria;
        }
    }
}