using System.Collections.Generic;
using Lix.Commons.Repositories;
using NHibernate;

namespace Lix.Commons.Specifications
{
    public class DefaultNHibernateCriteriaSpecificationExecutor<TEntity> : NHibernateSpecificationExecutorBase<INHibernateCriteriaSpecification, TEntity>
        where TEntity : class
    {
        public DefaultNHibernateCriteriaSpecificationExecutor(INHibernateCriteriaSpecification specification, ISession context)
            : base(specification, context)
        {
        }

        public override TEntity Get()
        {
            var criteria = this.Specification.Build(this.Context);
            return criteria.UniqueResult<TEntity>();
        }

        public override IEnumerable<TEntity> List()
        {
            var criteria = this.Specification.Build(this.Context);
            return criteria.List<TEntity>();
        }

        public override PagedResult<TEntity> List(int startIndex, int pageSize)
        {
            var criteria = this.Specification.Build(this.Context);
            return criteria.PagedList<TEntity>(startIndex, pageSize);
        }

        public override long Count()
        {
            return this.Specification.Build(this.Context).Count();
        }

        public override bool Exists()
        {
            return this.Count() > 0;
        }
    }
}