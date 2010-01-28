using System.Linq;
using Lix.Commons.Specifications;
using NHibernate;
using NHibernate.Criterion;

namespace Lix.Examples.Repository
{
    public class FindPeopleWithDuplicateNames : DefaultQueryableSpecification<Person>
    {
        public override IQueryable<Person> Build(IQueryable<Person> context)
        {
            return context.GroupBy(x => x.Name, x => x)
                .Where(x => x.Count() > 1)
                .SelectMany(x => x);
        }
    }

    public class FindPeopleWithDuplicateNames2 : DefaultNHibernateCriteriaSpecification<Person>
    {
        protected override ICriteria Build(ICriteria criteria)
        {
            var dCriteria = DetachedCriteria.For<Person>("pItem")
                .SetProjection(Projections.Count("Name"))
                .Add(Restrictions.EqProperty("pItem.Name", "person.Name"));

            return criteria.Add(Subqueries.Lt(1, dCriteria));
        }
    }
}