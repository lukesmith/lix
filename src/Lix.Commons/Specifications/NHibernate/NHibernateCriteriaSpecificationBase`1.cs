using NHibernate;

namespace Lix.Commons.Specifications.NHibernate
{
    public abstract class NHibernateCriteriaSpecificationBase<T> : INHibernateCriteriaSpecification
    {
        public ICriteria Build(ISession context)
        {
            var criteria = context.CreateCriteria(typeof(T));

            return this.Build(criteria);
        }

        protected abstract ICriteria Build(ICriteria criteria);
    }
}