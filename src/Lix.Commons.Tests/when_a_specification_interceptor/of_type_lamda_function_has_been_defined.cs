using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_a_specification_interceptor
{
    [TestFixture]
    public class of_type_lamda_function_has_been_defined
    {
        [TearDown]
        public void TearDown()
        {
            Specification.Interceptors.Clear();
        }

        [Test]
        public void should_get_the_replacement_lambda_function_specification()
        {
            var interceptWith = new Func<IQueryable<Fish>>(() => new List<Fish>().AsQueryable());
            Specification.Intercept<FindAll<Fish>>().With(interceptWith);

            var interceptBy = Specification.Interceptors.GetReplacement(new FindAll<Fish>());
            interceptBy.ShouldSatisfy(x => x.GetType() != typeof (EmptyFishQueryableSpecification2));
        }

        [Test]
        public void should_return_the_lamba_functions_result_when_building_the_specification()
        {
            var fish = new Fish();
            var context = new List<Fish> { fish }.AsQueryable();
            var interceptWith = new Func<IQueryable<Fish>>(() => context);
            Specification.Intercept<FindAll<Fish>>().With(interceptWith);

            var interceptBy = Specification.Interceptors.GetReplacement(new FindAll<Fish>());
            interceptBy.SetContext(context);
            var result = interceptBy.Build() as IQueryable;
            result.Cast<Fish>().Contains(fish).ShouldBeEqualTo(true);
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