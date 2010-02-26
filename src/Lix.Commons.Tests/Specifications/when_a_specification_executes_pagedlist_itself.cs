using Machine.Specifications;

namespace Lix.Commons.Tests.Specifications
{
    public class when_a_specification_executes_pagedlist_itself : specification_executes_itself
    {
        private Because of = () => repository.List(specification, 0, 0);

        private It should_call_the_get_method_on_the_specification = () => specification.PagedListCalled.ShouldBeTrue();

        private It should_have_the_repository_set = () => specification.SetRepositoryCalled.ShouldBeTrue();
    }
}