using NHibernate;

namespace Lix.Commons.Repositories
{
    public interface INHibernateRepository<TEntity> : IReportingRepository<TEntity>
        where TEntity : class
    {
        ISession CurrentSession { get; }
    }
}