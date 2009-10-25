using System.Collections.Generic;
using Lix.Commons.Repositories;
using NHibernate;

namespace Lix.Commons.Specifications
{
    public class DefaultNHibernateQuerySpecificationExecutor<TEntity> : NHibernateSpecificationExecutorBase<INHibernateQuerySpecification, TEntity>
        where TEntity : class
    {
        public DefaultNHibernateQuerySpecificationExecutor(INHibernateQuerySpecification specification, ISession context)
            : base(specification, context)
        {
        }

        public override TEntity Get()
        {
            var query = this.Specification.Build(this.Context);
            return query.UniqueResult<TEntity>();
        }

        public override IEnumerable<TEntity> List()
        {
            var query = this.Specification.Build(this.Context);
            return query.List<TEntity>();
        }

        public override PagedResult<TEntity> List(int startIndex, int pageSize)
        {
            var query = this.Specification.Build(this.Context);
            var countQuery = this.Specification.BuildCount(this.Context);

            return query.PagedList<TEntity>(countQuery, startIndex, pageSize);
        }

        public override long Count()
        {
            var query = this.Specification.BuildCount(this.Context);

            return query.Count();
        }

        public override bool Exists()
        {
            return this.Count() > 0;
        }
    }
}