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

        public void With(Func<IQueryable> func)
        {
            this.interceptBySpecification = new FakeQueryableSpecification(func());
        }

        public ISpecification InterceptedBy()
        {
            return this.interceptBySpecification;
        }
    }
}