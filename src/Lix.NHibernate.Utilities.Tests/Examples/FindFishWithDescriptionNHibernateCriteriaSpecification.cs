using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using NHibernate;
using NHibernate.Criterion;

namespace Lix.NHibernate.Utilities.Tests.Examples
{
    public class FindFishWithDescriptionNHibernateCriteriaSpecification : DefaultNHibernateCriteriaSpecification<Fish>
    {
        private readonly string description;

        public FindFishWithDescriptionNHibernateCriteriaSpecification(string description)
        {
            this.description = description;
        }

        /// <summary>
        /// Builds the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>
        /// An ICriteria representing the built specification.
        /// </returns>
        protected override ICriteria Build(ICriteria criteria)
        {
            return criteria.Add(Expression.Eq("Description", this.description));
        }
    }
}