using Machine.Specifications;

namespace Lix.Commons.Tests.Specifications
{
    public class when_a_specification_executes_get_itself : specification_executes_itself
    {
        private Because of = () => repository.Get(specification);

        private It should_call_the_get_method_on_the_specification = () => specification.GetCalled.ShouldBeTrue();

        private It should_have_the_repository_set = () => specification.SetRepositoryCalled.ShouldBeTrue();
    }
}
