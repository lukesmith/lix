using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;

namespace Lix.Commons.Specifications
{
    public class DefaultQueryableSpecificationExecutor<TEntity> : SpecificationExecutorBase<IQueryableSpecification<TEntity>, TEntity, IQueryable<TEntity>>
        where TEntity : class
    {
        public DefaultQueryableSpecificationExecutor(IQueryableSpecification<TEntity> specification, IQueryable<TEntity> context)
            : base(specification, context)
        {
        }

        public override TEntity Get()
        {
            return this.Specification.Build(this.Context).SingleOrDefault();
        }

        public override IEnumerable<TEntity> List()
        {
            return this.Specification.Build(this.Context).ToList();
        }

        public override PagedResult<TEntity> List(int startIndex, int pageSize)
        {
            return this.Specification.Build(this.Context).PagedList(startIndex, pageSize);
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