using Lix.Commons.Repositories;
using Lix.Commons.Specifications.Executors;
using NHibernate;

namespace Lix.Commons.Specifications
{
    public abstract class NHibernateSpecificationExecutorBase<TSpecification, TEntity> : SpecificationExecutorBase<TSpecification, TEntity, ISession>
        where TSpecification : class, ISpecification
        where TEntity : class
    {
        protected NHibernateSpecificationExecutorBase(TSpecification specification, INHibernateRepository<TEntity> repository)
            : base(specification, repository.CurrentSession)
        {
        }
    }
}