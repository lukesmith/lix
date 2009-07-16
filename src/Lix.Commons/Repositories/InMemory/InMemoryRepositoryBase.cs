using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Repositories.InMemory
{
    public abstract class InMemoryRepositoryBase<T> : RepositoryBase<T, InMemoryUnitOfWork>
        where T : class
    {
        private IList<T> repository = new List<T>();

        public InMemoryRepositoryBase(InMemoryUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected IList<T> Repository
        {
            get
            {
                return this.repository;
            }
        }

        protected override IQueryable<T> RepositoryQuery
        {
            get { return this.Repository.AsQueryable(); }
        }

        public override T Save(T entity)
        {
            this.Repository.Add(entity);

            return entity;
        }

        public override void Remove(T entity)
        {
            this.Repository.Remove(entity);
        }
    }
}
