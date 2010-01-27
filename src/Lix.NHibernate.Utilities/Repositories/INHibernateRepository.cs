using NHibernate;

namespace Lix.Commons.Repositories
{
    public interface INHibernateRepository<TEntity> : IQueryRepository<TEntity>
        where TEntity : class
    {
        ISession CurrentSession { get; }
    }
}