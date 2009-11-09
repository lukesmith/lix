using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Testing;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using MbUnit.Framework;
using Moq;

namespace Lix.Commons.Tests.Testing
{
    [TestFixture]
    public class SpecificationTestHelperTests
    {
        [Test]
        public void should_build_the_specification()
        {
            var specification = new Mock<FindFishWithIdSpecification>(1);

            specification.Setup(x => x.Build(It.IsAny<IQueryable<Fish>>()));

            SpecificationTestHelper.Test(specification.Object, new List<Fish>());

            specification.Verify();
        }

        [Test]
        public void should_return_an_empty_result_when_the_specification_is_null()
        {
            var result = SpecificationTestHelper.Test(null, new List<Fish>());

            result.Count().ShouldBeEqualTo(0);
        }

        [Test]
        public void should_return_an_empty_result_when_the_source_data_is_null()
        {
            var result = SpecificationTestHelper.Test(new FindFishWithDescriptionSpecification("smelly"), null);

            result.Count().ShouldBeEqualTo(0);
        }
    }
}
