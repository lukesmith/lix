using System;
using System.Linq;

namespace Lix.Commons.Specifications
{
    public class DefaultSpecificationInterceptor : ISpecificationInterceptor
    {
        private ISpecification interceptBySpecification;

        public void With(ISpecification specification)
        {
            this.interceptBySpecification = specification;
        }

        public void With<TEntity>(Func<IQueryable<TEntity>> func)
            where TEntity : class
        {
            this.interceptBySpecification = new InterceptionQueryableSpecification<TEntity>(func());
        }

        public ISpecification InterceptedBy()
        {
            return this.interceptBySpecification;
        }
    }
}