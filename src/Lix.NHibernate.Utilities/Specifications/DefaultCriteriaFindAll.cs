using NHibernate;

namespace Lix.Commons.Specifications
{
    public class DefaultCriteriaFindAll<TEntity> : DefaultNHibernateCriteriaSpecification<TEntity>
    {
        protected override ICriteria Build(ICriteria criteria)
        {
            return criteria;
        }
    }
}