using Machine.Specifications;
using It=Machine.Specifications.It;

namespace Lix.Commands.Tests
{
    [Subject(typeof(CommandPublisher), "Publish")]
    public class when_publishing_a_valid_command : CommandPublisherSpecification<ValidCommand>
    {
        private Because of = () => CommandPublisher.Publish(Command);

        private It should_call_the_command_handler = () => CommandHandler.Verify(x => x.Execute(Command));
    }
}