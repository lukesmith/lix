using System;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents a generic repository
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity the repository is for.</typeparam>
    [Obsolete("Use specific repository, either ICommandRepository<> or IQueryRepository<>")]
    public interface IRepository<TEntity> : ICommandRepository<TEntity>, IQueryRepository<TEntity>
        where TEntity : class
    {
    }
}
