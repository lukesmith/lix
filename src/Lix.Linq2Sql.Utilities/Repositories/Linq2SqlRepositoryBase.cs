using System.Linq;
using Lix.Commons.Specifications.Executors;

namespace Lix.Commons.Repositories
{
    class Linq2SqlRepositoryBase<TEntity> : RepositoryBase<TEntity, Linq2SqlUnitOfWork>
        where TEntity : class
    {
        protected Linq2SqlRepositoryBase(Linq2SqlUnitOfWork unitOfWork, ISpecificationExecutorFactory specificationExecutorFactory)
            : base(unitOfWork, specificationExecutorFactory)
        {
        }

        public new Linq2SqlUnitOfWork UnitOfWork
        {
            get
            {
                return base.UnitOfWork as Linq2SqlUnitOfWork;
            }
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
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>
        /// The <typeparamref name="TEntity"/> that was saved.
        /// </returns>
        public override TEntity Save(TEntity entity)
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