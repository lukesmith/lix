using Lix.Commons.Repositories;
using NHibernate;

namespace Lix.Commons.Specifications.Executors
{
    public abstract class NHibernateSpecificationExecutorBase<TSpecification, TEntity> : SpecificationExecutorBase<TSpecification, TEntity, ISession>
        where TSpecification : class, ISpecification
        where TEntity : class
    {
        private readonly INHibernateRepository<TEntity> repository;

        protected NHibernateSpecificationExecutorBase(TSpecification specification, INHibernateRepository<TEntity> repository)
            : base(specification)
        {
            this.repository = repository;
        }

        protected override ISession DataSource
        {
            get { return this.repository.CurrentSession; }
        }
    }
}