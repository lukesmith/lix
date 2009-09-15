using Lix.Commons.Specifications.NHibernate;
using Lix.Commons.Tests.Examples;
using NHibernate;

namespace Lix.Commons.Tests.Specifications.Examples
{
    public class TestNHibernateCriteriaSpecification : DefaultNHibernateCriteriaSpecification<Fish>
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