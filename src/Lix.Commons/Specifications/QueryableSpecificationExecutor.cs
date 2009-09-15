using System;
using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Specifications
{
    public class QueryableSpecificationExecutor<TEntity> : SpecificationExecutorBase<IQueryableSpecification<TEntity>, TEntity, IQueryable<TEntity>>
        where TEntity : class
    {
        public QueryableSpecificationExecutor(IQueryableSpecification<TEntity> specification, IQueryable<TEntity> context)
            : base(specification, context)
        {
        }

        public override TEntity Get()
        {
            return this.Specification.Build(this.Context).SingleOrDefault();
        }

        public override IEnumerable<TEntity> List(IQueryableSpecification<TEntity> specification)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> List(IQueryableSpecification<TEntity> specification, int startIndex, int pageSize)
        {
            throw new NotImplementedException();
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