using NHibernate;

namespace Lix.Commons.Repositories
{
    public interface INHibernateRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        ISession CurrentSession { get; }
    }
}