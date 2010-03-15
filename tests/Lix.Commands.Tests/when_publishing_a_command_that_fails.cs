using System;
using Machine.Specifications;

namespace Lix.Commands.Tests
{
    [Subject(typeof(CommandPublisher), "Publish")]
    public class when_publishing_a_command_that_fails : CommandPublisherSpecification<ValidCommand>
    {
        private Establish context =
            () =>
                {
                    exceptionThrownByHandler = new Exception();
                    CommandHandler.Setup(x => x.Execute(Moq.It.IsAny<ValidCommand>())).Throws(exceptionThrownByHandler);
                };

        private Because of = () => exceptionThrownByHandler = Catch.Exception(() => CommandPublisher.Publish(Command));

        private It should_log_a_failure_notification = () => CommandLogger.Verify(x => x.LogFailure(
                                                                                           Moq.It.Is<ICommand>(c => c == Command),
                                                                                           Moq.It.Is<Exception>(e => e == exceptionThrownByHandler)));

        private static Exception exceptionThrownByHandler;
    }
}