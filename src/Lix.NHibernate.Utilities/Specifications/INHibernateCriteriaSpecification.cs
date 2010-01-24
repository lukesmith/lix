using NHibernate;

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents an NHibernate <see cref="ICriteria"/> specification.
    /// </summary>
    public interface INHibernateCriteriaSpecification<TEntity> : INHibernateSpecification<ICriteria>
    {
    }
}