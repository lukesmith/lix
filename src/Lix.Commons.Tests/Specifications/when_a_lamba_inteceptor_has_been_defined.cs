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
    public class when_a_lamba_inteceptor_has_been_defined
    {
        [TearDown]
        public void TearDown()
        {
            Specification.Interceptors.Clear();
        }

        [Test]
        public void should_return_the_lamba_functions_result_when_building_the_specification()
        {
            var fish = new Fish();
            var interceptWith = new Func<IQueryable>(() => new List<Fish> { fish }.AsQueryable());
            Specification.Intercept<TestSpecification>().With(interceptWith);

            var interceptBy = Specification.Interceptors.GetReplacement(new TestSpecification());
            var result = interceptBy.Build(fish) as IQueryable;
            result.Cast<Fish>().Contains(fish).ShouldBeEqualTo(true);
        }
    }
}
