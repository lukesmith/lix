using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;

namespace Lix.Commons.Specifications.Executors
{
    public class QueryableSpecificationExecutor<TEntity> : SpecificationExecutorBase<IQueryableSpecification<TEntity>, TEntity, IQueryable<TEntity>>, IQueryableSpecificationExecutor<TEntity>
        where TEntity : class
    {
        private readonly IQueryRepository<TEntity> repository;

        public QueryableSpecificationExecutor(IQueryableSpecification<TEntity> specification, IQueryRepository<TEntity> repository)
            : base(specification)
        {
            this.repository = repository;
        }

        protected override IQueryable<TEntity> DataSource
        {
            get { return this.repository.RepositoryQuery; }
        }

        public override TEntity Get()
        {
            return this.Specification.Build(this.DataSource).SingleOrDefault();
        }

        public override IEnumerable<TEntity> List()
        {
            return this.Specification.Build(this.DataSource).ToList();
        }

        public override PagedResult<TEntity> List(int startIndex, int pageSize)
        {
            return this.Specification.Build(this.DataSource).PagedList(startIndex, pageSize);
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