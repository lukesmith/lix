using Lix.Commons.Specifications;
using NHibernate;

namespace Lix.Commons.Repositories
{
    public abstract class NHibernateSpecificationExecutor<TSpecification, TEntity> : SpecificationExecutorBase<TSpecification, TEntity, ISession>
        where TSpecification : class, ISpecification
        where TEntity : class
    {
        protected NHibernateSpecificationExecutor(TSpecification specification, ISession context)
            : base(specification, context)
        {
        }
    }
}