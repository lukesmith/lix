using System.Linq;
using Lix.Commons.Specifications;
using NHibernate;
using NHibernate.Criterion;

namespace Lix.Examples.Repository
{
    public class FindPeopleWithDuplicateNames2 : DefaultQueryableSpecification<Person>
    {
        public override IQueryable<Person> Build(IQueryable<Person> context)
        {
            return context.GroupBy(x => x.Name, x => x)
                .Where(x => x.Count() > 1)
                .SelectMany(x => x);
        }
    }

    public class FindPeopleWithDuplicateNames : DefaultNHibernateCriteriaSpecification<Person>
    {
        protected override ICriteria Build(ICriteria criteria)
        {
            criteria.Add(Expression.Sql("ID = 3"));
            return criteria;
        }
    }
}