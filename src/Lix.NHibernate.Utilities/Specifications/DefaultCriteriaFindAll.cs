using NHibernate;

namespace Lix.Commons.Specifications
{
    public class DefaultCriteriaFindAll<TEntity> : DefaultNHibernateCriteriaSpecification<TEntity>
        where TEntity : class
    {
        protected override ICriteria Build(ICriteria criteria)
        {
            return criteria;
        }
    }
}