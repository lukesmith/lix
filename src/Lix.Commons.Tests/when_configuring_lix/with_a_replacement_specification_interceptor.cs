using System;
using System.Linq;
using Lix.Commons.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_configuring_lix
{
    [TestFixture]
    public class with_a_custom_specification_interceptor
    {
        [Test]
        public void should_return_an_instance_of_the_custom_interceptor()
        {
            LixObjectFactory.Initialize(
                x => x.UseSpecificationInterceptor<FakeSpecificationInterceptor>());

            LixObjectFactory.CreateSpecificationInterceptor().ShouldBeTheSameTypeAs(
                typeof (FakeSpecificationInterceptor));
        }

        private class FakeSpecificationInterceptor : ISpecificationInterceptor
        {
            public void With(ISpecification specification)
            {
                throw new NotImplementedException();
            }

            public void With<TEntity>(Func<IQueryable<TEntity>> func) where TEntity : class
            {
                throw new NotImplementedException();
            }

            public ISpecification InterceptedBy()
            {
                throw new NotImplementedException();
            }
        }
    }
}
    
