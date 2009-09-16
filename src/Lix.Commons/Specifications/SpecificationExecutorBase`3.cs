using System;
using System.Collections.Generic;

namespace Lix.Commons.Specifications
{
    public abstract class SpecificationExecutorBase<TSpecification, TEntity, TContext> : ISpecificationExecutor<TSpecification, TEntity>
        where TSpecification : class, ISpecification
        where TEntity : class
        where TContext : class
    {
        protected SpecificationExecutorBase(TSpecification specification, TContext context)
        {
            this.Specification = specification;
            this.Context = context;
        }

        public TSpecification Specification
        {
            get;
            private set;
        }

        public TContext Context
        {
            get;
            private set;
        }

        public abstract TEntity Get();

        public abstract IEnumerable<TEntity> List();

        public abstract IEnumerable<TEntity> List(int startIndex, int pageSize);

        public abstract long Count();

        public abstract bool Exists();

        object ISpecificationExecutor<TEntity>.Specification
        {
            get
            {
                return this.Specification;
            }
        }

        IEnumerable<TEntity> ISpecificationExecutor<TEntity>.List()
        {
            return this.List();
        }

        IEnumerable<TEntity> ISpecificationExecutor<TEntity>.List(int startIndex, int pageSize)
        {
            return this.List(startIndex, pageSize);
        }

        long ISpecificationExecutor<TEntity>.Count()
        {
            return this.Count();
        }

        bool ISpecificationExecutor<TEntity>.Exists()
        {
            return this.Exists();
        }
    }
}