using Machine.Specifications;

namespace Lix.Commands.Tests
{
    [Subject(typeof(CommandPublisher), "Publish")]
    public class when_publishing_a_command_that_succeeds : CommandPublisherSpecification<ValidCommand>
    {
        private Because of = () => CommandPublisher.Publish(Command);

#pragma warning disable 169
        protected Behaves_like<SuccessfulCommandLoggerBehavior> it_logs_a_succesful_command;
#pragma warning restore 169
    }
}