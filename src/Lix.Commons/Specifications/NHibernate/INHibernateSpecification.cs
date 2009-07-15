using NHibernate;

namespace Lix.Commons.Specifications.NHibernate
{
    public interface INHibernateCriteriaSpecification : ISpecification<ISession, ICriteria>
    {
    }
}