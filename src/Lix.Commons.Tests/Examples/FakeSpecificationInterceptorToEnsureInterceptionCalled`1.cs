using System;
using System.Linq;
using Lix.Commons.Specifications;
using Moq;

namespace Lix.Commons.Tests.Examples
{
    public class FakeSpecificationInterceptorToEnsureInterceptionCalled<TEntity> : ISpecificationInterceptor
        where TEntity : class 
    {
        public bool WasIntercepted
        {
            get;
            private set;
        }

        public void With(ISpecification specification)
        {
        }

#pragma warning disable 693
        public void With<TEntity>(Func<IQueryable<TEntity>> func) where TEntity : class
#pragma warning restore 693
        {
        }

        public ISpecification InterceptedBy()
        {
            this.WasIntercepted = true;
            return new Mock<IQueryableSpecification<TEntity>>().Object;
        }
    }
}