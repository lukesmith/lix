using System.Collections.Generic;
using Lix.Commons.Repositories;

namespace Lix.Commons.Specifications.Executors
{
    public class NHibernateCriteriaSpecificationExecutor<TEntity> : NHibernateSpecificationExecutorBase<INHibernateCriteriaSpecification<TEntity>, TEntity>, INHibernateCriteriaSpecificationExecutor<TEntity>
        where TEntity : class
    {
        public NHibernateCriteriaSpecificationExecutor(INHibernateCriteriaSpecification<TEntity> specification, INHibernateRepository<TEntity> repository)
            : base(specification, repository)
        {
        }

        public override TEntity Get()
        {
            var criteria = this.Specification.Build(this.DataSource);
            return criteria.UniqueResult<TEntity>();
        }

        public override IEnumerable<TEntity> List()
        {
            var criteria = this.Specification.Build(this.DataSource);
            return criteria.List<TEntity>();
        }

        public override PagedResult<TEntity> List(int startIndex, int pageSize)
        {
            var criteria = this.Specification.Build(this.DataSource);
            return criteria.PagedList<TEntity>(startIndex, pageSize);
        }

        public override long Count()
        {
            return this.Specification.Build(this.DataSource).Count();
        }

        public override bool Exists()
        {
            return this.Count() > 0;
        }
    }
}