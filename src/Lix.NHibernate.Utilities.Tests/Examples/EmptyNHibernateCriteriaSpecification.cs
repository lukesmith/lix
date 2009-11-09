using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using NHibernate;

namespace Lix.NHibernate.Utilities.Tests.Examples
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