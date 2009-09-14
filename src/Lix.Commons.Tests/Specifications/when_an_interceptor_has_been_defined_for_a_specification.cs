using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Specifications.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Specifications
{
    [TestFixture]
    public class when_an_interceptor_has_been_defined_for_a_specification
    {
        [TearDown]
        public void TearDown()
        {
            Specification.Interceptors.Clear();
        }

        [Test]
        public void should_execute_the_replacement_lambda_function()
        {
            var interceptWith = new Func<IQueryable<Fish>>(() => new List<Fish>().AsQueryable());
            Specification.Intercept<TestSpecification>().With(interceptWith);

            var interceptBy = Specification.Interceptors.GetReplacement(new TestSpecification());
            interceptBy.ShouldSatisfy(x => x.GetType() != typeof (TestSpecification));
        }

        [Test]
        public void should_find_the_replacement_specification()
        {
            var interceptWith = new TestSpecification2();
            Specification.Intercept<TestSpecification>().With(interceptWith);

            var interceptBy = Specification.Interceptors.GetReplacement(new TestSpecification());
            interceptBy.ShouldBeTheSameObjectAs(interceptWith);
        }

        //[Test]
        //public void should_execute_the_replacement_specification()
        //{
        //    var testSpecification = new TestSpecification();
        //    var interceptedSpecification = new Mock<TestSpecificationImpl>();

        //    interceptedSpecification.Setup(x => x.Build(It.IsAny<IQueryable<Fish>>())).Returns(() => Enumerable.Empty<Fish>().AsQueryable());

        //    Specification.Intercept<TestSpecification>().With(interceptedSpecification.Object);

        //    this.inMemoryFishRepository.List(testSpecification);

        //    interceptedSpecification.Verify(x => x.Build(It.IsAny<IQueryable<Fish>>()));
        //}
    }
}