using Machine.Specifications;

namespace Lix.Commands.Tests
{
    [Subject(typeof(CommandPublisher), "Publish")]
    public class when_publishing_a_command_that_succeeds : CommandPublisherSpecification<ValidCommand>
    {
        private Because of = () => CommandPublisher.Publish(Command);

        private It should_log_a_succeeded_notification = () => CommandLogger.Verify(
                                                                   x => x.LogSuccess(Moq.It.Is<ICommand>(c => c == Command)));
    }
}