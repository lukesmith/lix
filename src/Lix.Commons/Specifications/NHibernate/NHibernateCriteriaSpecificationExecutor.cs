using System.Collections.Generic;
using Lix.Commons.Repositories;
using Lix.Commons.Repositories.NHibernate;
using NHibernate;

namespace Lix.Commons.Specifications.NHibernate
{
    public class NHibernateCriteriaSpecificationExecutor<TEntity> : NHibernateSpecificationExecutor<INHibernateCriteriaSpecification, TEntity>
        where TEntity : class
    {
        public NHibernateCriteriaSpecificationExecutor(INHibernateCriteriaSpecification specification, ISession context)
            : base(specification, context)
        {
        }

        public override TEntity Get()
        {
            var criteria = this.Specification.Build(this.Context);
            return criteria.UniqueResult<TEntity>();
        }

        public override IEnumerable<TEntity> List(INHibernateCriteriaSpecification specification)
        {
            var criteria = specification.Build(this.Context);
            return criteria.List<TEntity>();
        }

        public override IEnumerable<TEntity> List(INHibernateCriteriaSpecification specification, int startIndex, int pageSize)
        {
            var criteria = specification.Build(this.Context);
            return criteria.PagedList<TEntity>(startIndex, pageSize);
        }

        public override long Count(INHibernateCriteriaSpecification specification)
        {
            var criteria = specification.Build(this.Context);
            return criteria.Count();
        }

        public override bool Exists()
        {
            return this.Specification.Build(this.Context).Count() > 0;
        }
    }
}