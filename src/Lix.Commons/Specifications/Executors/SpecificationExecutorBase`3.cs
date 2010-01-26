using System.Collections.Generic;

namespace Lix.Commons.Specifications.Executors
{
    public abstract class SpecificationExecutorBase<TSpecification, TEntity, TDataSource> : ISpecificationExecutor<TSpecification, TEntity>
        where TSpecification : class, ISpecification
        where TEntity : class
        where TDataSource : class
    {
        protected SpecificationExecutorBase(TSpecification specification)
        {
            this.Specification = specification;
        }

        public TSpecification Specification
        {
            get;
            private set;
        }

        protected abstract TDataSource DataSource { get; }

        public abstract TEntity Get();

        public abstract IEnumerable<TEntity> List();

        public abstract PagedResult<TEntity> List(int startIndex, int pageSize);

        public abstract long Count();

        public abstract bool Exists();
    }
}