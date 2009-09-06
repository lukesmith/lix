using Lix.Commons.Specifications;
using Lix.Commons.Tests.Specifications.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Specifications
{
    [TestFixture]
    public class when_no_interceptor_has_been_defined
    {
        [Test]
        public void should_find_the_original_specification()
        {
            var specification = new TestSpecification();

            var interceptBy = Specification.Interceptors.GetReplacement(specification);
            interceptBy.ShouldBeTheSameObjectAs(specification);
        }

        //[Test]
        //public void should_execute_the_original_specification()
        //{
        //    var mockSpecification = new Mock<TestSpecification>();

        //    mockSpecification.Setup(x => x.Build(It.IsAny<IQueryable<Fish>>())).Returns(() => Enumerable.Empty<Fish>().AsQueryable());
        //    this.inMemoryFishRepository.List(mockSpecification.Object);

        //    mockSpecification.Verify(x => x.Build(It.IsAny<IQueryable<Fish>>()));
        //}
    }
}
