using System.Linq;
using Lix.Commons.Specifications.Executors;

namespace Lix.Commons.Repositories
{
    public abstract class LinqRepositoryBase<TEntity> : RepositoryBase<TEntity>, ILinqEnabledRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TEntity}"/> class.
        /// </summary>
        protected LinqRepositoryBase(ISpecificationExecutorFactory specificationExecutorFactory)
            : base(specificationExecutorFactory)
        {
        }

        IQueryable<TEntity> ILinqEnabledRepository<TEntity>.RepositoryQuery
        {
            get
            {
                return this.GetRepositoryQuery();
            }
        }

        /// <summary>
        /// Gets the repository query.
        /// </summary>
        /// <value>The repository query.</value>
        protected abstract IQueryable<TEntity> GetRepositoryQuery();
    }
}