using System.Linq;
using Lix.Commons.Specifications.Executors;

namespace Lix.Commons.Repositories
{
    /// <summary>
    /// Represents a <see cref="RepositoryBase{TEntity}"/> that supports Linq queries.
    /// </summary>
    /// <typeparam name="TEntity">The entity of the repository.</typeparam>
    public abstract class LinqRepositoryBase<TEntity> : RepositoryBase<TEntity>, ILinqEnabledRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinqRepositoryBase{TEntity}"/> class.
        /// </summary>
        /// <param name="specificationExecutorFactory">
        /// The <see cref="ISpecificationExecutorFactory"/> to use.
        /// </param>
        protected LinqRepositoryBase(ISpecificationExecutorFactory specificationExecutorFactory)
            : base(specificationExecutorFactory)
        {
        }

        /// <summary>
        /// Gets the <see cref="IQueryable{T}"/> for the <see cref="ILinqEnabledRepository{TEntity}"/>.
        /// </summary>
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
        /// <returns>The <see cref="IQueryable{T}"/> for the repository.</returns>
        protected abstract IQueryable<TEntity> GetRepositoryQuery();
    }
}