using System.Linq;
using Lix.Commons.Specifications.Executors;

namespace Lix.Commons.Repositories
{
    class Linq2SqlRepositoryBase<TEntity> : LinqRepositoryBase<TEntity>
        where TEntity : class
    {
        protected Linq2SqlRepositoryBase(Linq2SqlUnitOfWork unitOfWork, ISpecificationExecutorFactory specificationExecutorFactory)
            : base(specificationExecutorFactory)
        {
            this.UnitOfWork = unitOfWork;
        }

        protected Linq2SqlUnitOfWork UnitOfWork
        {
            get; private set;
        }

        /// <summary>
        /// Gets the repository query.
        /// </summary>
        /// <value>The repository query.</value>
        protected override IQueryable<TEntity> GetRepositoryQuery()
        {
            return this.UnitOfWork.DataContext.GetTable<TEntity>();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>
        /// The <typeparamref name="TEntity"/> that was added.
        /// </returns>
        public override TEntity Add(TEntity entity)
        {
            var table = this.UnitOfWork.DataContext.GetTable<TEntity>();

            table.Attach(entity);

            return entity;
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public override void Remove(TEntity entity)
        {
            var table = this.UnitOfWork.DataContext.GetTable<TEntity>();
            table.DeleteOnSubmit(entity);
        }
    }
}